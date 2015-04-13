using Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Team7_SPSUBookstore.Controllers
{
    public class OrderController : BaseController
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
        public ActionResult BillingAndPaymentInfo(Order order)
        {
            if (ModelState.IsValid)
            {
                Order o = (Order)Session["Order"];
                o.PaymentInfo = order.PaymentInfo;
                o.ShippingInfo = order.ShippingInfo;
                Session["Order"] = o;
                SetSearchCriteria();
                return RedirectToAction("Confirmation");
            }

            return View();
        }


        public ActionResult Confirmation()
        {
            return View((Order)Session["Order"]);
        }

        [HttpPost]
        public ActionResult Receipt()
        {
            return View((Order) Session["Order"]);
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
