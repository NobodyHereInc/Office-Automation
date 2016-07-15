using System;
using Microsoft.AspNetCore.Mvc;
using OA.IService;
using OA.Service;
using OA.Model;
using OA.Model.Enum;
using System.Linq;
using System.Collections.Generic;

// For more information on enabling MVC for empty projects, visit http://go.microsoft.com/fwlink/?LinkID=397860

namespace OA.UI.Controllers
{
    public class RoleInfoController : BaseController
    {

        private IRoleInfoService rs = new RoleInfoService();

        // GET: /<controller>/
        public IActionResult Index()
        {
            return View();
        }

        #region GetRoleInfor
        /// <summary>
        /// This action is used to display all role info.
        /// </summary>
        /// <returns></returns>
        public IActionResult GetRoleInfo()
        {
            // get page index and size info
            int pageIndex = int.Parse(Request.Form["page"]);
            int pageSize = int.Parse(Request.Form["rows"]);
            int totalcount = 0;
            short delflag = (short)DeleteEnumType.Normal;

            // get role info list from database.
            var roleInfoList = rs.GetPageList<int>(r => r.DelFlag == delflag, r => r.Id, pageIndex, pageSize, out totalcount, true);

            var rows = from r in roleInfoList
                       select new {
                           ID = r.Id,
                           RoleName = r.RoleName,
                           Sort = r.Sort,
                           Remark = r.Remark,
                           SubTime = r.SubTime
                       };
            // return json object.
            return Json(new { rows = rows, total = totalcount});
        }
        #endregion

        #region Add New Role Info
        /// <summary>
        /// This action is used to add new role info into database.
        /// </summary>
        /// <param name="role"></param>
        /// <returns></returns>
        public IActionResult AddRoleInfo(RoleInfo role)
        {
            // add missing info for roleInfo.
            role.DelFlag = 0;
            role.SubTime = DateTime.Now;
            role.ModifiedOn = DateTime.Now;

            // add new roleInfo into database.
            rs.Add(role);

            // return ok.
            return Content("ok"); 
        }
        #endregion

        #region Delete RoleInfo
        /// <summary>
        /// This action is used to delete batch of roleInfo.
        /// </summary>
        /// <returns></returns>
        public IActionResult DeleteRoleInfo()
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
            if (true == rs.DeleteEntities(list))
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
        public IActionResult ShowEditRoleInfo(int id)
        {
            // role that need to update.
            ViewData.Model = rs.GetList(r => r.Id == id).FirstOrDefault();

            //display info of role
            return View("ShowEditRoleInfo");
        }
        #endregion

        #region Edit RoleInfo
        public IActionResult EditRoleInfo(RoleInfo role)
        {
            role.ModifiedOn = DateTime.Now;

            if (rs.Edit(role))
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
