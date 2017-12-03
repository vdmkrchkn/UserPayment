using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Linq;
using System.Threading.Tasks;
using System.Web.Mvc;
using UserPayment.Models;

namespace UserPayment.Controllers
{
    public class AccountsController : Controller
    {
        private readonly UserDBContext _context;

        public AccountsController(UserDBContext context)
        {
            _context = context;
        }

        // GET: Accounts
        public async Task<ActionResult> Index(string aSrcUserLogin, string aDstUserLogin)
        {
            var accnt = from acc in _context.Account                                         
                        select acc;           

            if (!String.IsNullOrEmpty(aSrcUserLogin))
            {
                var userWallets = from w in _context.Wallet
                                  join user in _context.User
                                  on w.UserId equals user.Id
                                  where user.Login.Equals(aSrcUserLogin)
                                  select w.Id;

                accnt = accnt.Where(s => userWallets.Contains(s.SrcWalletId));
            }

            if (!String.IsNullOrEmpty(aDstUserLogin))
            {
                var userWallets = from w in _context.Wallet
                                  join user in _context.User
                                  on w.UserId equals user.Id
                                  where user.Login.Equals(aDstUserLogin)
                                  select w.Id;

                accnt = accnt.Where(s => userWallets.Contains(s.DstWalletId));
            }


            //
            SelectList list = new SelectList(_context.Wallet.Where(w => w.User.Login.Equals(aSrcUserLogin)));
            ViewBag.WalletIds = list;
            //
           // accnt = accnt.Include(a => a.Status);
            return View(accnt.ToList());
        }        

        // GET: Accounts/Details/5
        public async Task<ActionResult> Details(int? id)
    {
        if (id == null)
        {
            return HttpNotFound();
        }

        var account = _context.Account
            .SingleOrDefault(m => m.Id == id);
        if (account == null)
        {
            return HttpNotFound();
        }

        return View(account);
    }

        // 
        public async Task<ActionResult> Pay(int? id)
        {
            if (id == null)
            {
                return HttpNotFound();
            }

            var account = _context.Account
                .SingleOrDefault(m => m.Id == id);
            if (account == null)
            {
                return HttpNotFound();
            }

            // изменение статуса счёта
            var accntStatus = _context.AccountStatuses.SingleOrDefault(
                st => st.AccountId == account.Id && st.Status != Status.Paid);
            if (accntStatus != null)                
            {                    
                // изменение баланса у исходного кошелька
                var srcWallet = _context.Wallet.SingleOrDefault(w => w.Id == account.SrcWalletId);
                if (srcWallet != null)
                    srcWallet.Balance -= account.Price;
                // изменение баланса у конечного кошелька
                var dstWallet = _context.Wallet.SingleOrDefault(w => w.Id == account.DstWalletId);
                if (dstWallet != null)
                    dstWallet.Balance += account.Price;

                accntStatus.Status = Status.Paid;

                _context.SaveChanges();
            }

            return RedirectToAction("Index");
        }

        // 
        public async Task<ActionResult> Decline(int? id)
        {
            if (id == null)
            {
                return HttpNotFound();
            }

            var account = _context.Account
                .SingleOrDefault(m => m.Id == id);
            if (account == null)
            {
                return HttpNotFound();
            }

            // изменение статуса счёта
            var accntStatus = _context.AccountStatuses.SingleOrDefault(
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
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create(/*[Bind("SrcWalletId,DstWalletId,Price,Comment")]*/ Account account)
        {
            if (ModelState.IsValid)
            {
                account.Date = DateTime.Today;
                _context.Account.Add(account);
                _context.SaveChanges();

                _context.AccountStatuses.Add(
                    new AccountStatus
                    {
                        AccountId = _context.Account.Last().Id,
                        Status = Status.New }
                    );

                _context.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(account);
        }

        // GET: Accounts/Edit/5
        public async Task<ActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return HttpNotFound();
            }

            var account = _context.Account.SingleOrDefault(m => m.Id == id);
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
        public async Task<ActionResult> Edit(int id, /*[Bind("Id,SrcWalletId,DstWalletId,Date,Price,Comment")]*/ Account account)
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
        public async Task<ActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return HttpNotFound();
            }

            var account = _context.Account
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
        public async Task<ActionResult> DeleteConfirmed(int id)
        {
            var account = _context.Account.SingleOrDefault(m => m.Id == id);
            _context.Account.Remove(account);
            _context.SaveChanges();
            return RedirectToAction("Index");
        }

        private bool AccountExists(int id)
        {
            return _context.Account.Any(e => e.Id == id);
        }
    }
}
