using Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Team7_SPSUBookstore.Controllers
{
    public class HomeController : BaseController
    {
        public ActionResult Index()
        {
            IList<Book> books = DbManager.Books.AsQueryable().Where(x => x.ISBN == "Something").ToList();
            Session["isLoggedIn"] = false;
            ViewBag.Title = "something";
            ViewBag.hey = ";aoisnfoasnflksnlksnlsknf";
            return View();
        }

        public ActionResult About()
        {
            ViewBag.Message = "Your application description page.";

            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }
    }
}