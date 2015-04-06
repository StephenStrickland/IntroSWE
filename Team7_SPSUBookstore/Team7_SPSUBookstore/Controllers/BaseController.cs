using BookstoreDataSource;
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


        public DbManager DbManager = new DbManager("Resources/users.txt", "Resources/books.txt");
	}
}