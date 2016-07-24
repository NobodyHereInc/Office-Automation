using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace OA.UI.Controllers
{
    public class ExceptionTestController : Controller
    {
        // GET: ExceptionTest
        public ActionResult Index()
        {
            int a = 2;
            int b = 0;

            int c = a / b;
            return Content(c.ToString());
        }
    }
}