using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;

// For more information on enabling MVC for empty projects, visit http://go.microsoft.com/fwlink/?LinkID=397860

namespace OA.UI.Controllers
{
    public class TestController : Controller
    {
        // GET: /<controller>/
        public IActionResult Index()
        {
            int a = 2;
            int b = 0;
            int c = 0;

            try
            {
                c = a / b;            
            }
            catch (Exception)
            {
                Response.Redirect("Home/Error");
            }

            return Content(c.ToString());
        }
    }
}
