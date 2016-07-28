using ServiceStack.Redis;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace OA.UI.Models
{
    public class MyExceptionAttribute : HandleErrorAttribute
    {
        // public static Queue<Exception> exceptionQueue = new Queue<Exception>();

        // redis replace  Queue.
        public static IRedisClientsManager clientManager = new PooledRedisClientManager(new string[] { "127.0.0.1:6379" });
        public static IRedisClient redisClent = clientManager.GetClient();

        /// <summary>
        /// Web application will execute this method when app occur exception.
        /// </summary>
        /// <param name="filterContext"></param>
        public override void OnException(ExceptionContext filterContext)
        {
            base.OnException(filterContext);

            // write exception into Queue.
            //exceptionQueue.Enqueue(filterContext.Exception);

            //write exception into reids server.
            redisClent.EnqueueItemOnList("errorMessage", filterContext.Exception.ToString());

            // redirect to error page.
            filterContext.HttpContext.Response.Redirect("/Error.html");

            filterContext.HttpContext.Response.End();
        }
    }
}