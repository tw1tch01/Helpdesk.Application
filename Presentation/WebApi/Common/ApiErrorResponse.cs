using System;
using Microsoft.AspNetCore.Http;
using Newtonsoft.Json;

namespace Helpdesk.WebAPI.Common
{
    public class ApiErrorResponse
    {
        public ApiErrorResponse(HttpContext context, Exception exception)
        {
            Endpoint = context.Request.Path;
            Method = context.Request.Method;
            Message = exception.Message;
        }

        public string Endpoint { get; }

        public string Method { get; }

        public string Message { get; }

        public override string ToString()
        {
            return JsonConvert.SerializeObject(this);
        }
    }
}