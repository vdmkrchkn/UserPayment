using System.Web.Mvc;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using UserPayment.Controllers;
using UserPayment.Models;
using UserPayment.Tests.Mock;
using System.Web.Routing;

namespace UserPayment.Tests.Controllers
{
    [TestClass]
    public class HomeControllerTest
    {
        [TestMethod]
        public void Index()
        {
            // Упорядочение
            HomeController controller = new HomeController();

            // Действие
            ViewResult result = controller.Index() as ViewResult;

            // Утверждение
            Assert.IsInstanceOfType(result, typeof(ViewResult));            
        }

        [TestMethod]
        public void About()
        {
            // Упорядочение
            HomeController controller = new HomeController();

            // Действие
            ViewResult result = controller.About() as ViewResult;            
            // Утверждение
            Assert.IsNotNull(result);
        }

        [TestMethod]
        public void Contact()
        {
            // Упорядочение
            HomeController controller = new HomeController();

            // Действие
            ViewResult result = controller.Contact() as ViewResult;

            // Утверждение
            Assert.IsNotNull(result);
        }

        [TestMethod]
        public void Error()
        {
            // Упорядочение
            HomeController controller = new HomeController();
            var httpContext = new MockHttpContext().Object;
            ControllerContext context = new ControllerContext(
                new RequestContext(httpContext, new RouteData()),
                controller
            );
            controller.ControllerContext = context;
            // Действие
            ViewResult result = controller.Error() as ViewResult;
            var model = result.Model as ErrorViewModel;
            // Утверждение
            Assert.IsTrue(model.ShowRequestId());
        }
    }
}
