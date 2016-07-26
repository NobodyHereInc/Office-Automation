using OA.Common;
using System;
using System.Linq;
using System.Web.Mvc;
using OA.Model;
using OA.IService;
using Spring.Context;
using Spring.Context.Support;

namespace OA.UI.Controllers
{
    public class BaseController : Controller
    {
        public UserInfo LoginUser { get; set; }

        /// <summary>
        /// protect user enter uerl in bowser. 
        /// </summary>
        /// <param name="filterContext"></param>
        protected override void OnActionExecuting(ActionExecutingContext filterContext)
        {
            bool isExt = false; // exist flag.

            if (Request.Cookies["sessionId"] != null)
            {
                // get sessionID from cookie.
                String sessionId = Request.Cookies["sessionId"].Value;

                // use sessionId get value from memcahce.
                object obj = MemcacheHelper.Get(sessionId);

                if (obj != null)
                {
                    // Deserializer To Object
                    LoginUser = SerializerHelper.DeserializerToObject<UserInfo>(obj.ToString());

                    // this user is exist.
                    isExt = true;

                    //// backdoor
                    //if (LoginUser.UName == "walter")
                    //{
                    //    return;
                    //}

                    //// permission filter.
                    //// url address.
                    //string requestUrl = Request.Url.AbsolutePath.ToLower();
                    //string requestHttpMethod = Request.HttpMethod;

                    //// 
                    //IApplicationContext ctx = ContextRegistry.GetContext(); 
                    //IUserInfoService userInfoService = (IUserInfoService)ctx.GetObject("userInfoService");
                    //IActionInfoService actionInfoService = (IActionInfoService)ctx.GetObject("actionInfoService");
                    //var currentAction = actionInfoService.GetList(a => a.Url == requestUrl && a.HttpMethod == requestHttpMethod).FirstOrDefault();//根据URL地址与请求方式找出具体的权限.
                    //if (currentAction == null)
                    //{
                    //    Response.Redirect("/Error.html");
                    //    return;
                    //}
                    //// line one
                    //var currentUserInfo = userInfoService.GetList(u => u.ID == LoginUser.ID).FirstOrDefault();//登录用户
                    //var actions = currentUserInfo.R_UserInfo_ActionInfo.Where(r => r.ActionInfoID == currentAction.ID).FirstOrDefault();//判断登录用户是否有权限
                    //if (actions != null)
                    //{
                    //    if (actions.IsPass == true)
                    //    {
                    //        return;
                    //    }
                    //    else
                    //    {
                    //        Response.Redirect("/Error.html");
                    //        return;
                    //    }
                    //}
                    //// line two
                    //var currentUserRoles = currentUserInfo.RoleInfoes;
                    //var currentUserActions = from a in currentUserRoles
                    //                         select a.ActionInfoes;
                    //var count = (from a in currentUserActions
                    //             from b in a
                    //             where b.ID == currentAction.ID
                    //             select b).Count();
                    //if (count < 1)
                    //{
                    //    Response.Redirect("/Error.html");
                    //    return;
                    //}
                }
            }

            if (isExt == false)// user do not log in.
            {
                // return to login page.
                filterContext.HttpContext.Response.Redirect("/Login/Index");
            }

            base.OnActionExecuting(filterContext);
        }
    }
}