using Entities;
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
        public ActionResult Index(string query)//, int sortOrder)
        {
            query.ToLower();
            String[] criteria = query.Split(' ');
            List<String> criteriaList = new List<String>(criteria);
            criteriaList.Remove("the");
            criteriaList.Remove("of");
            criteriaList.Remove("a");
            List<BookDatabaseItem> bookList = DbManager.Books.ToList();
            List<String> searchResultsISBNList = new List<String>();
            

            foreach(var c in criteriaList)
            {
                if (c.Any(char.IsDigit) && c.Length == 13)
                {
                    var firstHalf = c.Substring(0, 3);
                    var secondHalf = c.Substring(3);
                     
                    searchResultsISBNList.AddRange(bookList.Where(x => x.ISBN.ToLower().Contains( firstHalf + "-" + secondHalf )).Select(x => x.ISBN).ToList());
                } 
                
                searchResultsISBNList.AddRange(bookList.Where(x => x.Title.ToLower().Contains(c)).Select(x => x.ISBN).ToList());
                searchResultsISBNList.AddRange(bookList.Where(x => x.Author.ToLower().Contains(c)).Select(x => x.ISBN).ToList());
                searchResultsISBNList.AddRange(bookList.Where(x => x.ISBN.ToLower().Contains(c)).Select(x => x.ISBN).ToList()); 
            }
            searchResultsISBNList= searchResultsISBNList.Distinct().ToList();

            Console.Write("aefaefawfe");
            //switch (sortOrder)
            //{
            //    case 1:
            //        bookList = bookList.OrderByDescending(b => b.Author);
            //        break;
            //    case 2:
            //        bookList = bookList.OrderBy(b => b.Author);
            //        break;
            //    //case 3:
            //    //    bookList = bookList.OrderByDescending(b => b.Price);
            //    //    break;
            //    //case 4:
            //    //    bookList = bookList.OrderBy(b => b.Price);
            //    //    break;
            //    case 3:
            //        bookList = bookList.OrderByDescending(b => b.Title);
            //        break;
            //    default:
            //        bookList = bookList.OrderBy(b => b.Title);
            //        break;
            //}
            return View(bookList.ToList());
        }
     

        [HttpGet]
        public ActionResult Index()
        {
            ViewBag.Title = "nothing here, try searching for something!!!";
            return View();
        }

        [HttpPost]
        public ActionResult AdvSearch(String query, SearchCriteria ADVcriteria)
        {
            bool advanced = ADVcriteria.isAdvanced;
            query.ToLower();
            String[] criteria = query.Split(' ');
            List<String> criteriaList = new List<String>(criteria);
            criteriaList.Remove("the");
            criteriaList.Remove("of");
            criteriaList.Remove("a");
            List<BookDatabaseItem> bookList = DbManager.Books.ToList();
            List<String> searchResultsISBNList = new List<String>();
            
            if (advanced == true)
            {
                foreach (var c in criteriaList)
                {
                    if (c.Any(char.IsDigit) && c.Length == 13)
                    {
                        var firstHalf = c.Substring(0, 3);
                        var secondHalf = c.Substring(3);

                        searchResultsISBNList.AddRange(bookList.Where(x => x.ISBN.ToLower().Contains(firstHalf + "-" + secondHalf)).Select(x => x.ISBN).ToList());
                    }
                    if (ADVcriteria.Course != null)
                    {
                        searchResultsISBNList.AddRange(bookList.Where(x => x.Course.ToLower().Contains(ADVcriteria.Course)).Select(x => x.ISBN).ToList());
                    }
                    if (ADVcriteria.Section != 0)
                    {
                        searchResultsISBNList.AddRange(bookList.Where(x => x.Section.Equals(ADVcriteria.Section)).Select(x => x.ISBN).ToList());
                    }
                    if (ADVcriteria.Professor != null)
                    {
                        searchResultsISBNList.AddRange(bookList.Where(x => x.Professor.ToLower().Contains(ADVcriteria.Professor)).Select(x => x.ISBN).ToList());

                    }
                    if (ADVcriteria.Semester != null)
                    {
                        searchResultsISBNList.AddRange(bookList.Where(x => x.Semester.ToLower().Contains(ADVcriteria.Semester)).Select(x => x.ISBN).ToList());

                    }
                    if (ADVcriteria.Course != null)
                    {
                        searchResultsISBNList.AddRange(bookList.Where(x => x.Course.ToLower().Contains(ADVcriteria.Course)).Select(x => x.ISBN).ToList());

                    }
                    if (ADVcriteria.CRN != 0)
                    {
                        searchResultsISBNList.AddRange(bookList.Where(x => x.CRN.Equals(ADVcriteria.CRN)).Select(x => x.ISBN).ToList());

                    }
                }
                searchResultsISBNList = searchResultsISBNList.Distinct().ToList();

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
