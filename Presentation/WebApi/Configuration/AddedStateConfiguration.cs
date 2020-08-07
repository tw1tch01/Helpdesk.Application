using Helpdesk.Persistence.Actions;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Options;

namespace Helpdesk.WebAPI.Configuration
{
    public class AddedStateConfiguration : IConfigureOptions<AddedStateAction>
    {
        private readonly HttpContext _httpContext;

        public AddedStateConfiguration(IHttpContextAccessor httpContextAccessor)
        {
            _httpContext = httpContextAccessor.HttpContext;
        }

        public void Configure(AddedStateAction options)
        {
            options.CreatedBy = _httpContext?.User?.Identity?.Name ?? "system";
            var request = _httpContext?.Request;
            options.CreatedProcess = request == null ? "/no-context" : $"{request.Path} [{request.Method}]";
        }
    }
}