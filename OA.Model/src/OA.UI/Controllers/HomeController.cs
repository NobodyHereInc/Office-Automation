using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using OA.DAL;
using OA.Model;

namespace OA.UI.Controllers
{
    public class HomeController : Controller //BaseController
    {
        public IActionResult Index()
        {
            //if (userInfo != null)
            //{
            //    ViewData["UserName"] = userInfo.UserName;
            //}
            
            return View();
        }

        public IActionResult HomePage()
        {
            ViewData["Date"] = DateTime.Now.ToString("yyyy-MM-dd");
            return View();
        }

        public IActionResult About()
        {
            ViewData["Message"] = "Your application description page.";

            return View();
        }

        public IActionResult Contact()
        {
            ViewData["Message"] = "Your contact page.";

            return View();
        }

        public IActionResult Error()
        {
            return View("Error");
        }
    }
}
