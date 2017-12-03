using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Web.Mvc;
using UserPayment.Controllers;
using UserPayment.Models;
using Moq;
using System.Collections.Generic;

namespace UserPayment.Tests.Controllers
{
    [TestClass]
    public class WalletControllerTest
    {
        [TestMethod]
        public void IndexViewModel_IsViewResult()
        {
            // Arrange
            var mock = new Mock<IRepository<Wallet>>();
            mock.Setup(a => a.GetItemList()).Returns(new List<Wallet>());            
            WalletsController controller = new WalletsController(mock.Object);

            // Act
            ViewResult result = controller.Index() as ViewResult;

            // Assert            
            Assert.IsInstanceOfType(result, typeof(ViewResult));
            //Assert.IsInstanceOfType(result.Model as HomeController, typeof(HomeController));
        }

        [TestMethod]
        public void CreatePostAction_RedirectToIndexView()
        {
            // arrange
            var expected = "Index";
            var mock = new Mock<IRepository<Wallet>>();
            var wallet = new Wallet();
            var controller = new WalletsController(mock.Object);
            // act
            RedirectToRouteResult result = controller.Create(wallet) as RedirectToRouteResult;

            // assert
            Assert.IsNotNull(result);
            Assert.AreEqual(expected, result.RouteValues["action"]);
        }

        [TestMethod]
        public void CreatePostAction_SaveModel()
        {
            // arrange
            var mock = new Mock<IRepository<Wallet>>();
            var wallet = new Wallet();
            var controller = new WalletsController(mock.Object);
            // act
            RedirectToRouteResult result = controller.Create(wallet) as RedirectToRouteResult;
            // assert
            mock.Verify(a => a.Create(wallet));
            mock.Verify(a => a.Save());
        }
    }
}
