using OA.UI.Models;
using System.Web;
using System.Web.Mvc;

namespace OA.UI
{
    public class FilterConfig
    {
        public static void RegisterGlobalFilters(GlobalFilterCollection filters)
        {
            //filters.Add(new HandleErrorAttribute());
            filters.Add(new MyExceptionAttribute());// use custmer exception filter.
        }
    }
}
