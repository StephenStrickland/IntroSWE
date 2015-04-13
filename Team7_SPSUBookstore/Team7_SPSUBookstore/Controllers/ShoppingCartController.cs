using Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Team7_SPSUBookstore.Controllers
{
    public class ShoppingCartController : BaseController
    {
        //
        // GET: /ShoppingCart/
        public ActionResult Index()
        {
            var model = new List<ShoppingCartBook>();

            if (Session["ShoppingCart"] != null)
                model = (List<ShoppingCartBook>)Session["ShoppingCart"];
            else
            {
                ShoppingCartBook a = new ShoppingCartBook();
                a.ISBN = "978-0073376356";
                a.QuantityInCart = 5;
                a.Price = (decimal)55.55;
                a.TypeInCart = (StockType.New);

                ShoppingCartBook b = new ShoppingCartBook();
                b.ISBN = "978-0132662253";
                b.QuantityInCart = 7;
                b.Price = (decimal)33.33;
                b.TypeInCart = (StockType.Used);

                model.Add(a);
                model.Add(b);
            }

            ViewData["ShoppingCartBooks"] = model;

            Session["ShoppingCart"] = model;

            //CalculateSubtotal();

            return View();
        }


        public ActionResult Checkout()
        {
            return RedirectToAction("Index", "Order");
            //return View("Order/BillingAndPaymentInfo.cshtml");
        }


        public ActionResult RemoveFromCart(int id)
        {
            var cartBooks = new List<ShoppingCartBook>();
            cartBooks = (List<ShoppingCartBook>)Session["ShoppingCart"];
            cartBooks.RemoveAt(id);
            Session.Add("ShoppingCart", cartBooks);

            cartBooks = null;

            return RedirectToAction("Index");
        }

        public ActionResult AddToCart(string isbn, StockType stockType, int qty)
        {
            var bookToAdd = new ShoppingCartBook();
            bookToAdd.ISBN = isbn;
            bookToAdd.TypeInCart = stockType;
            bookToAdd.QuantityInCart = qty;

            var cartBooks = new List<ShoppingCartBook>();

            if (Session["ShoppingCart"] != null)
            {
                cartBooks = (List<ShoppingCartBook>)Session["ShoppingCart"];
            }
            bookToAdd.Price = DbManager.Books.Where(x => x.ISBN == bookToAdd.ISBN).FirstOrDefault()
                .Stock.Where(x => x.Type == stockType).Select(x => x.Price).FirstOrDefault();
            cartBooks.Add(bookToAdd);

            Session["ShoppingCart"] = cartBooks;

            bookToAdd = null;
            cartBooks = null;

            return RedirectToAction("PreCart", "ShoppingCart");
        }

        [HttpPost]
        public ViewResult CalculateSubtotal()
        {
            decimal CartSubtotal = 0.00M;

            var cartBooks = new List<ShoppingCartBook>();

            if (Session["ShoppingCart"] != null)
            {
                cartBooks = (List<ShoppingCartBook>)Session["ShoppingCart"];

                foreach (ShoppingCartBook book in cartBooks)
                {
                    CartSubtotal += book.Price * book.QuantityInCart;
                }
            }

            ViewData.Add("CartSubtotal", CartSubtotal);

            return View();
        }

        [HttpPost]
        public ViewResult ItemsInCart()
        {
            var cartBooks = new List<ShoppingCartBook>();

            if (Session["ShoppingCart"] != null)
            {
                cartBooks = (List<ShoppingCartBook>)Session["ShoppingCart"];
            }

            var count = 0;

            foreach(ShoppingCartBook b in cartBooks)
            {
                count += b.QuantityInCart;
            }

            ViewData.Add("ItemsInCart", count);

            return View();
        }

        public ActionResult PreCart()
        {
            CalculateSubtotal();
            ItemsInCart();

            var model = new List<ShoppingCartBook>();

            if (Session["ShoppingCart"] != null)
                model = (List<ShoppingCartBook>)Session["ShoppingCart"];

            ViewData["ShoppingCartBooks"] = model;

            Session["ShoppingCart"] = model;

            return View("PreCart");
        }

        [HttpPost]
        public ActionResult PreCartInvalid()
        {
            return View("Index", "Home");
        }

        [HttpPost]
        public ActionResult BookDetails(string isbn)
        {
            var bookDetails = DbManager.Books.Where(x => x.ISBN == isbn).First();

            ViewData.Add("BookDetails", "author");

            return View();
        }

        public String GetAuthor (string isbn)
        {
            return DbManager.Books.Where(x => x.ISBN == isbn).First().Author;
        }

        public String GetTitle(string isbn)
        {
            return DbManager.Books.Where(x => x.ISBN == isbn).First().Title;
        }

        public String GetImage(string isbn)
        {
            return DbManager.Books.Where(x => x.ISBN == isbn).First().ISBN;
        }
    }
}
