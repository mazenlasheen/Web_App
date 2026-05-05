using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using WEB_APPLICATION.Models;


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
            UserDAL userDal = new UserDAL() ; 
            
            User.Role  userRole = UtilityDAL.parseStringToRole(role.ToLower());
            bool valid = userDal.checkValidCredentials(userName , password ) ;
           
            if (!valid  ) // check if credentials are valid first ; 
            {
                ViewBag.Error = "The entered credentials are not vaild !";
                return View();
            } 
            else
            { // then attempt to register 
                bool success =  userDal.registerUser(userName , password, userRole , firstName , lastName ) ;
                if (success )
                {
                    TempData["success"] = "You have successfullly Registered into Edu Nest " ;
                    return RedirectToAction("Login","Account") ; // redirects it to the login page 
                } 
                else
                {
                    ViewBag.Error = "An issue occured while Attempting registration " ;     
                    return View() ; // returns to the registration page 
                }                
            }  
        }
        
        [HttpGet]
        public ActionResult Login()
        {
            return View() ; // this basically goes to Views/Account/Login.cshtml and then gets the html code 
        }
    }
}
    
    
    
