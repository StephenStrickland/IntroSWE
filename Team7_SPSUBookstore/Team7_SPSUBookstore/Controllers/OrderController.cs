using System.Text;
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
            Order o = (Order) Session["Order"];
            Session["Order"] = null;
            Session["ShoppingCart"] = null;
            return View(o);
        }

        public string GenerateReceipt(Order o)
        {
            StringBuilder sb = new StringBuilder();
            sb.Append("-------------------------");
            sb.Append("Shipping Info");
            sb.Append("Billing Info"):
            for(ShoppingCartBook book in o.BooksInCart  )
            {
                
            }
            string receipt = "";
            return "";
        }


    }
}
