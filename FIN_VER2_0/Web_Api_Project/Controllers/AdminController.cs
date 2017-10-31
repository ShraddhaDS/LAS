using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Model;
using BLL;

namespace Web_Api_Project.Controllers
{
    public class AdminController : Controller
    {

        public ActionResult UserList()
        {           
            return View();
        }

       

    }
}
