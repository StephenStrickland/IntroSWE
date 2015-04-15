using BookstoreDataSource;
using Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Team7_SPSUBookstore.Controllers
{
    public class BaseController : Controller
    {
        //
        // GET: /Base/

        public BaseController()
	{
            DbManager = new DbManager("Resources/users.txt", "Resources/books.xlsx");
       
	}

        public DbManager DbManager;




        public List<SelectListItem> GetProfessorsForDropDown()
        {

            return DbManager.Books.Select(x => x.Professor).Distinct().Select(x => new SelectListItem() { Text = x.ToString(), Value = x.ToString() }).OrderBy(x => x.Text).ToList();

        }
        public List<SelectListItem> GetSectionsForDropDown()
        {

            return DbManager.Books.Select(x => x.Section).Distinct().Select(x => new SelectListItem() { Text = x.ToString(), Value = x.ToString() }).ToList();

        }
        public List<SelectListItem> GetCoursesForDropDown()
        {

            return DbManager.Books.Select(x => new SelectListItem() { Text = x.Course, Value = x.Course }).OrderBy(x => x.Text).ToList();

        }

        public void SetSearchCriteria()
        {
            ViewData["profs"] = GetProfessorsForDropDown();
            ViewData["section"] = GetSectionsForDropDown();
            ViewData["course"] = GetCoursesForDropDown();
        }





	}
}