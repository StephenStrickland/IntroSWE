using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Team7_SPSUBookstore.Controllers
{
    public class BookController : BaseController
    {
        //
        // GET: /Book/
        [Route("Book/{id}")]
        public ActionResult Index( string id)
        {
                var book = DbManager.Books.Where(x => x.ISBN == id).FirstOrDefault();
            return View(book);
        }

        public class SPSU_Class
        {
            public virtual string Prof { get; set; }
            public virtual string Course { get; set; }
            public virtual int Section { get; set; }
            public virtual string Semester { get; set; }

        }

        [AllowAnonymous]
        [HttpGet]
        [Route("Book/GetClasses")]
        public JsonResult GetClasses()
        {
            var classes = DbManager.Books.Select(x => new SPSU_Class() { Prof = x.Professor, Course = x.Course, Section = x.Section, Semester = x.Semester }).ToList();
            return Json(classes, JsonRequestBehavior.AllowGet);

        }


    }

}