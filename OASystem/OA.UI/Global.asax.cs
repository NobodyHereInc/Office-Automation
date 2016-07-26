using log4net;
using OA.UI.Models;
using Spring.Web.Mvc;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading;
using System.Web;
using System.Web.Mvc;
using System.Web.Optimization;
using System.Web.Routing;

namespace OA.UI
{
    public class MvcApplication : SpringMvcApplication//System.Web.HttpApplication
    {
        protected void Application_Start()
        {

            // for book into with update lucene.Net
            IndexManager.GetInstance().CreateThread();
            log4net.Config.XmlConfigurator.Configure(); //Read Log4Net Config Info.
            AreaRegistration.RegisterAllAreas();
            FilterConfig.RegisterGlobalFilters(GlobalFilters.Filters);
            RouteConfig.RegisterRoutes(RouteTable.Routes);
            BundleConfig.RegisterBundles(BundleTable.Bundles);
            string fileLogPath = Server.MapPath("/Log/");// get path of log folder.


            //start a thread to scan log queue.
            ThreadPool.QueueUserWorkItem((a) =>
            {
                while (true)// Continuously scans the log queue.
                {
                    if (MyExceptionAttribute.exceptionQueue.Count() > 0)//check log queue thether contains exception data.
                    {
                        Exception ex= MyExceptionAttribute.exceptionQueue.Dequeue(); // get exception data from queue.                
                        if (ex != null)
                        {
                            //string fileName = DateTime.Now.ToString("yyyy-MM-dd") + ".txt";
                            //File.AppendAllText(fileLogPath + fileName, ex.ToString(), Encoding.Default);//write exception into log file.
                            ILog logger = LogManager.GetLogger("errorMsg");
                            logger.Error(ex); // write exception into log file.
                        }
                        else
                        {
                            Thread.Sleep(3000);
                        }
                    }
                    else
                    {
                        Thread.Sleep(3000); // If there is no data in the queue, so that the current thread to rest, avoid idling CUP.
                    }
                }


            }, fileLogPath);
        }
    }
}
