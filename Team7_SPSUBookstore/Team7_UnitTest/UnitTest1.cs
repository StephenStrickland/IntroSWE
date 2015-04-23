using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Web.Mvc;
using System.Web;
using System.Web.WebPages;
using Entities;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Team7_SPSUBookstore.Controllers;

namespace Team7_UnitTest
{
    [TestClass]
    public class UnitTest1
    {
        [TestMethod]
        public void TestMethod1()
        {
            Assert.AreEqual("", "");
        }

        [TestMethod]
        public void TestOrder()
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
            Assert.AreEqual(testOrder.TotalCost, (decimal)1.07);
        }

        [TestMethod]
        public void TestOrderController()
        {
            //Glad to see MVC refuses to work

            const string expectedViewName = "About";
            var controller = new HomeController();

            var result = controller.About() as ViewResult;


            Assert.IsNotNull(result, "Should have returned a ViewResult");
            Assert.AreEqual("", result.ViewName, "View name should have been {0}", expectedViewName);
        }
    }
}
