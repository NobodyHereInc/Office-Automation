using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using OA.IService;
using OA.Service;
using Microsoft.AspNetCore.Http;

// For more information on enabling MVC for empty projects, visit http://go.microsoft.com/fwlink/?LinkID=397860

namespace OA.UI.Controllers
{
    public class LoginController : Controller
    {
        IUserInfoService us = new UserInfoService();

        // GET: /<controller>/
        public IActionResult Index()
        {
            return View();
        }
        #region Check Login
        public  IActionResult CheckLogin()
        {
            // check captcha.
            var captchaImage = Request.Form["g-recaptcha-response"];
            if (String.IsNullOrEmpty(captchaImage))
            {
                return Content("Captcha image missing.");
            }

            // check user name and password
            String UserName = Request.Form["LoginCode"];
            String UserPwd = Request.Form["LoginPwd"];

            var userInfo = us.GetList(u => (u.UserName == UserName && u.UPwd == UserPwd)).FirstOrDefault();

            // if userInfo is found.
            if (userInfo == null)
            {
                return Content("User Name or PassWord is wrong.");
            }
            else // otherwise.
            {
                // store User Id into Session.
                HttpContext.Session.SetString("Uid", userInfo.UserId.ToString());

                // return content "ok"
                return Content("Ok");
            }        
        }
        #endregion

        #region Old CheckLogIn
        //public async Task<IActionResult> CheckLogin()
        //{
        //    // get value of g-recaptcha-response.
        //    var response = Request.Form["g-recaptcha-response"];

        //    // check value of response.
        //    if (String.IsNullOrEmpty(response))
        //    {
        //        ModelState.AddModelError("", "Captcha image missing.");
        //        return View("Index");
        //    }
        //    else
        //    {
        //        var verified = await IsCaptchaVerified(response);
        //        if (!verified)
        //        {
        //            ModelState.AddModelError("", "Captcha image Verfication failed.");
        //        }
        //    }

        //    if (ModelState.IsValid)
        //    {
        //        // do something.
        //    }
        //}
        #endregion

        #region Tool Functions
        ///// <summary>
        ///// 
        ///// </summary>
        ///// <returns></returns>
        //private async Task<bool> IsCaptchaVerified(String value)
        //{   
        //}
        #endregion
    }


}
