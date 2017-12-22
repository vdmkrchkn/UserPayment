using System.Web.Mvc;
using UserPayment.Models;

namespace UserPayment.Controllers
{
    public class HomeController : Controller
    {
        public ActionResult Index()
        {            
            return View();
        }

        public ActionResult About()
        {            
            return View();
        }

        public ActionResult Contact()
        {            
            return View();
        }

        public ActionResult Error()
        {
            var request = HttpContext.Request;
            return View(
                new ErrorViewModel
                {
                    RequestId = request.ContentType             
                }
            );
        }
    }
}
