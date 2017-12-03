using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web.Mvc;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using UserPayment;
using UserPayment.Controllers;
using UserPayment.Models;

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

        [TestMethod, Ignore]
        public void Error()
        {
            // Упорядочение
            HomeController controller = new HomeController();
            
            // Действие
            ViewResult result = controller.Error() as ViewResult;
            var model = result.Model as ErrorViewModel;
            // Утверждение
            Assert.IsFalse(model.ShowRequestId());
        }
    }
}
