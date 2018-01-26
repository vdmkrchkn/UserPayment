using NLog;
using System;
using System.Data.Entity.Infrastructure;
using System.Linq;
using System.Web.Mvc;
using UserPayment.Models;

namespace UserPayment.Controllers
{
    public class UsersController : Controller
    {
        private readonly IRepository<User> _repo;
        private static Logger _logger = LogManager.GetCurrentClassLogger();

        public UsersController(IRepository<User> aRepo)
        {
            if (aRepo == null)
            {
                var message = "пустой репозитарий";
                _logger.Error(message);
                throw new Exception(message);
            }
            _repo = aRepo;
        }

        // GET: Users
        public ActionResult Index()
		{
			return View(_repo.GetItemList());
		}

		// GET: Users/Details/5
		public ActionResult Details(int? id)
		{
			if (id == null)
			{
				return HttpNotFound();
			}

			var user = _repo.GetItem(id.Value);
			if (user == null)
			{
				return HttpNotFound();
			}

			return View(user);
		}
		
		[HttpGet] // Users/Create
		public ActionResult Create()
        {
            return View();
        }

		// POST: Users/Create
		// To protect from overposting attacks, please enable the specific properties you want to bind to, for 
		// more details see http://go.microsoft.com/fwlink/?LinkId=317598.
		[HttpPost]
		[ValidateAntiForgeryToken]
		public ActionResult Create(/*[Bind("Id,Login,Password")]*/ User user)
		{
			if (ModelState.IsValid)
			{
				if(UserExists(user))
				{
					ModelState.AddModelError(string.Empty, "user exists");
					return View(user);
				}
				_repo.Create(user);
				_repo.Save();
				return RedirectToAction("Index");
			}
			return View(user);
		}

		[HttpGet] // Users/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return HttpNotFound();
            }

            var user = _repo.GetItem(id.Value);
            if (user == null)
            {
                return HttpNotFound();
            }
            return View(user);
        }

        // POST: Users/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(int id, /*[Bind("Id,Login,Password")]*/ User user)
        {
            if (id != user.Id)
            {
                return HttpNotFound();
            }

            if (ModelState.IsValid)
            {
				if (UserExists(user))
				{
					ModelState.AddModelError(string.Empty, "user exists");
					return View(user);
				}

				try
                {                    
                    _repo.Update(user);
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!UserExists(user))
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
            return View(user);
        }

		// GET: Users/Delete/5
		public ActionResult Delete(int? id)
		{
			if (id == null)
			{
				return HttpNotFound();
			}

			var user = _repo.GetItem(id.Value);
			if (user == null)
			{
				return HttpNotFound();
			}

			return View(user);
		}

		// POST: Users/Delete/5
		[HttpPost, ActionName("Delete")]
		[ValidateAntiForgeryToken]
		public ActionResult DeleteConfirmed(int id)
		{			
			_repo.Delete(id);			
			return RedirectToAction("Index");
		}

		private bool UserExists(User aUser)
        {
			// проверка по id
			bool isUserExists = _repo.GetItemList().Any(e => e.Id == aUser.Id);
			// если по id нет, то проверка по логин
			if (!isUserExists)
				return _repo.GetItemList().Any(e => e.Login == aUser.Login);
			return true;
        }
    }
}
