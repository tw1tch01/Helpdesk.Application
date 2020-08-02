using System.Net;
using System.Threading.Tasks;
using Helpdesk.WebAPI.Common;
using Microsoft.AspNetCore.Diagnostics;
using Microsoft.AspNetCore.Http;

namespace Helpdesk.WebAPI.Handlers
{
    public static class ExceptionHandler
    {
        public static async Task Handle(HttpContext context)
        {
            var feature = context.Features.Get<IExceptionHandlerPathFeature>();
            var exception = feature.Error;

            while (exception.InnerException != null) exception = exception.InnerException;

            var response = new ApiErrorResponse(context, exception);

            context.Response.ContentType = "application/json";
            context.Response.StatusCode = (int)HttpStatusCode.InternalServerError;
            await context.Response.WriteAsync(response.ToString());
        }
    }
}