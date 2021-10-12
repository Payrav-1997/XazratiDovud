using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.Extensions.Logging;

namespace Core.Extensions
{
    public class ExceptionLogingAttribute : ExceptionFilterAttribute
    {
        private readonly ILogger<ExceptionLogingAttribute> _logger;

        public ExceptionLogingAttribute(ILogger<ExceptionLogingAttribute> logger)
        {
            this._logger = logger;
        }


        public override void OnException(ExceptionContext context)
        {
            this._logger.LogError("DisplayName -- {0}, Message -- {1}", context.ActionDescriptor.DisplayName, context.Exception.Message);
            if (context.Exception.InnerException != null && !string.IsNullOrEmpty(context.Exception.InnerException.Message))
            {
                this._logger.LogError("DisplayName -- {0}, InnerException -- {1}", context.ActionDescriptor.DisplayName, context.Exception.InnerException.Message);
            }
            this._logger.LogError("DisplayName -- {0}, StackTrace -- {1}", context.ActionDescriptor.DisplayName, context.Exception.StackTrace);

            context.Result = new ViewResult() { ViewName = "Error" };
        }
    }
}
