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


// For more information on enabling MVC for empty projects, visit http://go.microsoft.com/fwlink/?LinkID=397860

namespace OA.UI.Controllers
{
    public class ActionInfoController : BaseController
    {
        private IActionInfoService asf = new ActionInfoService();

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
    }
}
