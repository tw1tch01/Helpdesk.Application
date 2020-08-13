using Microsoft.AspNetCore.Authorization;

namespace WebApi.Authorization.Requirements
{
    public interface IEndpointRequirement : IAuthorizationRequirement
    {
    }
}