using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Linq;
using System.Threading.Tasks;
using System.Web.Mvc;
using UserPayment.Models;

namespace UserPayment.Controllers
{
    public class UsersController : Controller
    {
        private readonly UserDBContext _context;

        public UsersController() : this(new UserDBContext()) { }

        public UsersController(UserDBContext context)
        {
            _context = context;
        }

		// GET: Users
		public ActionResult Index()
		{
			return View(_context.User.ToList());
		}

		// GET: Users/Details/5
		public ActionResult Details(int? id)
		{
			if (id == null)
			{
				return HttpNotFound();
			}

			var user = _context.User
				.SingleOrDefault(m => m.Id == id);
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
				_context.User.Add(user);
				_context.SaveChanges();
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

            var user = _context.User.SingleOrDefault(m => m.Id == id);
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
                try
                {
                    _context.Entry(user).State = EntityState.Modified;
                //_context.Update(user);    // asp.net core
                    _context.SaveChanges();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!UserExists(user.Id))
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

			var user = _context.User
				.SingleOrDefault(m => m.Id == id);
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
			var user = _context.User.SingleOrDefault(m => m.Id == id);
			_context.User.Remove(user);
			_context.SaveChanges();
			return RedirectToAction("Index");
		}

		private bool UserExists(int id)
        {
            return _context.User.Any(e => e.Id == id);
        }
    }
}
