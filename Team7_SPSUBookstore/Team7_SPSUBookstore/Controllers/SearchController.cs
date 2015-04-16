using System.Web.Routing;
using Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Microsoft.Ajax.Utilities;

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
        public ActionResult Index(SearchCriteria AdvCriteria, int sort = 0)//, int sortOrder)
        {
            bool advanced = true;
            if (AdvCriteria.BasicSearch.IsNullOrWhiteSpace())
                AdvCriteria.BasicSearch = "";
            AdvCriteria.BasicSearch = AdvCriteria.BasicSearch.ToLower();
            String[] criteria = AdvCriteria.BasicSearch.Split(' ');
            List<String> criteriaList = new List<String>(criteria);
            criteriaList.Remove("the");
            criteriaList.Remove("of");
            criteriaList.Remove("a");
            List<BookDatabaseItem> bookList = DbManager.Books.ToList();
            List<String> searchResultsISBNList = new List<String>();
            Session.Add("SearchCritera", AdvCriteria.BasicSearch);
           
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

                       bookList = bookList.Where(x => x.Semester.Equals(AdvCriteria.Semester)).ToList();
                        
                    }
                    if (!String.IsNullOrEmpty(AdvCriteria.Course))
                    {
                        bookList = bookList.Where(x => x.Course.Equals(AdvCriteria.Course)).ToList();
                    }
                    if (AdvCriteria.Section != 0)
                    {
                        bookList = bookList.Where(x => x.Section.Equals(AdvCriteria.Section)).ToList();
                    }
                    if (!String.IsNullOrEmpty(AdvCriteria.Professor))
                    {
                        bookList = bookList.Where(x => x.Professor.Equals(AdvCriteria.Professor)).ToList();

                    }
                   
                    if (AdvCriteria.CRN != 0)
                    {
                        bookList = bookList.Where(x => x.CRN.ToString().Equals(AdvCriteria.CRN)).ToList();

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
            ViewBag.SortType = (SortType) 0;
            if (tempBookList.Count > 0)
                bookList = tempBookList;
            if (bookList.Count == 1 || tempBookList.Count ==1)
            {
                bookList = Sort(bookList, 0);
                Session.Add("SearchResults", bookList);
                RedirectToAction("Index", "Book", bookList.FirstOrDefault().ISBN);
            }
            if (!tempBookList.Any())
            {
                tempBookList = Sort(tempBookList, sort);
                Session.Add("SearchResults", tempBookList);
                return View(tempBookList);
            }
            SearchTerms(AdvCriteria.BasicSearch);
            bookList = Sort(bookList, 0);
            Session.Add("SearchResults", bookList);
            return View(bookList.ToList());
        }
     

        [HttpGet]
        public ActionResult Index()
        {
            ViewBag.Title = "No results found for " + Session["SearchCritera"] + "! Pleae try something else.";
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
        public List<BookDatabaseItem> Sort(List<BookDatabaseItem> searchResults, int sortType)
        {
            switch (sortType)
            {
                case 1:
                    searchResults = searchResults.OrderByDescending(b => b.Title).ToList();
                    break;
                case 2:
                    searchResults = searchResults.OrderBy(b => b.Author).ToList();
                    break;
                case 3:
                    searchResults = searchResults.OrderByDescending(b => b.Author).ToList();
                    break;
                case 4:
                    searchResults = searchResults.OrderByDescending(b => b.Stock.First().Price).ToList();
                    break;
                case 5:
                    searchResults = searchResults.OrderBy(b => b.Stock.First().Price).ToList();
                    break;
                default:
                    searchResults = searchResults.OrderBy(b => b.Title).ToList();
                    break;
            }

            return searchResults;
        }

        [HttpGet]
        public ActionResult SortResults(int sortType)
        {
            var searchCritera = (List<BookDatabaseItem>)Session["SearchResults"];
            ViewBag.SearchTerms = Session["SearchCritera"];
            ViewBag.SortType = (SortType) sortType;
            return View("Index", Sort(searchCritera, sortType));
        }

        public ActionResult SearchTerms(String criteria)
        {
            ViewBag.SearchTerms = Session["SearchCritera"];
            return View();
        }

        public enum SortType
        {
            TitleAtoZ, TitleZtoA, AuthorAtoZ, AuthoerZtoA, PriceNewHightoLow, PriceNewLowtoHigh
        }

    }
}
