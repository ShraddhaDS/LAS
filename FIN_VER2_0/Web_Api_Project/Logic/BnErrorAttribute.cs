using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Web_Api_Project.Logic
{
    public class BnErrorAttribute : System.Web.Http.Filters.ExceptionFilterAttribute
    {
        public override void OnException(System.Web.Http.Filters.HttpActionExecutedContext actionExecutedContext)
        {
            if (actionExecutedContext.Exception != null)
            {
                dbErrorLogging.LogError(actionExecutedContext.Exception);
                Elmah.ErrorSignal.FromCurrentContext().Raise(actionExecutedContext.Exception);
                base.OnException(actionExecutedContext);
            }
              
        }
    }

}