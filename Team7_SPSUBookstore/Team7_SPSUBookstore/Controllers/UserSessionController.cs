using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Entities;

namespace Team7_SPSUBookstore.Controllers
{
    public class UserSessionController : BaseController
    {
        //
        // GET: /UserSession/
        [HttpGet]
        public ActionResult Login()
        {

           

            return View();
        }


        [HttpPost]
        public ActionResult Login(string username, string password)
        {
            int attempts = (Session["attempts"] != null ? (int)Session["attempts"] + 1 : 0);
            Session["attempts"] = attempts;
            User user = DbManager.Users.Where(x => x.Email == username).FirstOrDefault();
            bool validLogin = (user != null && user.Password.ToLower() == password.ToLower());
            if (attempts >= 5 && validLogin)
            {


                DateTime jailDateTime = DateTime.Now.AddMinutes(10);

                if (Session["jailtime"] != null)
                    jailDateTime = (DateTime)Session["jailtime"];
                else
                    Session["jailtime"] = jailDateTime;

                TimeSpan jailtime = jailDateTime.Subtract(DateTime.Now);
                ModelState.AddModelError("", "You have failed to login 5 times, you can login again in " + jailtime.Minutes.ToString() + " minutes");
                return View();

            }

            // User user = DbManager.Users.Where(x => x.Email == email).FirstOrDefault();
            if (!validLogin)
            {
                ModelState.AddModelError("", "Invalid username or password.");
            }
            else
            {
                Session["isLoggedIn"] = true;
                return RedirectToAction("Index", "Home");
            }


            return View();
        }


        public ActionResult Guest(string email)
        {
            Session["isLoggedIn"] = true;
            return RedirectToAction("Index", "Home");
        }

        public ActionResult LogOff(string email)
        {
            Session["isLoggedIn"] = false;
            return RedirectToAction("Index", "Home");
        }

        



	}
}