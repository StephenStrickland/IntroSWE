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
        public ActionResult Login(string email, string password)
        {

            User user = DbManager.Users.Where(x => x.Email == email).FirstOrDefault();
            if(user == null ||  user == default(User))
            {
                ModelState.AddModelError("", "Invalid username or password.");
            }

            return View();
        }


        public ActionResult Guest(string email)
        {
            return View();
        }



	}
}