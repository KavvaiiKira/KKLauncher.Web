using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using System.Net;

namespace KKLauncher.Web.Server.Filters
{
    public class HttpGlobalExceptinFilter : IExceptionFilter
    {
        private readonly ILogger<HttpGlobalExceptinFilter> _logger;

        public HttpGlobalExceptinFilter(ILogger<HttpGlobalExceptinFilter> logger)
        {
            _logger = logger;
        }

        public void OnException(ExceptionContext exceptionContext)
        {
            _logger.LogError(new EventId(
                exceptionContext.Exception.HResult),
                exceptionContext.Exception,
                exceptionContext.Exception.Message);

            var actionName = exceptionContext.ActionDescriptor.DisplayName;
            var exceptionStack = exceptionContext.Exception.StackTrace;
            var exceptionMessage = exceptionContext.Exception.Message;

            exceptionContext.Result = new ContentResult
            {
                Content = $"An error occurred in {actionName}: \n {exceptionMessage} \n {exceptionStack}"
            };

            exceptionContext.ExceptionHandled = true;

            switch (exceptionContext.Exception)
            {
                case ArgumentNullException:
                case ArgumentException:
                    exceptionContext.HttpContext.Response.StatusCode = (int)HttpStatusCode.BadRequest;
                    break;
                default:
                    exceptionContext.HttpContext.Response.StatusCode = (int)HttpStatusCode.InternalServerError;
                    break;
            }
        }
    }
}
