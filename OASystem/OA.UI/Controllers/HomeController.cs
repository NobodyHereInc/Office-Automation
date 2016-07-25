using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using OA.Model.ActionEqualityCompare;
using OA.Model.Enum;
using OA.IService;

namespace OA.UI.Controllers
{
    public class HomeController : BaseController
    {
        private IUserInfoService userInfoService { get; set; }
        public ActionResult Index()
        {
            if (LoginUser != null)
            {
                ViewData["UserName"] = LoginUser.UName;
                ViewData["Date"] = DateTime.Now.ToString("yyyy-MM-dd");
            }

            return View();
        }

        public ActionResult About()
        {
            ViewBag.Message = "Your application description page.";

            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }

        #region log out
        public ActionResult LogOut()
        {
            if (Request.Cookies["sessionId"] != null)
            {
                string key = Request.Cookies["sessionId"].Value;
                Common.MemcacheHelper.Delete(key);
                Response.Cookies["cp1"].Expires = DateTime.Now.AddDays(-1);
                Response.Cookies["cp2"].Expires = DateTime.Now.AddDays(-1);
            }
            return Redirect("/Login/Index");
        }
        #endregion

        #region Get Menu Itmes
        public ActionResult GetMenuItmes()
        {
            //1. get user's roles.
            var userInfo = userInfoService.GetList(u => u.ID == LoginUser.ID).FirstOrDefault();
            var userRoles = userInfo.RoleInfoes;
            //2. get all permisstion which user have.
            short menuType = (short)ActionTypeEnum.MenuActionType;
            var userMenuItem = (from r in userRoles
                                from a in r.ActionInfoes
                                where a.ActionTypeEnum == menuType
                                select a).ToList();//6
            //3. get user special permisstion.
            var userActions = userInfo.R_UserInfo_ActionInfo.ToList();
            //4. find out userActions allow permission.
            var isPassUserActions = from a in userActions
                                    where a.IsPass == true && a.ActionInfo.ActionTypeEnum == menuType
                                    select a;

            var isPassActions = (from a in isPassUserActions
                                 select a.ActionInfo).ToList();

            userMenuItem.AddRange(isPassActions);//合并两个集合.
            // find out userActions Nnot allow permission.
            var isNotPassUserActions = (from a in userActions
                                        where a.IsPass == false
                                        select a.ActionInfoID).ToList();
            //
            userMenuItem = userMenuItem.Where(a => !isNotPassUserActions.Contains(a.ID)).ToList();

            // get away Distinct of permission.
            List<Model.ActionInfo> Result = userMenuItem.Distinct().ToList();
            JsonResult jsonResult = null;
            try
            {
                var result = from u in Result
                             select new
                             {
                                 icon = u.MenuIcon,
                                 title = u.ActionInfoName,
                                 url = u.Url
                             };


                jsonResult = Json(result, JsonRequestBehavior.AllowGet);
            }
            catch
            {

            }
            return jsonResult;

        }
        #endregion
    }
}