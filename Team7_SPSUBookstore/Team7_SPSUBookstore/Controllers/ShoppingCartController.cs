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

            ViewData["ShoppingCartBooks"] = model;

            Session["ShoppingCart"] = model;

            CalculateSubtotal();

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

        public ActionResult AddToCart(string isbn, StockType stockType, int qty, bool fromCart = false)
        {
            bool isAlreadyInCart = false;
            var cartBooks = new List<ShoppingCartBook>();
            var bookToAdd = new ShoppingCartBook();

            if (Session["ShoppingCart"] != null)
                cartBooks = (List<ShoppingCartBook>)Session["ShoppingCart"];

            int i = cartBooks.FindIndex(item => item.ISBN == isbn);
            if (i >= 0)
                foreach (var b in cartBooks)
                    if (b.ISBN.Equals(isbn) && b.TypeInCart.Equals(stockType))
                    {
                        isAlreadyInCart = !isAlreadyInCart;
                        i = cartBooks.IndexOf(b);
                    }

            if (!isAlreadyInCart)
            {
                bookToAdd.ISBN = isbn;
                bookToAdd.TypeInCart = stockType;
                bookToAdd.QuantityInCart = qty;

                bookToAdd.Price = DbManager.Books.Where(x => x.ISBN == bookToAdd.ISBN).FirstOrDefault()
                    .Stock.Where(x => x.Type == stockType).Select(x => x.Price).FirstOrDefault();
                cartBooks.Add(bookToAdd);
            }
            else if(fromCart)
            {
                
                cartBooks[i].QuantityInCart = qty;
                cartBooks.Add(cartBooks[i]);
                cartBooks.RemoveAt(i);
            }
            else
            {
                cartBooks[i].QuantityInCart += qty;
                cartBooks.Add(cartBooks[i]);
                cartBooks.RemoveAt(i);
            }

            Session["ShoppingCart"] = cartBooks;

            bookToAdd = null;
            cartBooks = null;
            if(fromCart)
            {
                return RedirectToAction("Index", "ShoppingCart");
            }
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

            ViewBag.CartSubtotal = CartSubtotal;
            //ViewData.Add("CartSubtotal", CartSubtotal);

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

            foreach (ShoppingCartBook b in cartBooks)
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

        public String GetAuthor(string isbn)
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
