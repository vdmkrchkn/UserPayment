using NLog;
using System;
using System.Data;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Linq;
using System.Web.Mvc;
using UserPayment.Models;

namespace UserPayment.Controllers
{
    public class AccountsController : Controller
    {
        private readonly EFDbContext _context;
        private static Logger _logger = LogManager.GetCurrentClassLogger();

        public AccountsController() : this(new EFDbContext()) { }

        public AccountsController(EFDbContext context)
        {
            _context = context;
        }

        IQueryable<Account> GetUserAccounts(IQueryable<Account> accountModel,
            string aUserLogin)
        {
            if (!string.IsNullOrEmpty(aUserLogin))
            {
                var userWallets = from w in _context.Set<Wallet>()
                                  join user in _context.Set<User>()
                                  on w.UserId equals user.Id
                                  where user.Login.Equals(aUserLogin)
                                  select w.Id;

                return accountModel.Where(s => userWallets.Contains(s.SrcWalletId));
            }

            return accountModel;
        }

        // GET: Accounts
        public ActionResult Index(string aSrcUserLogin, string aDstUserLogin)
        {
            var model = from acc in _context.Set<Account>()
                        select acc;

            model = GetUserAccounts(model, aSrcUserLogin);
            model = GetUserAccounts(model, aDstUserLogin);

            //
            SelectList list = new SelectList(
                _context.Set<Wallet>().Where(w => w.User.Login.Equals(aSrcUserLogin)));
            ViewBag.WalletIds = list;
            //
            model = model.Include(a => a.Status);
            //
            if (Request.IsAjaxRequest())
                return PartialView("_Accounts", model.ToList());
            //
            return View(model.ToList());
        }

        // GET: Accounts/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return HttpNotFound();
            }

            var account = _context.Set<Account>()
                .SingleOrDefault(m => m.Id == id);
            if (account == null)
            {
                return HttpNotFound();
            }

            return View(account);
        }

        // оплатить счёт
        public ActionResult Pay(int? id)
        {
            if (id == null)
            {
                return HttpNotFound();
            }
            // поиск счёта
            var account = _context.Set<Account>()
                .SingleOrDefault(m => m.Id == id);
            if (account == null)
            {
                return HttpNotFound();
            }
            
            // проверка счёта на оплаченность
            var accntStatus = _context.Set<AccountStatus>().SingleOrDefault(
                st => st.AccountId == account.Id && st.Status != Status.Paid);
            if (accntStatus != null)
            {
                using (var transaction = _context.Database.BeginTransaction())
                {
                    try
                    {
                        // изменение баланса у исходного кошелька
                        var srcWallet = _context.Set<Wallet>()
                            .SingleOrDefault(w => w.Id == account.SrcWalletId);
                        if (srcWallet != null)
                            srcWallet.Balance -= account.Price;
                        // изменение баланса у конечного кошелька
                        var dstWallet = _context.Set<Wallet>()
                            .SingleOrDefault(w => w.Id == account.DstWalletId);
                        if (dstWallet != null)
                            dstWallet.Balance += account.Price;
                        // изменение статуса счёта
                        accntStatus.Status = Status.Paid;

                        _context.SaveChanges();
                        transaction.Commit();
                    }
                    catch (Exception ex)
                    {
                        Console.WriteLine(ex.Message);
                        transaction.Rollback();
                    }
                }                
            }

            return RedirectToAction("Index");
        }

        // 
        public ActionResult Decline(int? id)
        {
            if (id == null)
            {
                return HttpNotFound();
            }

            var account = _context.Set<Account>()
                .SingleOrDefault(m => m.Id == id);
            if (account == null)
            {
                return HttpNotFound();
            }

            // изменение статуса счёта
            var accntStatus = _context.Set<AccountStatus>().SingleOrDefault(
                st => st.AccountId == account.Id && st.Status != Status.Declined);
            if (accntStatus != null)
            {
                accntStatus.Status = Status.Declined;

                _context.SaveChanges();
            }

            return RedirectToAction("Index");
        }

        // GET: Accounts/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Accounts/Create
        // To protect from overposting attacks, please enable the specific properties 
        // you want to bind to, for more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(
            /*[Bind("SrcWalletId,DstWalletId,Price,Comment")]*/ Account account)
        {
            if (ModelState.IsValid)
            {
				// проверка валидности создаваемого счёта
				if(!account.isValid())
				{
					ModelState.AddModelError(string.Empty, "incorrect account data");
					return View(account);
				}

                account.Date = DateTime.Today;
                _context.Set<Account>().Add(account);
                _context.SaveChanges();				

				_context.Set<AccountStatus>().Add(
                    new AccountStatus
                    {
                        AccountId = _context.Set<Account>()
                            .OrderByDescending(w => w.Id).First().Id,
                        Status = Status.New
                    }
                );

                _context.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(account);
        }

        // GET: Accounts/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return HttpNotFound();
            }

            var account = _context.Set<Account>().SingleOrDefault(m => m.Id == id);
            if (account == null)
            {
                return HttpNotFound();
            }
            return View(account);
        }

        // POST: Accounts/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(int id,
            /*[Bind("Id,SrcWalletId,DstWalletId,Date,Price,Comment")]*/ Account account)
        {
            if (id != account.Id)
            {
                return HttpNotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Entry(account).State = EntityState.Modified;
                    //_context.Update(account);
                    _context.SaveChanges();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!AccountExists(account.Id))
                    {
                        return HttpNotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction("Index");
            }
            return View(account);
        }

        // GET: Accounts/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return HttpNotFound();
            }

            var account = _context.Set<Account>()
                .SingleOrDefault(m => m.Id == id);
            if (account == null)
            {
                return HttpNotFound();
            }

            return View(account);
        }

        // POST: Accounts/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            var account = _context.Set<Account>().SingleOrDefault(m => m.Id == id);
            _context.Set<Account>().Remove(account);
            _context.SaveChanges();
            return RedirectToAction("Index");
        }

        private bool AccountExists(int id)
        {
            return _context.Set<Account>().Any(e => e.Id == id);
        }
    }
}
