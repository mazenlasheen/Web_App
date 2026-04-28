using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using namespace WEB_APPLICATION.Models;


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
        public ActionResult Registration(string userName , string password , string role , string firstName , string lastName   )
        {
            // checking for valid credentials 
            boolean valid = User.checkValidCredentials(userName , password ) ;
            if (!valid) 
            {
                ViewBag.Error = "The entered credentials are not vaild !"
                return View()
            }
            User.Role  userRole = UtilityDal.parseRole(role.lower());

            boolean success =  UserDAL.registerUser(userName , password, userRole , firstName , lastName )
            if (success ) 
            {
                TempData["success"] = "You have successfullly Registered into Edu Nest " ;
                return
            }
        }
    }
}