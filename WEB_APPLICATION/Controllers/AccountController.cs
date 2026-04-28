using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace WEB_APPLICATION.Controllers
{
    public class AccountController : Controller
    {
        // GET: Account
        [HttpGet]
        public ActionResult Registration() 
        {
            return View() ; 
        }

        [HttpPost] // post 
        public ActionResult Registration(string userName , string password , string )
        {

        }
    }
}