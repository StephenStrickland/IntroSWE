using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Team7_SPSUBookstore.Controllers
{
    public class SearchController : BaseController
    {
        //
        // GET: /Search/
        [HttpPost]
        //public ActionResult Index(string criteria)
       // {
           // ViewBag.Title = criteria;
           // return View();
        //}
        public ActionResult Index(string query, int sortOrder)
        {
            var bookList = from b in DbManager.Books
                           select b;
            if (!String.IsNullOrEmpty(query))
            {
                bookList = bookList.Where(b => b.Title.Contains(query));
            }
            switch (sortOrder)
            {
                case 1:
                    bookList = bookList.OrderByDescending(b => b.Author);
                    break;
                case 2:
                    bookList = bookList.OrderBy(b => b.Author);
                    break;
                //case 3:
                //    bookList = bookList.OrderByDescending(b => b.Price);
                //    break;
                //case 4:
                //    bookList = bookList.OrderBy(b => b.Price);
                //    break;
                case 5:
                    bookList = bookList.OrderByDescending(b => b.Title);
                    break;
                default:
                    bookList = bookList.OrderBy(b => b.Title);
                    break;
            }
            return View(bookList.ToList());
        }

        [HttpGet]
        public ActionResult Index()
        {
            ViewBag.Title = "nothing here, try searching for something!!!";
            return View();
        }

        //
        // GET: /Search/Details/5
        public ActionResult Details(int id)
        {
            return View();
        }

        //
        // GET: /Search/Create
        public ActionResult Create()
        {
            return View();
        }

        //
        // POST: /Search/Create
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
        // GET: /Search/Edit/5
        public ActionResult Edit(int id)
        {
            return View();
        }

        //
        // POST: /Search/Edit/5
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
        // GET: /Search/Delete/5
        public ActionResult Delete(int id)
        {
            return View();
        }

        //
        // POST: /Search/Delete/5
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
    }
}
