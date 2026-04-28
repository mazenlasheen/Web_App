using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace WEB_APPLICATION.Controllers
{
    public class CourseController : Controller
    {
        // GET: Course
        public ActionResult Index()
        {
            return View();
        }
    }
}