using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using OA.IService;
using OA.Model;

namespace OA.UI.Controllers
{
    public class LogInController : Controller
    {
        private IUserInfoService userInfoService { get; set; }

        // GET: LogIn
        public ActionResult Index()
        {
            return View();
        }

        #region set Captcha
        public ActionResult ValidateCode()
        {
            // instance of ValidateCode
            Common.ValidateCode validatCode = new Common.ValidateCode();
            // get 4 digital captcha.
            String code = validatCode.CreateValidateCode(4);
            // save validatCode into session.
            Session["validateCode"] = code;
            // draw interference points and point in captcha picture.
            byte[] buffer = validatCode.CreateValidateGraphic(code);

            return File(buffer, "image/jpeg");
        }
        #endregion

        #region Check Login
        public ActionResult CheckLogin()
        {
            // get validateCode from session.
            string validateCode = Session["validateCode"] == null ? string.Empty : Session["validateCode"].ToString();

            // if validateCode is not fill in.
            if (String.IsNullOrEmpty(validateCode))
            {
                return Content("Captcha can not be empty.");
            }

            // clear session.
            Session["validateCode"] = null;

            // get captcha string by user enter.
            String requestCode = Request["vCode"];

            if (!requestCode.Equals(validateCode, StringComparison.InvariantCultureIgnoreCase))
            {
                return Content("Captcha is wrong. Please enter again.");
            }

            // check user name and password
            String UserName = Request.Form["LoginCode"];
            String UserPwd = Request.Form["LoginPwd"];

            var userInfo = userInfoService.GetList(u => (u.UName == UserName && u.UPwd == UserPwd)).FirstOrDefault();

            // if userInfo is found.
            if (userInfo == null)
            {
                return Content("User Name or PassWord is wrong.");
            }
            else // otherwise.
            {
                // store User Id into Session.
                Session["userInfo"] = userInfo;

                // return content "ok"
                return Content("Ok");
            }
        }
        #endregion
    }
}