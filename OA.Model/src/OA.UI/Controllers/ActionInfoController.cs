using System.Linq;
using Microsoft.AspNetCore.Mvc;
using OA.IService;
using OA.Service;
using OA.Model.Enum;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Hosting;
using System;
using System.IO;
using OA.Model;
using System.Collections.Generic;


// For more information on enabling MVC for empty projects, visit http://go.microsoft.com/fwlink/?LinkID=397860

namespace OA.UI.Controllers
{
    public class ActionInfoController : BaseController
    {
        private IActionInfoService asf = new ActionInfoService();
        private IRoleInfoService rs = new RoleInfoService();
        private IRoleInfoActionInfoService ras = new RoleInfoActionInfoService();

        private IHostingEnvironment _environment;

        public ActionInfoController(IHostingEnvironment environment)
        {
            //upload config
            _environment = environment;
        }

        // GET: /<controller>/
        public IActionResult Index()
        {
            return View();
        }

        #region GetActionInfo
        public IActionResult GetActionInfo()
        {
            // get page index and size info
            int pageIndex = int.Parse(Request.Form["page"]);
            int pageSize = int.Parse(Request.Form["rows"]);
            int totalcount = 0;
            short delflag = (short)DeleteEnumType.Normal;

            // get role info list from database.
            var actionInfoList = asf.GetPageList<int>(r => r.DelFlag == delflag, r => r.Id, pageIndex, pageSize, out totalcount, true);

            var rows = from r in actionInfoList
                       select new
                       {
                           ID = r.Id,
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
        public IActionResult GetMenuIcon()
        {
            // get path of upload file to save MenuIcon.
            var uploads = Path.Combine(_environment.WebRootPath, "uploads");
            // get upload file.
            IFormFile file = Request.Form.Files.GetFile("Photo");

            // String fileName = Path.GetFileName();
            String fileExt = Path.GetExtension(file.FileName);

            // check Extensions.
            if (fileExt == ".jpg")
            {
                // using Guid.NewGuid().ToString() to create new file name.
                String newFileName = Guid.NewGuid().ToString() +　fileExt;
                using (var fileStream = new FileStream(Path.Combine(uploads, newFileName), FileMode.Create))
                {
                    file.CopyTo(fileStream);         
                }

                return Content(@"|ok:|" + "/uploads/" +  newFileName.ToString() + "|");
            }

            return Content("no:");
        }
        #endregion

        #region Add Action Info
        public IActionResult AddActionInfo(ActionInfo actionInfo)
        {
            // modify and check actionInfo.
            actionInfo.DelFlag = 0;
            actionInfo.ModifiedOn = DateTime.Now.ToString();
            actionInfo.SubTime = DateTime.Now;
            actionInfo.Url = actionInfo.Url.ToLower();
            actionInfo.MenuIcon = Request.Form["MenuIcon"];
            actionInfo.HttpMethod = actionInfo.HttpMethod.ToLower();

            // add action.
            if (asf.Add(actionInfo))
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
        public IActionResult DeleteActionInfo()
        {
            // get list of action' id that need to delete.
            String strIds = Request.Form["strId"];

            String[] ids = strIds.Split(',');

            List<int> deleteIds = GetDeleteId(ids);

            // whether delete successfully.
            if (asf.DeleteEntities(deleteIds))
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
        public IActionResult ShowEditActionInfo(int id)
        {
            // get actionInfo by id and pass to model.
            ViewData.Model =  asf.GetList(a => a.Id == id).FirstOrDefault();

            // return.
            return View();
        }
        #endregion

        #region Edit ActionInfo
        public IActionResult EditActionInfo(ActionInfo actionInfo)
        {
            actionInfo.ModifiedOn = DateTime.Now.ToString();

            if (asf.Edit(actionInfo))
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
        public IActionResult SetActionRole(int id)
        {
            // get permission.
            var actionInfo = asf.GetList(a => a.Id == id).FirstOrDefault();

            //
            ViewBag.ActionInfo = actionInfo;

            // get all role.
            short delFlag = (short)DeleteEnumType.Normal;
            ViewBag.allRoles = rs.GetList(r => r.DelFlag == delFlag).ToList();

            // get all exist role id.

            var temp = ras.GetList(al => al.ActionInfoId == actionInfo.Id);

            List<int> list = new List<int>();
            foreach (var item in temp)
            {
                list.Add(item.RoleInfoId);
            }

            ViewBag.AllExtRoleIds = list;

            return View();
        }

        [HttpPost]
        public IActionResult SetActionRole(FormCollection collection)
        {
            // get actionInfo id
            int actionId = int.Parse(Request.Form["actionId"]);

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
            if (asf.SetActionRole(actionId, roleIdList))
            {
                return Content("ok");
            }
            else
            {
                return Content("NO");
            }
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
