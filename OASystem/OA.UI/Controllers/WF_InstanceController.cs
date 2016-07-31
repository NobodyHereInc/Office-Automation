using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using OA.Model;
using OA.IService;
using OA.WorkFlow;

namespace OA.UI.Controllers
{
    public class WF_InstanceController : BaseController
    {
        private IWF_TempService wF_TempService { get; set; }
        private IUserInfoService userInfoService { get; set; }
        private IWF_InstanceService wF_InstanceService { get; set; }
        private IWF_StepInfoService wF_StepInfoService { get; set; }

        #region Show temp list
        // GET: WF_Instance
        public ActionResult Index()
        {
            // get delete flag.
            short DelFlag = (short)OA.Model.Enum.DeleteEnumType.Normal;

            ViewData.Model = wF_TempService.GetList(w => w.DelFlag == DelFlag);

            return View("index");
        }
        #endregion

        #region Start WorkFlow
        public ActionResult StartWorkFlow(int id)
        {
            // get delete flag.
            short DelFlag = (short)OA.Model.Enum.DeleteEnumType.Normal;

            // get temp info.
            var currentTemp = wF_TempService.GetList(t => t.ID == id).FirstOrDefault();

            // pass currentTemp to view page.
            ViewBag.Temp = currentTemp;

            var userInfoList = from u in userInfoService.GetList(u => u.DelFlag == 0).ToList()
                               select new SelectListItem()
                               {
                                   Selected = false,
                                   Text = u.UName,
                                   Value = u.ID.ToString()
                               };

            // if name of ViewData is the same name of DropDownList will auto fill in.
            ViewData["FlowTo"] = userInfoList; 

            // return.
            return View();
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="id"> wf_temp's id </param>
        /// <param name="instance"> content of workflow. </param>
        /// <returns></returns>
        [HttpPost]
        [ValidateInput(false)]
        public ActionResult StartWorkFlow(int id, WF_Instance instance)
        {
            // 1. Insert instance of workflow into database (table: wf_instance).
            instance.ApplicationId = Guid.Empty;
            instance.Result = -1;
            instance.Status = -1;
            instance.StartedBy = LoginUser.ID;
            instance.SubTime = DateTime.Now;
            instance.WF_TempID = id;

            // add instance into database.
            wF_InstanceService.Add(instance);

            // 2. Start WorkFlow
            var dic = new Dictionary<String, object>()
            {
                {"TempBookMarkName", "Manager Approval"}
            };

            // create Workflow Application and launch workflow.
            var application = WorkFlowApplicationHelper.CreateWorkflowApplication(new FinancialActivity(), dic);
            // get id of application.
            instance.ApplicationId = application.Id;
            // update id of application to database.
            wF_InstanceService.Edit(instance);

            // 3. create step one (Start Financial Approval)
            WF_StepInfo stepInfo = new WF_StepInfo();
            stepInfo.ChildStepID = 0;
            stepInfo.Comment = string.Empty; // step one no Comment.
            stepInfo.DelFlag = 0;
            stepInfo.IsProcessed = true;
            stepInfo.IsEndStep = false;
            stepInfo.IsStartStep = true;
            stepInfo.ParentStepID = -1;
            stepInfo.ProcessBy = LoginUser.ID;
            stepInfo.ProcessTime = DateTime.Now;
            stepInfo.Remark = "Launch a workflow (Financial Approval)";
            stepInfo.SetpName = "Start Financial Approval";
            stepInfo.StepResult = (short)OA.WorkFlow.Eeum.WorkFlowStateEunm.IsPass;
            stepInfo.SubTime = DateTime.Now;
            stepInfo.Title = "Start Financial Approval";
            stepInfo.WF_InstanceID = instance.ID;

            // store step one info into database.
            wF_StepInfoService.Add(stepInfo);

            // create step two (Manager Approval)
            WF_StepInfo ManagerStepInfo = new WF_StepInfo();
            ManagerStepInfo.ChildStepID = 0;
            ManagerStepInfo.Comment = string.Empty; // step one no Comment.
            ManagerStepInfo.DelFlag = 0;
            ManagerStepInfo.IsProcessed = false;
            ManagerStepInfo.IsEndStep = false;
            ManagerStepInfo.IsStartStep = false;
            ManagerStepInfo.ParentStepID = stepInfo.ID;
            ManagerStepInfo.ProcessBy = int.Parse(Request["FlowTo"]);
            ManagerStepInfo.ProcessTime = DateTime.Now;
            ManagerStepInfo.Remark = "Manager is approvaling (Financial Approval)";
            ManagerStepInfo.SetpName = "Manager Approval";
            ManagerStepInfo.StepResult = (short)OA.WorkFlow.Eeum.WorkFlowStateEunm.untreated;
            ManagerStepInfo.SubTime = DateTime.Now;
            ManagerStepInfo.Title = "Manager Approval";
            ManagerStepInfo.WF_InstanceID = instance.ID;

            // store step two info into database.
            wF_StepInfoService.Add(ManagerStepInfo);

            // return 
            return Redirect("/WF_Instance/StartWorkflow?id=" + id);
        }
        #endregion

        #region I Need to approval workflow
        public ActionResult GetMyWorkFlows()
        {
            // get login user's id.
            int userID = LoginUser.ID;

            // get all approval that need to handle by this user.
            var stepList = wF_StepInfoService.GetList(s => s.ProcessBy == userID && s.IsProcessed == false && s.DelFlag == 0).ToList();

            var allInstance = (from s in stepList
                               select s.WF_Instance).ToList();

            return View(allInstance);
        }
        #endregion

        #region Check WorkFlow (Approval) 
        public ActionResult CheckWF(int id)
        {
            // get instance by id.
            var instance = wF_InstanceService.GetList(w => w.ID == id).FirstOrDefault();

            // Pass instance into view page.
            ViewBag.Instance = instance;

            // get step info.
            var stepInfoList = wF_StepInfoService.GetList(s => s.WF_InstanceID == instance.ID).ToList();

            // pass step info list to view page.
            ViewBag.Steps = stepInfoList;

            // get all user info.
            var userInfoList = from u in userInfoService.GetList(u => u.DelFlag == 0).ToList()
                               select new SelectListItem()
                               {
                                   Selected = false,
                                   Text = u.UName,
                                   Value = u.ID.ToString()
                               };

            //
            ViewData["ProcessBy"] = userInfoList;

            // return.
            return View();
        }
        #endregion

    }
}