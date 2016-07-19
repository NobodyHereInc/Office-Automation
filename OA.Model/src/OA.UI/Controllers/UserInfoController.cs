using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.AspNetCore.Mvc;
using OA.Model;
using OA.IService;
using OA.Service;
using OA.Model.Enum;
using OA.Model.SearchParams;
using Microsoft.AspNetCore.Http;

// For more information on enabling MVC for empty projects, visit http://go.microsoft.com/fwlink/?LinkID=397860

namespace OA.UI.Controllers
{
    public class UserInfoController : BaseController
    {
        private IUserInfoService us = new UserInfoService();
        private IRoleInfoService rs = new RoleInfoService();
        private IUserInfoRoleInfoService urs = new UserInfoRoleInfoService();
        private IRUserInfoActionInfoService ras = new RUserInfoActionInfoService();
        private IActionInfoService asf = new ActionInfoService();

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
            var pagesize = int.Parse(Request.Form["rows"]); // page size.
            int total = 0; // total record.

            String name = Request.Form["name"]; // get username for search.
            String remark = Request.Form["remark"]; // get remark for search.

            // fill search filter.
            UserInfoFilter userInfoFilter = new UserInfoFilter()
            {
                Uname = name,
                PageIndex = pageIndex,
                PageSize = pagesize,
                TotalCount = total,
                Uremark = remark
            };

            #region Old Version
            ////delete enum.
            //short deleteType = (short)DeleteEnumType.Normal;
            ////get total record.
            //var UserInfoList = us.GetPageList<String>(c => c.DelFlag == deleteType, c => c.Sort, pageIndex, pagesize, out total, true);
            #endregion

        // new version for search User info.
        var UserInfoList = us.LoadSearchUserInfo(userInfoFilter);


        // select info.
        var temp = from u in UserInfoList
                       select new {
                           UserId = u.Id,
                           UserName = u.Uname,
                           UPwd = u.Upwd, 
                           Remark = u.Remark,
                           SubTime = u.SubTime,
                           Sort = u.Sort,
                           DelFlag = u.DelFlag
                       };

            temp = temp.Where(u => u.DelFlag != 1);

            // return Json.
            return Json(new {rows = temp, total = userInfoFilter.TotalCount});
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

        #region ShowEditUserInfo
        public IActionResult ShowEditUserInfo(int id)
        {
            UserInfo userInfo = us.GetList(u => u.Id == id).FirstOrDefault();
            //UserInfo userInfo = us.GetById(id);

            ViewData.Model = userInfo;

            return View("ShowEditUserInfo");
        }

        #endregion

        #region Tool Functions
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
        #endregion

        #region Set Role Info
        public IActionResult SetRoleInfo(int id)
        {
            // get user info with sepcific id.
            UserInfo user = us.GetList(u => u.Id == id).FirstOrDefault();

            // set value to view page.
            ViewBag.UserInfo = user;

            // delete flag.
            short deleteflag = (short)DeleteEnumType.Normal;

            // get all roleInfo from database.
            ViewBag.AllRoles = rs.GetList(r => r.DelFlag == deleteflag).ToList();

            // get exist RoleInfo id for this user.
            var temp = urs.GetList(ur => ur.UserInfoId == user.Id);

            List<int> list = new List<int>();
            foreach (var item in temp)
            {
                list.Add(item.RoleInfoId);
            }

            ViewBag.ExtAllRoleIds = list;

            return View();
        }
        #endregion

        #region Set User Role Info
        [HttpPost]
        public IActionResult SetUserRoleInfo(FormCollection collection)
        {
            // get user id.
            int userId = int.Parse(Request.Form["userId"]);
            // get a string contains all role's id.
            String keysStr = Request.Form["roleId"];
            // split role's id String.
            String[] AllKeys = keysStr.Split(',');
            List<int> roleIdList = new List<int>();

            foreach (String key in AllKeys)
            {
                roleIdList.Add(int.Parse(key));
            }

            // set role(s) to this user.
            if (us.SetUserRole(userId, roleIdList))
            {
                return Content("ok");
            }
            else
            {
                return Content("NO");
            }           
        }
        #endregion

        #region Set User Action Info
        public IActionResult SetUserActionInfo(int id)
        {
            // get this user info.
           ViewBag.userInfo = us.GetList(u => u.Id == id).FirstOrDefault();

            // get all action.
            ViewBag.allAction = asf.GetList(a => a.DelFlag == 0).ToList();

            // get all exist action.
            ViewBag.allExtAction = ras.GetList(r => r.UserInfoId == userInfo.Id).ToList();

            return View();
        }
        #endregion

    }
}
