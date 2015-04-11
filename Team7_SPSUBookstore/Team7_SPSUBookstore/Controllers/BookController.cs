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
        public ActionResult Index()
        {
            return View();
        }

        //
        // GET: /Book/Details/5
        public ActionResult Details(int id)
        {
            return View();
        }

        //
        // GET: /Book/Create
        public ActionResult Create()
        {
            return View();
        }

        //
        // POST: /Book/Create
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
        // GET: /Book/Edit/5
        public ActionResult Edit(int id)
        {
            return View();
        }

        //
        // POST: /Book/Edit/5
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
        // GET: /Book/Delete/5
        public ActionResult Delete(int id)
        {
            return View();
        }

        //
        // POST: /Book/Delete/5
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

        public class SPSU_Class
        {
            public virtual string Prof { get; set; }
            public virtual string Course { get; set; }
            public virtual int Section { get; set; }
            public virtual string Semester { get; set; }

        }
        [AllowAnonymous]
        [HttpGet]
        public JsonResult GetClasses()
        {
            var classes = DbManager.Books.Select(x => new SPSU_Class() { Prof = x.Professor, Course = x.Course, Section = x.Section, Semester = x.Semester}).ToList();
            return Json(classes, JsonRequestBehavior.AllowGet);

        }


    }

}