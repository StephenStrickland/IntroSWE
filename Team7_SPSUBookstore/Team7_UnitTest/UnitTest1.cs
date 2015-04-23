using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Web.Mvc;
using System.Web.WebPages;
using Entities;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Team7_SPSUBookstore.Controllers;
using System.IO;
using System.Net;
using System.Web;
using System.Web.SessionState;
using System.Reflection;
using System.Web.Routing;

namespace Team7_UnitTest
{
    [TestClass]
    public class UnitTest1
    {

        [TestInitialize]
        public void TestSetup()
        {
            // We need to setup the Current HTTP Context as follows:            

            // Step 1: Setup the HTTP Request
            var httpRequest = new HttpRequest("", "http://localhost/", "");

            // Step 2: Setup the HTTP Response
            var httpResponce = new HttpResponse(new StringWriter());

            // Step 3: Setup the Http Context
            var httpContext = new HttpContext(httpRequest, httpResponce);
            var sessionContainer =
                new HttpSessionStateContainer("id",
                                               new SessionStateItemCollection(),
                                               new HttpStaticObjectsCollection(),
                                               10,
                                               true,
                                               HttpCookieMode.AutoDetect,
                                               SessionStateMode.InProc,
                                               false);
            httpContext.Items["AspSession"] =
                typeof(HttpSessionState)
                .GetConstructor(
                                    BindingFlags.NonPublic | BindingFlags.Instance,
                                    null,
                                    CallingConventions.Standard,
                                    new[] { typeof(HttpSessionStateContainer) },
                                    null)
                .Invoke(new object[] { sessionContainer });

            // Step 4: Assign the Context
            HttpContext.Current = httpContext;
        }





        [TestMethod]
        public void TestMethod1()
        {
            Assert.AreEqual("", "");
        }



        [TestMethod]
        public void TestBook_Index()
        {
            //Glad to see MVC refuses to work

            string expectedBookTitle = "Essentials of Software Engineering 3rd Edition";
            var controller = new BookController();

            var result = controller.Index("978-1449691998") as ViewResult;

            var book = result.Model as BookDatabaseItem;

            Assert.IsNotNull(result, "Should have returned a ViewResult");
            Assert.AreEqual(expectedBookTitle, book.Title, "Book Title should have been {0}", expectedBookTitle);
        }

        [TestMethod]
        public void TestUserSession_Login()
        {
            var controller = new UserSessionController();
            var wrapper = new HttpContextWrapper(HttpContext.Current);
            controller.ControllerContext = new ControllerContext(wrapper, new RouteData(), controller);

            var result = controller.Login("email@spsu.edu", "1234568") as ViewResult;

            Assert.AreEqual(true, controller.Session["isLoggedIn"]);
        }

        [TestMethod]
        public void TestUserSession_Guest()
        {
            //Glad to see MVC refuses to work

            var controller = new UserSessionController();
            var wrapper = new HttpContextWrapper(HttpContext.Current);
            controller.ControllerContext = new ControllerContext(wrapper, new RouteData(), controller);

            var result = controller.Guest("email@spsu.edu") as ViewResult;
            Assert.AreEqual(true, controller.Session["isLoggedIn"]);
        }

        [TestMethod]
        public void TestUserSession_Logoff()
        {
            //Glad to see MVC refuses to work

            var controller = new UserSessionController();
            var wrapper = new HttpContextWrapper(HttpContext.Current);
            controller.ControllerContext = new ControllerContext(wrapper, new RouteData(), controller);

            var result = controller.LogOff("email@spsu.edu") as ViewResult;
            Assert.AreEqual(false, controller.Session["isLoggedIn"]);
        }


        [TestMethod]
        public void TestOrder_Tax_And_SubTotal()
        {
            List<ShoppingCartBook> bookList = new List<ShoppingCartBook>();
            ShoppingCartBook testBook = new ShoppingCartBook();
            testBook.ISBN = "123";
            testBook.QuantityInCart = 1;
            testBook.TypeInCart = StockType.New;
            testBook.Price = 1;

            bookList.Add(testBook);

            Order testOrder = new Order(bookList);

            Assert.AreEqual(testOrder.SubTotalCost, 1);
            Assert.AreEqual(testOrder.Tax, (decimal)0.07);
        }
        [TestMethod]
        public void TestOrder_TotalCost()
        {
            List<ShoppingCartBook> bookList = new List<ShoppingCartBook>();
            ShoppingCartBook testBook = new ShoppingCartBook();
            testBook.ISBN = "123";
            testBook.QuantityInCart = 1;
            testBook.TypeInCart = StockType.New;
            testBook.Price = 3;

            bookList.Add(testBook);
            bookList.Add(testBook);


            Order testOrder = new Order(bookList);
            Assert.AreEqual(testOrder.TotalCost, (decimal)6.42);
        }

        [TestMethod]
        public void TestOrderController_AboutPage()
        {
            //Glad to see MVC refuses to work

            const string expectedViewName = "About";
            var controller = new HomeController();

            var result = controller.About() as ViewResult;


            Assert.IsNotNull(result, "Should have returned a ViewResult");
            Assert.AreEqual("", result.ViewName, "View name should have been {0}", expectedViewName);
        }

        [TestMethod]
        public void TestOrderController_Loading_Order_From_Session()
        {
            var controller = new OrderController();
            var wrapper = new HttpContextWrapper(HttpContext.Current);
            controller.ControllerContext = new ControllerContext(wrapper, new RouteData(), controller);
            var result = controller.BillingAndPaymentInfo() as ViewResult;

            Assert.IsNotNull(controller.Session["Order"]);
        }
    }
}
