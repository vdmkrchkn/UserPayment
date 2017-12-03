using NLog;
using System;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Linq;
using System.Threading.Tasks;
using System.Web.Mvc;
using UserPayment.Models;

namespace UserPayment.Controllers
{
    public class WalletsController : Controller
    {        
        private readonly IRepository _repo;
        private static Logger logger = LogManager.GetCurrentClassLogger();              

        public WalletsController(IRepository aRepo)
        {
            if (aRepo == null)
            {
                var message = "пустой репозитарий";
                logger.Error(message);
                throw new Exception(message);
            }
            _repo = aRepo;
        }

        public WalletsController()
            : this(new WalletRepository("UserDBContext"))
        { }

        // GET: Wallets
        public ActionResult Index()
        {                                
            return View(_repo.GetWalletList());
        }

        // GET: Wallets/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                logger.Warn("action Details: пустой id");
                return HttpNotFound();
            }

            var wallet = _repo.GetWallet(id.Value);
            if (wallet == null)
            {
                logger.Warn("action Details: отсутствует запись с id = {0}", id);
                return HttpNotFound();
            }

            return View(wallet);
        }

        // GET: Wallets/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Wallets/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(/*[Bind("Id,UserId,Balance")]*/ Wallet wallet)
        {
            if (ModelState.IsValid)
            {
                logger.Info("создание кошелька {0}", wallet.ToString());
                _repo.Create(wallet);
                _repo.Save();
                return RedirectToAction("Index");
            }
            //else
            //    logger.Warn("создаваемая запись недопустима");
            //
            return View(wallet);
        }

        // GET: Wallets/Edit/5
        public async Task<ActionResult> Edit(int? id)
        {
            if (id == null)
            {
                logger.Warn("action Edit: пустой id");
                return HttpNotFound();
            }

            var wallet = _repo.GetWallet(id.Value);
            if (wallet == null)
            {
                logger.Warn("action Edit: отсутствует запись с id = {0}", id);
                return HttpNotFound();
            }
            return View(wallet);
        }

        // POST: Wallets/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Edit(int id, /*[Bind("Id,UserId,Balance")]*/ Wallet wallet)
        {
            if (id != wallet.Id)
            {
                return HttpNotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {                    
                    _repo.Update(wallet);                 
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!WalletExists(wallet.Id))
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
            return View(wallet);
        }

        // GET: Wallets/Delete/5
        public async Task<ActionResult> Delete(int? id)
        {
            if (id == null)
            {
                logger.Warn("action Delete: пустой id");
                return HttpNotFound();
            }

            var wallet = _repo.GetWallet(id.Value);
            if (wallet == null)
            {
                logger.Warn("action Delete: отсутствует запись с id = {0}", id);
                return HttpNotFound();
            }

            return View(wallet);
        }

        // POST: Wallets/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> DeleteConfirmed(int id)
        {            
            _repo.Delete(id);            
            return RedirectToAction("Index");
        }

        private bool WalletExists(int id)
        {
            return _repo.GetWallet(id) != null;
        }
    }
}
