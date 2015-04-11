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
                bookList = bookList.Where(b => b.Author.Contains(query));
                bookList = bookList.Where(b => b.ISBN.Contains(query));
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
                case 3:
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

        [HttpPost]
        public ActionResult AdvSearch(String query, String crn, String course, String semester, String professor, String edition, int section)
        {
            var bookList = from b in DbManager.Books
                           select b;
            String ssection = Convert.ToString(section);

            if (!String.IsNullOrEmpty(query))
            {
                bookList = bookList.Where(b => b.Title.Contains(query));
                bookList = bookList.Where(b => b.Author.Contains(query));
                bookList = bookList.Where(b => b.ISBN.Contains(query));
            }
            if (!String.IsNullOrEmpty(crn))
            {
                bookList = bookList.Where(b => b.CRN.Contains(crn));
            }
            if (!String.IsNullOrEmpty(course))
            {
                bookList = bookList.Where(b => b.Course == course);
            }
            if (!String.IsNullOrEmpty(edition))
            {
                bookList = bookList.Where(b => b.Edition.Contains(edition));
            }
            if (!String.IsNullOrEmpty(semester))
            {
                bookList = bookList.Where(b => b.Semester == semester);
            }
            if (!String.IsNullOrEmpty(professor))
            {
                bookList = bookList.Where(b => b.Professor == professor);
            }
            if (!String.IsNullOrEmpty(ssection))
            {
                bookList = bookList.Where(b => b.Section == int.Parse(ssection));
            }

            
            return View(bookList.ToList());
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
    }
}
