using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using OA.Model;
using OA.IService;

namespace OA.UI.Controllers
{
    public class WF_TempController : Controller
    {

        private IWF_TempService wF_TempService { get; set; }


        // GET: WF_Temp
        public ActionResult Index()
        {
            // get delete flag.
            short DelFlag = (short)OA.Model.Enum.DeleteEnumType.Normal;

            var wf_tempList = wF_TempService.GetList(w => w.DelFlag == DelFlag).ToList();

            ViewData["list"] = wf_tempList;

            return View();
        }

        #region Create new WorkFlow Temp
        public ActionResult Add()
        {
            return View();
        }
        #endregion
    }
}