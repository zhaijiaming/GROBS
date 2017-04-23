using System.Web;
using System.Web.Mvc;
using GROBS.Filters;

namespace GROBS
{
    public class FilterConfig
    {
        public static void RegisterGlobalFilters(GlobalFilterCollection filters)
        {
            filters.Add(new HandleErrorAttribute());
            filters.Add(new YaoAuthorizeAttribute());
        }
    }
}
