using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.AspNetCore.Http;
using OA.Model;
using OA.IService;
using OA.Service;

// For more information on enabling MVC for empty projects, visit http://go.microsoft.com/fwlink/?LinkID=397860

namespace OA.UI.Controllers
{
    /// <summary>
    /// Class Description: each controller need to inherite this class to check session.
    /// </summary>
    public class BaseController : Controller
    {
        public String userId = String.Empty;
        public UserInfo userInfo;
        public IUserInfoService us = new UserInfoService();
        /// <summary>
        /// 
        /// </summary>
        /// <param name="context"></param>
        public override void OnActionExecuting(ActionExecutingContext context)
        {
            bool isExt = false;

            userId = HttpContext.Session.GetString("Uid");

            if (!String.IsNullOrEmpty(userId))
            {
                isExt = true;
            }

            // user did not log in.
            if (isExt == false)
            {
                HttpContext.Response.Redirect("/Login/Index/");
            }
            else
            {
                userInfo = us.GetList(u => u.UserId == Convert.ToInt32(userId)).FirstOrDefault();
            }
        }
    }
}
