using System.Collections.Generic;
using Newtonsoft.Json;

namespace Helpdesk.WebAPI.Common
{
    public class ApiErrorResponse
    {
        public string Endpoint { get; }

        public string Method { get; }

        public string Message { get; }

        public IDictionary<string, object> Data { get; }

        public override string ToString()
        {
            return JsonConvert.SerializeObject(this);
        }
    }
}