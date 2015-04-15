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
        public ActionResult Index(SearchCriteria AdvCriteria)//, int sortOrder)
        {
            bool advanced = AdvCriteria.isAdvanced;
            AdvCriteria.BasicSearch = AdvCriteria.BasicSearch.ToLower();
            String[] criteria = AdvCriteria.BasicSearch.Split(' ');
            List<String> criteriaList = new List<String>(criteria);
            criteriaList.Remove("the");
            criteriaList.Remove("of");
            criteriaList.Remove("a");
            List<BookDatabaseItem> bookList = DbManager.Books.ToList();
            List<String> searchResultsISBNList = new List<String>();
           
                foreach (var c in criteriaList)
                {
                    if (c.Any(char.IsDigit) && (c.Length == 13|| c.Length == 14))
                    {
                        var firstHalf = c.Substring(0, 3);
                        var secondHalf = c.Substring(3);

                         var book = bookList.Where(x => (x.ISBN.ToLower().Contains(firstHalf + "-" + secondHalf) || (x.ISBN.ToLower().Contains(c)))).Select(x => x.ISBN).ToList();
                        if (book.Count == 1)
                        {
                            return RedirectToAction("Index", "Book", new {id = book.FirstOrDefault()});
                        }
                    
                    }
             
                }
                if (advanced)
                  {
                    if (!String.IsNullOrEmpty(AdvCriteria.Semester))
                    {

                       bookList = bookList.Where(x => x.Semester.ToLower().Contains(AdvCriteria.Semester)).ToList();
                        
                    }
                    if (!String.IsNullOrEmpty(AdvCriteria.Course))
                    {
                        bookList = bookList.Where(x => x.Course.ToLower().Contains(AdvCriteria.Course)).ToList();
                    }
                    if (AdvCriteria.Section != 0)
                    {
                        bookList = bookList.Where(x => x.Section.Equals(AdvCriteria.Section)).ToList();
                    }
                    if (!String.IsNullOrEmpty(AdvCriteria.Professor))
                    {
                        bookList = bookList.Where(x => x.Professor.ToLower().Contains(AdvCriteria.Professor)).ToList();

                    }
                   
                    if (AdvCriteria.CRN != 0)
                    {
                        bookList = bookList.Where(x => x.CRN.Equals(AdvCriteria.CRN)).ToList();

                    }
                    if (AdvCriteria.isRequired == true)
                    {
                        bookList = bookList.Where(x => x.isRequired.Equals(AdvCriteria.isRequired)).ToList();

                    }
            
                }
                var tempBookList = new List<BookDatabaseItem>();

            foreach(var c in criteriaList)
            {

                   tempBookList.AddRange( bookList.Where(x => x.Title.ToLower().Contains(c)).ToList());
 
                   tempBookList.AddRange( bookList.Where(x => x.Author.ToLower().Contains(c)).ToList());
              

        
            }
            tempBookList = tempBookList.Distinct().ToList();
            if (tempBookList.Count > 0)
                bookList = tempBookList;
            if (bookList.Count == 1 || tempBookList.Count ==1)
            {
                RedirectToAction("Index", "Book", bookList.FirstOrDefault().ISBN);
            }
            if (!tempBookList.Any())
            {
                return View(tempBookList);
            }
            SearchTerms(AdvCriteria.BasicSearch);

            return View(bookList.ToList());
        }
     

        [HttpGet]
        public ActionResult Index()
        {
            ViewBag.Title = "nothing here, try searching for something!!!";
            return View();
        }

        [HttpPost]
        public ActionResult AdvSearch(String query, SearchCriteria AdvCriteria)
        {
            bool advanced = AdvCriteria.isAdvanced;
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
             
                    if (!String.IsNullOrEmpty(AdvCriteria.Semester))
                    {
                        searchResultsISBNList.AddRange(bookList.Where(x => x.Semester.ToLower().Contains(AdvCriteria.Semester)).Select(x => x.ISBN).ToList());

                    }
                    if (!String.IsNullOrEmpty(AdvCriteria.Course))
                    {
                        searchResultsISBNList.AddRange(bookList.Where(x => x.Course.ToLower().Contains(AdvCriteria.Course)).Select(x => x.ISBN).ToList());
                    }
                    if (AdvCriteria.Section != 0)
                    {
                        searchResultsISBNList.AddRange(bookList.Where(x => x.Section.Equals(AdvCriteria.Section)).Select(x => x.ISBN).ToList());
                    }
                    if (!String.IsNullOrEmpty(AdvCriteria.Professor))
                    {
                        searchResultsISBNList.AddRange(bookList.Where(x => x.Professor.ToLower().Contains(AdvCriteria.Professor)).Select(x => x.ISBN).ToList());

                    }
                   
                    if (AdvCriteria.CRN != 0)
                    {
                        searchResultsISBNList.AddRange(bookList.Where(x => x.CRN.Equals(AdvCriteria.CRN)).Select(x => x.ISBN).ToList());

                    }
                    if (AdvCriteria.isRequired == true)
                    {
                        searchResultsISBNList.AddRange(bookList.Where(x => x.isRequired.Equals(AdvCriteria.isRequired)).Select(x => x.ISBN).ToList());

                    }
            
                }
                searchResultsISBNList = searchResultsISBNList.Distinct().ToList();

            }

            
            return View(bookList.ToList());
        }


        public ActionResult SearchTerms(String criteria)
        {
            ViewBag.SearchTerms = criteria;
            return View();
        }
    }
}
