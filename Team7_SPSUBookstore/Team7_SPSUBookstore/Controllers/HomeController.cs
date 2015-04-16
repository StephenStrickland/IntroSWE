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
            IList<BookDatabaseItem> books = DbManager.Books.AsQueryable().Where(x => x.ISBN == "Something").ToList();
            ViewBag.Title = "something";
            ViewBag.hey = ";aoisnfoasnflksnlksnlsknf";
           var j = new List<SelectListItem>();
           j.Add(new SelectListItem() { Text="asfd", Value="asf"});
           SetSearchCriteria();
           ViewBag.noSearchBar = true;
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