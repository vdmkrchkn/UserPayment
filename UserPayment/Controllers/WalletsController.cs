using NLog;
using System;
using System.Data.Entity.Infrastructure;
using System.Web.Mvc;
using UserPayment.Models;

namespace UserPayment.Controllers
{
    public class WalletsController : Controller
    {        
        private readonly IRepository<Wallet> _repo;
        private static Logger _logger = LogManager.GetCurrentClassLogger();              

        public WalletsController(IRepository<Wallet> aRepo)
        {
            if (aRepo == null)
            {
                var message = "пустой репозитарий";
                _logger.Error(message);
                throw new Exception(message);
            }
            _repo = aRepo;
        }        

        // GET: Wallets
        [HandleError(ExceptionType = typeof(System.Data.SqlClient.SqlException))]
        public ActionResult Index()
        {                                
            return View(_repo.GetItemList());
        }

        // GET: Wallets/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                _logger.Warn("action Details: пустой id");
                return HttpNotFound();
            }

            var wallet = _repo.GetItemById(id.Value);
            if (wallet == null)
            {
                _logger.Warn("action Details: отсутствует запись с id = {0}", id);
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
                _logger.Info("создание кошелька {0}", wallet.ToString());
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
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                _logger.Warn("action Edit: пустой id");
                return HttpNotFound();
            }

            var wallet = _repo.GetItemById(id.Value);
            if (wallet == null)
            {
                _logger.Warn("action Edit: отсутствует запись с id = {0}", id);
                return HttpNotFound();
            }
            return View(wallet);
        }

        // POST: Wallets/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(int id, /*[Bind("Id,UserId,Balance")]*/ Wallet wallet)
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
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                _logger.Warn("action Delete: пустой id");
                return HttpNotFound();
            }

            var wallet = _repo.GetItemById(id.Value);
            if (wallet == null)
            {
                _logger.Warn("action Delete: отсутствует запись с id = {0}", id);
                return HttpNotFound();
            }

            return View(wallet);
        }

        // POST: Wallets/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            _repo.Delete(id);
            return RedirectToAction("Index");
        }

        private bool WalletExists(int id)
        {
            return _repo.GetItemById(id) != null;
        }
    }
}
