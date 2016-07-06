using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using OA.Model;
using OA.Service;

// For more information on enabling MVC for empty projects, visit http://go.microsoft.com/fwlink/?LinkID=397860

namespace OA.UI.Controllers
{
    public class UserInfoController : Controller
    {
        private UserInfoService us = new UserInfoService();

        // GET: /<controller>/
        public IActionResult Index()
        {
            ViewData.Model = us.GetList(u => true);

            return View("index");
        }
    }
}
