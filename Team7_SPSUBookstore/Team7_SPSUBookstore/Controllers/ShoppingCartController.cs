using Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Team7_SPSUBookstore.Controllers
{
    public class ShoppingCartController : Controller
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
                a.ISBN = "33333";
                a.QuantityInCart = 5;
                a.Price = (decimal)55.55;
                a.TypeInCart = (StockType.New);

                ShoppingCartBook b = new ShoppingCartBook();
                b.ISBN = "44444";
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

        //
        // GET: /ShoppingCart/Details/5
        public ActionResult Details(int id)
        {
            return View();
        }

        //
        // GET: /ShoppingCart/Create
        public ActionResult Create()
        {
            return View();
        }

        //
        // POST: /ShoppingCart/Create
        [HttpPost]
        public ActionResult Create(FormCollection collection)
        {
            try
            {
                // TODO: Add insert logic here

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }

        //
        // GET: /ShoppingCart/Edit/5
        public ActionResult Edit(int id)
        {
            return View();
        }

        //
        // POST: /ShoppingCart/Edit/5
        [HttpPost]
        public ActionResult Edit(int id, FormCollection collection)
        {
            try
            {
                // TODO: Add update logic here

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }

        //
        // GET: /ShoppingCart/Delete/5
        public ActionResult Delete(int id)
        {
            return View();
        }

        //
        // POST: /ShoppingCart/Delete/5
        [HttpPost]
        public ActionResult Delete(int id, FormCollection collection)
        {
            try
            {
                // TODO: Add delete logic here

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
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

        public ActionResult AddToCart(int id)
        {
            var cartBooks = new List<ShoppingCartBook>();

            if (Session["ShoppingCart"] != null)
            {
                cartBooks = (List<ShoppingCartBook>)Session["ShoppingCart"];
            }

            //cartBooks.Add(id);
            //from book page add item here

            Session["ShoppingCart"] = cartBooks;

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
                    CartSubtotal += book.Price;
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

            ViewData.Add("ItemsInCart", cartBooks.Count);

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
    }
}
