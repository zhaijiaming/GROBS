using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace GROBS.Filters
{
    public class YaoAuthorizeAttribute : AuthorizeAttribute
    {
        protected override bool AuthorizeCore(HttpContextBase httpContext)
        {
            var isAuthorized = false;
            if (httpContext != null && httpContext.Session != null)
            {
                if (httpContext.Session["user_id"] != null && httpContext.Session["user_account"] != null)
                    isAuthorized = true;
            }
            return isAuthorized;// base.AuthorizeCore(httpContext);
        }
        public override void OnAuthorization(AuthorizationContext filterContext)
        {
            base.OnAuthorization(filterContext);
        }
    }
}