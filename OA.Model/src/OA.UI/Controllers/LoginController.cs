using System;
using System.Linq;
using Microsoft.AspNetCore.Mvc;
using OA.IService;
using OA.Service;
using Microsoft.AspNetCore.Http;

// For more information on enabling MVC for empty projects, visit http://go.microsoft.com/fwlink/?LinkID=397860

namespace OA.UI.Controllers
{
    public class LoginController : Controller
    {
        public IUserInfoService us = new UserInfoService();

        // GET: /<controller>/
        public IActionResult Index()
        {
            return View();
        }

        #region Find Pwd
        public IActionResult FindPwd()
        {
            return View();
        }

        [HttpPost]
        public IActionResult FindPwd(FormCollection collection)
        {
            ////
            //var certificate = new X509Certificate2(@"C:\path\to\certificate.p12", "password", X509KeyStorageFlags.Exportable);
            //var credential = new ServiceAccountCredential(new ServiceAccountCredential
            //    .Initializer("your-developer-id@developer.gserviceaccount.com")
            //{
            //    // Note: other scopes can be found here: https://developers.google.com/gmail/api/auth/scopes
            //    Scopes = new[] { "https://mail.google.com/" },
            //    User = "username@gmail.com"
            //}.FromCertificate(certificate));

            ////You can also use FromPrivateKey(privateKey) where privateKey
            //// is the value of the fiel 'private_key' in your serviceName.json file

            //bool result = await credential.RequestAccessTokenAsync(cancel.Token);

            //// Note: result will be true if the access token was received successfully


            // get UserName and mail from Request object.
            String UserName = Request.Form["txtName"];
            String mail = Request.Form["txtMail"];

            //get this user info
            var userInfo = us.GetList(u => u.Uname == UserName).FirstOrDefault();

            // check this user whether exist.
            if (userInfo != null)
            {
                // check Email
                if (mail == "menglingchen3019@gmail.com")
                {
                    us.FindUserPwd(userInfo);
                    return Content("User password has been reset, Please check your Email.");
                }
                else
                {
                    return Content("Email is not correct.");
                }
            }
            else // otherwise.
            {
                return Content("Can't find this User.");
            }
        }
        #endregion

        #region Check Login
        public IActionResult CheckLogin()
        {
            // check captcha.
            //var captchaImage = Request.Form["g-recaptcha-response"];
            //if (String.IsNullOrEmpty(captchaImage))
            //{
            //    return Content("Captcha image missing.");
            //}

            // check user name and password
            String UserName = Request.Form["LoginCode"];
            String UserPwd = Request.Form["LoginPwd"];

            var userInfo = us.GetList(u => (u.Uname == UserName && u.Upwd == UserPwd)).FirstOrDefault();

            // if userInfo is found.
            if (userInfo == null)
            {
                return Content("User Name or PassWord is wrong.");
            }
            else // otherwise.
            {
                // store User Id into Session.
                HttpContext.Session.SetString("Uid", userInfo.Id.ToString());

                #region RememberMe
                //if (!String.IsNullOrEmpty(Request.Form["checkMe"]))
                //{
                //    HttpContext.Response.Cookies.Append("", "");
                //}
                #endregion

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
        #endregion
    }


}
