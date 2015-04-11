using Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Team7_SPSUBookstore.Controllers
{
    public class OrderController : Controller
    {
        //
        // GET: /Order/
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult BillingAndPaymentInfo()
        {
            if (Session["Order"] == null)
            {
                var cart = (List<ShoppingCartBook>)Session["ShoppingCart"];
                var order = new Order(cart);
                Session["Order"] = order;
            }
            return View();
        }

        [HttpPost]
        public ActionResult Confirm()
        {
            if (ModelState.IsValid)
            {
                return RedirectToAction("Confirmation");
            }
            return RedirectToAction("BillingAndPaymentInfo");
        }

        public ActionResult Confirmation()
        {

            return View();
        }


        public string GenerateReceipt()
        {
            string receipt =
                @"Thank you for your purchase of {0} books for a total of {1}.

Customer: {2}

Payment Method:
{3}

Shipping Info:
{4}

";





            return "";
        }


    }
}
