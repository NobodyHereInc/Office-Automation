using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;
using OA.IService;
using OA.Model;
using OA.Model.SearchParam;
using OA.Model.Enum;

namespace OA.UI.Controllers
{
    public class UserInfoController : BaseController
    {
        private IUserInfoService userInfoService { get; set; }
        private IRoleInfoService roleInfoService { get; set; }
        private IActionInfoService actionInfoService { get; set; }
        private IR_UserInfo_ActionInfoService r_UserInfo_ActionInfoService { get; set; }

        // GET: /<controller>/
        public ActionResult Index()
        {
            ViewData.Model = userInfoService.GetList(u => true);
            return View("index");
        }

        #region Get all User Information.
        public ActionResult GetUserInfo()
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
            var UserInfoList = userInfoService.LoadSearchUserInfo(userInfoFilter);


            // select info.
            var temp = from u in UserInfoList
                       select new
                       {
                           UserId = u.ID,
                           UserName = u.UName,
                           UPwd = u.UPwd,
                           Remark = u.Remark,
                           SubTime = u.SubTime,
                           Sort = u.Sort,
                           DelFlag = u.DelFlag
                       };

            temp = temp.Where(u => u.DelFlag != 1);

            // return Json.
            return Json(new { rows = temp, total = userInfoFilter.TotalCount }, JsonRequestBehavior.AllowGet);
        }
        #endregion

        #region Delete User Information
        public ActionResult DeleteUserInfo()
        {
            // get delete ids String.
            String strId = Request.Form["strId"];

            // split strID string.
            String[] strIds = strId.Split(',');

            // calling deleteIds method to get int list for delete Id.
            List<int> deleteIds = GetDeleteId(strIds);

            // whether delete successfully.
            if (userInfoService.DeleteEntities(deleteIds))
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
        public ActionResult AddUserInfo(UserInfo user)
        {
            user.SubTime = DateTime.Now;
            user.ModifiedOn = DateTime.Now;
            user.Sort = "0";
            user.DelFlag = 0;

            userInfoService.Add(user);
            return Content("ok");

        }
        #endregion

        #region Update User Information
        public ActionResult EditUserInfo(UserInfo user)
        {
            // add new info.
            user.ModifiedOn = DateTime.Now;

            // save update to database.
            if (userInfoService.Edit(user))
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
        public ActionResult ShowEditUserInfo(int id)
        {
            UserInfo userInfo = userInfoService.GetList(u => u.ID == id).FirstOrDefault();
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
        public ActionResult SetRoleInfo(int id)
        {
            // get user info with sepcific id.
            UserInfo user = userInfoService.GetList(u => u.ID == id).FirstOrDefault();

            // set value to view page.
            ViewBag.UserInfo = user;

            // delete flag.
            short deleteflag = (short)DeleteEnumType.Normal;

            // get all roleInfo from database.
            ViewBag.AllRoles = roleInfoService.GetList(r => r.DelFlag == deleteflag).ToList();

            // get exist RoleInfo id for this user.
            ViewBag.ExtAllRoleIds = (from r in user.RoleInfoes
                                     select r.ID).ToList();

            return View();
        }
        #endregion

        #region Set User Role Info
        [HttpPost]
        public ActionResult SetUserRoleInfo(FormCollection collection)
        {
            // get user id.
            int userId = int.Parse(Request["userId"]);
            // get all value of name in form.
            string[] AllKeys = Request.Form.AllKeys;
            // initiate a list.
            List<int> list = new List<int>();
            foreach (string key in AllKeys)
            {
                if (key.StartsWith("cba_"))
                {
                    string roleId = key.Replace("cba_", "");
                    list.Add(int.Parse(roleId));
                }
            }

            // get role for this user.
            userInfoService.SetUserRole(userId, list);

            // return.
            return Content("ok");
        }
        #endregion

        #region Set User Action Info
        public ActionResult SetUserActionInfo(int id)
        {
            // get user info by id.
            int userId = int.Parse(Request["id"]);
            var userInfo = userInfoService.GetList(u => u.ID == userId).FirstOrDefault();
            // pass user info to view page.
            ViewData.Model = userInfo;
            ViewBag.UserInfo = userInfo;
            // get all action info and pass to view page.
            ViewBag.AllActions = actionInfoService.GetList(a => a.DelFlag == 0).ToList();
            // get user have action info (include allow and now allow).
            ViewBag.AllExtActions = userInfo.R_UserInfo_ActionInfo.ToList();

            // return.
            return View();
        }

        public ActionResult SetActionForUser()
        {
            // get user id.
            int userId = int.Parse(Request["userId"]);
            // get action ids.
            int actionId = int.Parse(Request["actionId"]);
            // get all not allow.
            string value = Request["value"];

            bool isPass = value == "true" ? true : false;

            if (userInfoService.SetUserAction(userId, actionId, isPass))
            {
                return Content("ok");
            }
            else
            {
                return Content("no");
            }
        }
        #endregion

        #region Clear Action User
        public ActionResult ClearActionUser(int userId, int actionId)
        {
            var actionInfo = r_UserInfo_ActionInfoService.GetList(r => r.UserInfoID == userId && r.ActionInfoID == actionId).FirstOrDefault();
            if (actionInfo != null)
            {
                r_UserInfo_ActionInfoService.Remove(actionInfo);
            }
            return Content("ok");
        }
        #endregion
    }
}