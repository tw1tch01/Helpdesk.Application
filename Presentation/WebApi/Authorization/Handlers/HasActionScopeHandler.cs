using System.Threading.Tasks;
using Central.Contracts.Policies;
using Microsoft.AspNetCore.Authorization;
using WebApi.Authorization.Requirements;

namespace WebApi.Authorization.Handlers
{
    public class HasActionScopeHandler : AuthorizationHandler<ActionRequirement>
    {
        protected override Task HandleRequirementAsync(AuthorizationHandlerContext context, ActionRequirement requirement)
        {
            if (context.User.HasClaim(c => c.Type == "scope" && c.Value == HelpdeskScopes.Action))
            {
                context.Succeed(requirement);
            }

            return Task.CompletedTask;
        }
    }
}