using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using OA.Model;
using OA.IService;
using OA.Model.Enum;

namespace OA.UI.Controllers
{
    public class ActionInfoController : BaseController
    {
        IActionInfoService actionInfoService { get; set; }
        IRoleInfoService roleInfoService { get; set; }

        // GET: ActionInfo
        public ActionResult Index()
        {
            return View();
        }

        #region GetActionInfo
        public ActionResult GetActionInfo()
        {
            // get page index and size info
            int pageIndex = int.Parse(Request.Form["page"]);
            int pageSize = int.Parse(Request.Form["rows"]);
            int totalcount = 0;
            short delflag = (short)DeleteEnumType.Normal;

            // get role info list from database.
            var actionInfoList = actionInfoService.GetPageList<int>(r => r.DelFlag == delflag, r => r.ID, pageIndex, pageSize, out totalcount, true);

            var rows = from r in actionInfoList
                       select new
                       {
                           ID = r.ID,
                           actionInfoName = r.ActionInfoName,
                           sort = r.Sort,
                           subTime = r.SubTime,
                           remark = r.Remark,
                           httpMethod = r.HttpMethod,
                           actionTypeEnum = r.ActionTypeEnum,
                           url = r.Url,

                       };
            // return json object.
            return Json(new { rows = rows, total = totalcount });
        }
        #endregion

        #region Get Menu Icon
        public ActionResult GetMenuIcon()
        {
            // get icon file.
            HttpPostedFileBase file = Request.Files[0];
            // get image name.
            string fileName = System.IO.Path.GetFileName(file.FileName);
            // get image extented.
            string fileExt = System.IO.Path.GetExtension(fileName);
            // check file whether is a image file.
            if (fileExt == ".jpg")
            {
                string newfileName = Guid.NewGuid().ToString() + fileExt;
                file.SaveAs(Server.MapPath("/Content/uploads/" + newfileName));
                return Content("ok:/Content/uploads/" + newfileName);

            }
            else
            {
                return Content("no:");
            }
        }
        #endregion

        #region Add Action Info
        public ActionResult AddActionInfo(ActionInfo actionInfo)
        {
            // modify and check actionInfo.
            actionInfo.DelFlag = 0;
            actionInfo.ModifiedOn = DateTime.Now.ToString();
            actionInfo.SubTime = DateTime.Now;
            actionInfo.Url = actionInfo.Url.ToLower();
            actionInfo.MenuIcon = Request.Form["MenuIcon"];
            actionInfo.HttpMethod = actionInfo.HttpMethod.ToLower();

            // add action.
            if (actionInfoService.Add(actionInfo))
            {
                return Content("Ok");
            }
            else
            {
                return Content("No");
            }

        }
        #endregion

        #region Delete Action Info
        public ActionResult DeleteActionInfo()
        {
            // get list of action' id that need to delete.
            String strIds = Request.Form["strId"];

            String[] ids = strIds.Split(',');

            List<int> deleteIds = GetDeleteId(ids);

            // whether delete successfully.
            if (actionInfoService.DeleteEntities(deleteIds))
            {
                return Content("Ok");
            }
            else // otherwise.
            {
                return Content("No");
            }
        }
        #endregion

        #region Show Edit ActionInfo
        public ActionResult ShowEditActionInfo(int id)
        {
            //get actionInfo by id and pass to model.
            ViewData.Model = actionInfoService.GetList(a => a.ID == id).FirstOrDefault();

            // return.
            return View();
        }
        #endregion

        #region Edit ActionInfo
        public ActionResult EditActionInfo(ActionInfo actionInfo)
        {
            actionInfo.ModifiedOn = DateTime.Now.ToString();

            if (actionInfoService.Edit(actionInfo))
            {
                return Content("ok");
            }
            else
            {
                return Content("no");
            }
        }
        #endregion

        #region Set Action to Role
        public ActionResult SetActionRole(int id)
        {
            // get user info by id.
            var actionInfo = actionInfoService.GetList(a => a.ID == id).FirstOrDefault();
            // pass user info into view page.
            ViewBag.ActionInfo = actionInfo;
            // get DeleteEnumType.
            short DelFlag = (short)DeleteEnumType.Normal;
            // get all role info.
            ViewBag.AllRoles = roleInfoService.GetList(r => r.DelFlag == DelFlag).ToList();
            // get this user have exist role info.
            ViewBag.AllExtRoleIds = (from r in actionInfo.RoleInfoes
                                     select r.ID).ToList();
            return View();
        }

        [HttpPost]
        public ActionResult SetActionRole(FormCollection collection)
        {
            int actionId = int.Parse(Request["actionId"]);
            string[] AllKeys = Request.Form.AllKeys;
            List<int> list = new List<int>();
            foreach (string key in AllKeys)
            {
                if (key.StartsWith("cba_"))
                {
                    string k = key.Replace("cba_", "");
                    list.Add(int.Parse(k));
                }
            }
            actionInfoService.SetActionRole(actionId, list);
            return Content("ok");
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
    }
}