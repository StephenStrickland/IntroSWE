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
            model = (List<ShoppingCartBook>)Session["ShoppingCart"];

            ViewData["ShoppingCartBook"] = model;

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
            var model = new List<ShoppingCartBook>();
            model = (List<ShoppingCartBook>)Session["ShoppingCart"];
            model.RemoveAt(id);
            Session.Add("ShoppingCart", model);

            return RedirectToAction("Index");
        }

        public ActionResult AddToCart(int id)
        {
            var model = (List<ShoppingCartBook>)Session["ShoppingCart"];

            return RedirectToAction("PreCart", "ShoppingCart");
        }

        public ActionResult PreCart()
        {
            return View("PreCart");
        }
    }
}
