using Helpdesk.Persistence.Actions;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Options;

namespace Helpdesk.WebAPI.Configuration
{
    public class ModifiedStateConfiguration : IConfigureOptions<ModifiedStateAction>
    {
        private readonly HttpContext _httpContext;

        public ModifiedStateConfiguration(IHttpContextAccessor httpContextAccessor)
        {
            _httpContext = httpContextAccessor.HttpContext;
        }

        public void Configure(ModifiedStateAction options)
        {
            options.ModifiedBy = _httpContext?.User?.Identity?.Name ?? "system";
            var request = _httpContext?.Request;
            options.ModifiedProcess = request == null ? "/no-context" : $"{request.Path} [{request.Method}]";
        }
    }
}