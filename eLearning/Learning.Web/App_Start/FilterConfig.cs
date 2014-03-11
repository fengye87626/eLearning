using System.Web;
using System.Web.Mvc;
using Learning.Web.Filters;

namespace Learning.Web
{
    public class FilterConfig
    {
        public static void RegisterGlobalFilters(GlobalFilterCollection filters)
        {
            filters.Add(new HandleErrorAttribute());
        }
    }
}