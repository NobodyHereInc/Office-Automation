using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using OA.IService;
using OA.Model;
using OA.Common;

namespace OA.UI.Controllers
{
    public class LogInController : Controller
    {
        private IUserInfoService userInfoService { get; set; }

        // GET: LogIn
        public ActionResult Index()
        {
            CheckCookieInfo();// check Cookie
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
                // Session["userInfo"] = userInfo;

                String sessionId = Guid.NewGuid().ToString();// create sessionId by Memcache.
                // use MemcacheHelper to store.
                MemcacheHelper.Set(sessionId, SerializerHelper.SerializerToString(userInfo));
                // use cookie to store sessionId
                Response.Cookies["sessionId"].Value = sessionId;


                // check user whether select remember Me
                if (!String.IsNullOrEmpty(Request["checkMe"]))
                {
                    HttpCookie cookie1 = new HttpCookie("cp1", UserName);
                    HttpCookie cookie2 = new HttpCookie("cp2", Common.WebCommon.Md5Stirng(UserPwd));
                    cookie1.Expires = DateTime.Now.AddDays(3);
                    cookie2.Expires = DateTime.Now.AddDays(3);
                    Response.Cookies.Add(cookie1);
                    Response.Cookies.Add(cookie2);
                }

                // return content "ok"
                return Content("Ok");
            }
        }
        #endregion

        #region Find Pwd
        public ActionResult FindPwd()
        {
            return View();
        }

        [HttpPost]
        public ActionResult FindPwd(FormCollection collection)
        {
            // get user name and Email.
            string txtName = Request["txtName"];
            string txtMail = Request["txtMail"];

            // check this user whether is exist in database.
            var userInfo = userInfoService.GetList(u => u.UName == txtName).FirstOrDefault();

            // if user is exist.
            if (userInfo != null)
            {
                // check email.
                if (txtMail == "menglingchen3019@gmail.com")
                {
                    // user FindUserPwd to get new password.
                    userInfoService.FindUserPwd(userInfo);
                    // return message.
                    return Content("The Password has find,Please check your Email.!!");
                }
                else // otherwise
                {
                    // email is not correct.
                    return Content("This Email is wrong.");
                }
            }
            else // otherwise
            {
                // this user it not exist.
                return Content("Can not find this user Name!!");
            }
        }
        #endregion

        #region check cookie
        private void CheckCookieInfo()
        {
            if ((Request.Cookies["cp1"] != null) && (Request["cp2"] != null))
            {
                String CookieUserName = Request.Cookies["cp1"].Value;
                String CookieUserPwd = Request.Cookies["cp2"].Value;

                var userInfo = userInfoService.GetList(u => u.UName == CookieUserName).FirstOrDefault();

                if (userInfo != null)
                {
                    //
                    string temp = Common.WebCommon.Md5Stirng(userInfo.UPwd);

                    if (temp == CookieUserPwd)
                    {
                        String sessionId = Guid.NewGuid().ToString();// create sessionId by Memcache.
                                                                     // use MemcacheHelper to store.
                        MemcacheHelper.Set(sessionId, SerializerHelper.SerializerToString(userInfo));
                        // use cookie to store sessionId
                        Response.Cookies["sessionId"].Value = sessionId;

                        Response.Redirect("/home/index");
                    }
                }

                // delete cookie.
                Response.Cookies["cp1"].Expires = DateTime.Now.AddDays(-1);
                Response.Cookies["cp2"].Expires = DateTime.Now.AddDays(-1);
            }
        }
        #endregion
    }
}