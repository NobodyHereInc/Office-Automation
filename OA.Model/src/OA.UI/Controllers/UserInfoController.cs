using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using OA.Model;
using OA.IService;
using OA.Service;
using OA.Model.Enum;
using Microsoft.AspNetCore.Mvc.Formatters.Json;

// For more information on enabling MVC for empty projects, visit http://go.microsoft.com/fwlink/?LinkID=397860

namespace OA.UI.Controllers
{
    public class UserInfoController : Controller
    {
        IUserInfoService us = new UserInfoService();

        // GET: /<controller>/
        public IActionResult Index()
        {
            ViewData.Model = us.GetList(u => true);
            return View("index");
        }

        #region Get all User Information.
        public IActionResult GetUserInfo()
        {
            int pageIndex = int.Parse(Request.Form["page"]); // page index.
            int pagesize = int.Parse(Request.Form["rows"]); // page size.
            int total; // total record.

            // delete enum.
            short deleteType = (short)DeleteEnumType.Normal;

            // get total record.
            var UserInfoList = us.GetPageList<String>(c => c.DelFlag == deleteType, c => c.Sort, pageIndex, pagesize, out total, true);

            // select info.
            var temp = from u in UserInfoList
                       select new {
                           UserId = u.UserId,
                           UserName = u.UserName,
                           UPwd = u.UPwd,
                           Remark = u.Remark,
                           SubTime = u.SubTime,
                           Sort = u.Sort,
                           DelFlag = u.DelFlag
                       };

            // return Json.
            return Json(new {rows = temp, total = total});
        }
        #endregion

        #region Delete User Information
        public IActionResult DeleteUserInfo()
        {
            // get delete ids String.
            String strId = Request.Form["strId"];

            // split strID string.
            String[] strIds = strId.Split(',');

            // calling deleteIds method to get int list for delete Id.
            List<int> deleteIds = GetDeleteId(strIds);

            // whether delete successfully.
            if (us.DeleteEntities(deleteIds))
            {
                return Content("Ok");
            }
            else // otherwise.
            {
                return Content("No");
            }
        }
        #endregion

        #region Add User Info
        public IActionResult AddUserInfo(UserInfo user)
        {
            user.SubTime = DateTime.Now;
            user.ModifiedOn = DateTime.Now;
            user.Sort = "0";
            user.DelFlag = 0;

            us.Add(user);
            return Content("ok");

        }
        #endregion

        #region Update User Information
        public IActionResult EditUserInfo(UserInfo user)
        {
            // add new info.
            user.ModifiedOn = DateTime.Now;

            // save update to database.
            if (us.Edit(user))
            {
                return Content("ok");
            }
            else
            {
                return Content("no");
            }


            
        }
        #endregion

        /// <summary>
        /// This Function is used to convert string array to int list.
        /// </summary>
        /// <param name="strIds">input string array.</param>
        /// <returns>int list.</returns>
        public List<int> GetDeleteId(String[] strIds)
        {
            // resutl in list.
            List<int> result = new List<int>();

            // convert each string to int.
            foreach (String item in strIds)
            {
                result.Add(int.Parse(item));
            }

            // return  int list.
            return result;
        }
    }
}
