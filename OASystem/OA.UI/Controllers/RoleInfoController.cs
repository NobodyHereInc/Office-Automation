using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using OA.Model;
using OA.Model.Enum;
using OA.IService;

namespace OA.UI.Controllers
{
    public class RoleInfoController : BaseController
    {
        IRoleInfoService roleInfoService { get; set; }

        // GET: RoleInfo
        public ActionResult Index()
        {
            return View();
        }

        #region GetRoleInfor
        /// <summary>
        /// This action is used to display all role info.
        /// </summary>
        /// <returns></returns>
        public ActionResult GetRoleInfo()
        {
            // get page index and size info
            int pageIndex = int.Parse(Request.Form["page"]);
            int pageSize = int.Parse(Request.Form["rows"]);
            int totalcount = 0;
            short delflag = (short)DeleteEnumType.Normal;

            // get role info list from database.
            var roleInfoList = roleInfoService.GetPageList<int>(r => r.DelFlag == delflag, r => r.ID, pageIndex, pageSize, out totalcount, true);

            var rows = from r in roleInfoList
                       select new
                       {
                           ID = r.ID,
                           RoleName = r.RoleName,
                           Sort = r.Sort,
                           Remark = r.Remark,
                           SubTime = r.SubTime
                       };
            // return json object.
            return Json(new { rows = rows, total = totalcount });
        }
        #endregion

        #region Add New Role Info
        /// <summary>
        /// This action is used to add new role info into database.
        /// </summary>
        /// <param name="role"></param>
        /// <returns></returns>
        public ActionResult AddRoleInfo(RoleInfo role)
        {
            // add missing info for roleInfo.
            role.DelFlag = 0;
            role.SubTime = DateTime.Now;
            role.ModifiedOn = DateTime.Now;

            // add new roleInfo into database.
            roleInfoService.Add(role);

            // return ok.
            return Content("ok");
        }
        #endregion

        #region Delete RoleInfo
        /// <summary>
        /// This action is used to delete batch of roleInfo.
        /// </summary>
        /// <returns></returns>
        public ActionResult DeleteRoleInfo()
        {
            // get delete ids String.
            String strId = Request.Form["strId"];

            // split strID string.
            String[] strIds = strId.Split(',');

            //
            List<int> list = new List<int>();

            // convert string to int (ids)
            foreach (String item in strIds)
            {
                list.Add(int.Parse(item));
            }

            // delete those roleInfo.
            if (true == roleInfoService.DeleteEntities(list))
            {
                return Content("ok");
            }
            else
            {
                return Content("no");
            }

        }
        #endregion

        #region ShowEditRoleInfo
        public ActionResult ShowEditRoleInfo(int id)
        {
            // role that need to update.
            ViewData.Model = roleInfoService.GetList(r => r.ID == id).FirstOrDefault();

            //display info of role
            return View("ShowEditRoleInfo");
        }
        #endregion

        #region Edit RoleInfo
        public ActionResult EditRoleInfo(RoleInfo role)
        {
            role.ModifiedOn = DateTime.Now;

            if (roleInfoService.Edit(role))
            {
                return Content("ok");
            }
            else
            {
                return Content("no");
            }
        }
        #endregion
    }
}