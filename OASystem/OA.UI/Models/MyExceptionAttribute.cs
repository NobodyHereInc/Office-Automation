using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace OA.UI.Models
{
    public class MyExceptionAttribute : HandleErrorAttribute
    {
        public static Queue<Exception> exceptionQueue = new Queue<Exception>();

        /// <summary>
        /// Web application will execute this method when app occur exception.
        /// </summary>
        /// <param name="filterContext"></param>
        public override void OnException(ExceptionContext filterContext)
        {
            // write exception into Queue.
            exceptionQueue.Enqueue(filterContext.Exception);

            // redirect to error page.
            filterContext.HttpContext.Response.Redirect("/error.html");
        }
    }
}