using System.Threading.Tasks;
using Central.Contracts.Policies;
using Microsoft.AspNetCore.Authorization;
using WebApi.Authorization.Requirements;

namespace WebApi.Authorization.Handlers
{
    public class HasFullScopeHandler : AuthorizationHandler<IEndpointRequirement>
    {
        protected override Task HandleRequirementAsync(AuthorizationHandlerContext context, IEndpointRequirement requirement)
        {
            if (context.User.HasClaim(c => c.Type == "scope" && c.Value == HelpdeskScopes.Full))
            {
                context.Succeed(requirement);
            }

            return Task.CompletedTask;
        }
    }
}