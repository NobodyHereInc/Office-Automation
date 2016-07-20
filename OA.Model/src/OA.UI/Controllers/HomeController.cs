using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using OA.DAL;
using OA.Model;
using OA.Service;
using OA.IService;
using Microsoft.AspNetCore.Http;
using OA.Model.Enum;

namespace OA.UI.Controllers
{
    public class HomeController : BaseController
    {
        private IUserInfoRoleInfoService ur = new UserInfoRoleInfoService();
        private IRoleInfoActionInfoService ra = new RoleInfoActionInfoService();
        private IActionInfoService a = new ActionInfoService();
        private IRUserInfoActionInfoService rua = new RUserInfoActionInfoService();

        public IActionResult Index()
        {
            if (userInfo != null)
            {
                ViewData["UserName"] = userInfo.Uname;
                ViewData["Date"] = DateTime.Now.ToString("yyyy-MM-dd");
            }

            return View();
        }

        #region LogOut
        public IActionResult LogOut()
        {
            // clear session info.
            HttpContext.Session.SetString("Uid", "");

            return Redirect("/Login/index");
        }
        #endregion

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

        #region Get Menu Itmes
        public IActionResult GetMenuItmes()
        {
            // 1. get this user is exist role.
            int id = userInfo.Id;

            // 2. get all permission for this user from User_role_action table.
            var extRole = ur.GetList(u => u.UserInfoId == id);

            List<ActionInfo> list = new List<ActionInfo>();

            short menuType = (short)ActionTypeEnum.MenuActionType;

            foreach (var role in extRole)
            {
                var action = ra.GetList(r => r.RoleInfoId == role.RoleInfoId).FirstOrDefault();
                var actionInfo = a.GetList(act => act.Id == action.ActionInfoId).FirstOrDefault();

                if (null != action && actionInfo.ActionTypeEnum == menuType)
                {
                    list.Add(actionInfo);
                }
            }

            // 3. get special Permissin from R_User_action table.
            var temp = rua.GetList(ra => ra.UserInfoId == id && ra.IsPass == true);

            foreach (var item in temp)
            {
                var actionInfoR = a.GetList(act => act.Id == item.ActionInfoId).FirstOrDefault();

                if (actionInfoR != null && actionInfoR.ActionTypeEnum == menuType)
                {
                    list.Add(actionInfoR);
                }
            }

            // get not allow permission
            var isNotAllow = rua.GetList(ra => ra.UserInfoId == id && ra.IsPass == false).ToList();

            // if special permission is not allow.
            foreach (var notAllow in isNotAllow)
            {
                var actionInfoR = a.GetList(act => act.Id == notAllow.ActionInfoId).FirstOrDefault();

                if (list.Contains(actionInfoR))
                {
                    list.Remove(actionInfoR);
                }
            }

            // Remove duplicates from a List.
            List<ActionInfo> Result = list.Distinct().ToList();

            var finalresult = from u in Result
                              select new
                              {
                                  icon = u.MenuIcon,
                                  title = u.ActionInfoName,
                                  url = u.Url
                              };

            // return final result
            return Json(finalresult);
        }
        #endregion
    }
}
