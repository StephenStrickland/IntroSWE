using System.Text;
using BookstoreDataSource;
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
            Order o = (Order)Session["Order"];
            Session["Order"] = null;
            Session["ShoppingCart"] = null;
            GenerateReceipt(o);
            Dictionary<string, string> dict = new Dictionary<string, string>();

            foreach (ShoppingCartBook book in o.BooksInCart)
            {
                string title = DbManager.Books.ToList().Where(x => x.ISBN.Equals(book.ISBN)).First().Title;
                dict.Add(book.ISBN, title);
            }
            ViewBag.titles = dict;
            return View(o);
        }

        public void GenerateReceipt(Order o)
        {
            StringBuilder sb = new StringBuilder();

            sb.AppendLine("-------------------------");
            sb.AppendLine("Shipping Info");
            sb.AppendLine("First Name: " + o.ShippingInfo.FirstName);
            sb.AppendLine("Last Name: " + o.ShippingInfo.LastName);
            sb.AppendLine("Street Address: " + o.ShippingInfo.StreetAddr);
            sb.AppendLine("City: " + o.ShippingInfo.City + " State: " + o.ShippingInfo.State + " Zip Code: " + o.ShippingInfo.ZipCode);
            sb.AppendLine();
            sb.AppendLine("Billing Info");
            sb.AppendLine("First Name: " + o.PaymentInfo.FirstName);
            sb.AppendLine("Last Name: " + o.PaymentInfo.LastName);
            sb.AppendLine("Street Address: " + o.PaymentInfo.StreetAddr);
            sb.AppendLine("City: " + o.PaymentInfo.City + " State: " + o.PaymentInfo.State + " Zip Code: " + o.PaymentInfo.ZipCode);
            sb.AppendLine("Credit Card: " + o.PaymentInfo.CCnumberStub);
            sb.AppendLine("-------------------------");
            foreach (ShoppingCartBook book in o.BooksInCart)
            {
                string title = DbManager.Books.ToList().Where(x => x.ISBN.Equals(book.ISBN)).First().Title;
                string link = "www.spsubookstore.com/ebook/download/" + book.ISBN + ".pdf";
                sb.AppendLine("Title: " + title);
                sb.AppendLine("ISBN: " + book.ISBN);
                sb.AppendLine("Type: " + book.TypeInCart);
                sb.AppendLine("Quantity: " + book.QuantityInCart);
                sb.AppendLine("Price: " + book.Price);
                if (book.TypeInCart == StockType.eBook)
                {
                    sb.AppendLine("Download Link: " + link);
                }
                sb.AppendLine();
            }
            long milliseconds = DateTime.Now.Ticks / TimeSpan.TicksPerMillisecond;
            DateTime date = DateTime.Now;
            String root = AppDomain.CurrentDomain.BaseDirectory;
            String file = root + "/Resources/Receipts/" + date.Month + "-" + date.Day + "-" + milliseconds + "-" + o.ShippingInfo.FirstName + " " + o.ShippingInfo.LastName + ".txt";
            System.IO.File.WriteAllText(file, sb.ToString());
        }


    }
}
