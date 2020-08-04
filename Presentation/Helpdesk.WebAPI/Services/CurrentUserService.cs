using System;
using System.Security.Claims;
using Helpdesk.Common.Interfaces;
using Microsoft.AspNetCore.Http;

namespace Helpdesk.WebAPI.Services
{
    public class CurrentUserService : ICurrentUserService
    {
        public CurrentUserService(IHttpContextAccessor httpContextAccessor)
        {
            var identitifer = httpContextAccessor.HttpContext?.User?.FindFirstValue(ClaimTypes.NameIdentifier);
            UserGuid = string.IsNullOrWhiteSpace(identitifer) ? Guid.Empty : Guid.Parse(identitifer);
        }

        public Guid UserGuid { get; }
    }
}