using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using OA.Service;
using OA.IService;
using OA.Model;
using OA.Model.SearchParam;

namespace OA.UI.Controllers
{
    public class UserInfoController : Controller
    {
        private IUserInfoService userInfoService { get; set; }
        //private IRoleInfoService rs = new RoleInfoService();
        //private IUserInfoRoleInfoService urs = new UserInfoRoleInfoService();
        //private IRUserInfoActionInfoService ras = new RUserInfoActionInfoService();
        //private IActionInfoService asf = new ActionInfoService();

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

        //#region Set Role Info
        //public IActionResult SetRoleInfo(int id)
        //{
        //    // get user info with sepcific id.
        //    UserInfo user = us.GetList(u => u.Id == id).FirstOrDefault();

        //    // set value to view page.
        //    ViewBag.UserInfo = user;

        //    // delete flag.
        //    short deleteflag = (short)DeleteEnumType.Normal;

        //    // get all roleInfo from database.
        //    ViewBag.AllRoles = rs.GetList(r => r.DelFlag == deleteflag).ToList();

        //    // get exist RoleInfo id for this user.
        //    var temp = urs.GetList(ur => ur.UserInfoId == user.Id);

        //    List<int> list = new List<int>();
        //    foreach (var item in temp)
        //    {
        //        list.Add(item.RoleInfoId);
        //    }

        //    ViewBag.ExtAllRoleIds = list;

        //    return View();
        //}
        //#endregion

        //#region Set User Role Info
        //[HttpPost]
        //public IActionResult SetUserRoleInfo(FormCollection collection)
        //{
        //    // get user id.
        //    int userId = int.Parse(Request.Form["userId"]);
        //    // get a string contains all role's id.
        //    String keysStr = Request.Form["roleId"];
        //    // split role's id String.
        //    String[] AllKeys = keysStr.Split(',');
        //    List<int> roleIdList = new List<int>();

        //    foreach (String key in AllKeys)
        //    {
        //        roleIdList.Add(int.Parse(key));
        //    }

        //    // set role(s) to this user.
        //    if (us.SetUserRole(userId, roleIdList))
        //    {
        //        return Content("ok");
        //    }
        //    else
        //    {
        //        return Content("NO");
        //    }
        //}
        //#endregion

        //#region Set User Action Info
        //public IActionResult SetUserActionInfo(int id)
        //{
        //    // get this user info.
        //    userInfo = us.GetList(u => u.Id == id).FirstOrDefault();
        //    ViewBag.userInfo = userInfo;
        //    ViewData.Model = userInfo;

        //    // get all action.
        //    ViewBag.allAction = asf.GetList(a => a.DelFlag == 0).ToList();

        //    // get all exist action.
        //    ViewBag.allExtAction = ras.GetList(r => r.UserInfoId == userInfo.Id).ToList();

        //    return View();
        //}

        //public ActionResult SetActionForUser()
        //{
        //    // get userId
        //    int userId = int.Parse(Request.Form["userId"]);
        //    // get actionId
        //    int actionId = int.Parse(Request.Form["actionId"]);
        //    // get isPass
        //    string value = Request.Form["value"];
        //    // convert string to bool.
        //    bool isPass = value == "true" ? true : false;
        //    // 
        //    if (ras.SetUserAction(userId, actionId, isPass))
        //    {
        //        return Content("ok");
        //    }
        //    else
        //    {
        //        return Content("no");
        //    }
        //}
        //#endregion

        //#region Clear Action User
        //public IActionResult ClearActionUser(int userId, int actionId)
        //{
        //    //// get userId
        //    //int userId = int.Parse(Request.Form["userId"]);
        //    //// get actionId
        //    //int actionId = int.Parse(Request.Form["actionId"]);

        //    // get this record.
        //    var actionInfo = ras.GetList(r => r.UserInfoId == userId && r.ActionInfoId == actionId).FirstOrDefault();

        //    if (actionInfo != null)
        //    {
        //        ras.Remove(actionInfo);
        //        return Content("ok");
        //    }
        //    else
        //    {
        //        return Content("no");
        //    }
        //}
        //#endregion
    }
}