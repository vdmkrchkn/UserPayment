using NLog;
using System;
using System.Data.Entity.Infrastructure;
using System.Linq;
using System.Web.Mvc;
using UserPayment.Models;

namespace UserPayment.Controllers
{
    public class AccountsController : Controller
    {        
        private readonly IAccountService _service;
        private static Logger _logger = LogManager.GetCurrentClassLogger();        

        public AccountsController(IAccountService service)
        {            
            _service = service;
            //            
            SelectList list = new SelectList(_service.GetWallets().Select(w => w.Id));
            ViewBag.WalletIds = list;
        }        

        // GET: Accounts
        public ActionResult Index(string aSrcUserLogin, string aDstUserLogin)
        {
            var model = _service.GetAccounts().AsQueryable();

            model = _service.GetUserAccounts(model, aSrcUserLogin);
            model = _service.GetUserAccounts(model, aDstUserLogin);
           
            //
            //model = model.Include(a => a.Status);
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

            var account = _service.GetAccounts()
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
            // service todo: поиск счёта 
            var account = _service.GetAccounts()
                .SingleOrDefault(m => m.Id == id);
            if (account == null)
            {
                return HttpNotFound();
            }
            
            // проверка счёта на оплаченность
            var accntStatus = _service.GetAccountStatuses().SingleOrDefault(
                st => st.AccountId == account.Id && st.Status != Status.Paid);
            if (accntStatus != null)
            {
                //using (var transaction = _context.Database.BeginTransaction())
                {
                    try
                    {
                        // изменение баланса у исходного кошелька
                        var srcWallet = _service.GetWallets()
                            .SingleOrDefault(w => w.Id == account.SrcWalletId);
                        if (srcWallet != null)
                            srcWallet.Balance -= account.Price;
                        // изменение баланса у конечного кошелька
                        var dstWallet = _service.GetWallets()
                            .SingleOrDefault(w => w.Id == account.DstWalletId);
                        if (dstWallet != null)
                            dstWallet.Balance += account.Price;
                        // изменение статуса счёта
                        accntStatus.Status = Status.Paid;

                        /// todo
                        //_context.SaveChanges();
                        //transaction.Commit();
                    }
                    catch (Exception ex)
                    {
                        Console.WriteLine(ex.Message);
                        //transaction.Rollback();
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

            var account = _service.GetAccounts()
                .SingleOrDefault(m => m.Id == id);
            if (account == null)
            {
                return HttpNotFound();
            }

            // изменение статуса счёта
            var accntStatus = _service.GetAccountStatuses().SingleOrDefault(
                st => st.AccountId == account.Id && st.Status != Status.Declined);
            if (accntStatus != null)
            {
                accntStatus.Status = Status.Declined;
                /// todo
                //_context.SaveChanges();
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
            [Bind(Exclude = "Id")] Account account)
        {
            if (!_service.CreateAccount(account))
            {
                ModelState.AddModelError(string.Empty, "incorrect account data");
                return View();
            }
            return RedirectToAction("Index");            
        }

        // GET: Accounts/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return HttpNotFound();
            }

            var account = _service.GetAccounts().SingleOrDefault(m => m.Id == id);
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
            [Bind(Exclude = "Id")] Account account)
        {
            if (id != account.Id)
            {
                return HttpNotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _service.UpdateAccount(account);                    
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!_service.AccountExists(account))
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

            var account = _service.GetAccounts()
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
            var account = _service.GetAccounts().SingleOrDefault(m => m.Id == id);
            if(account != null)
                _service.DeleteAccount(account);
            return RedirectToAction("Index");
        }        
    }
}
