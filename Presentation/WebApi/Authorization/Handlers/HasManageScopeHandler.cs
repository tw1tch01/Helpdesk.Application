using System.Threading.Tasks;
using Central.Contracts.Policies;
using Microsoft.AspNetCore.Authorization;
using WebApi.Authorization.Requirements;

namespace WebApi.Authorization.Handlers
{
    public class HasManageScopeHandler : AuthorizationHandler<ManageRequirement>
    {
        protected override Task HandleRequirementAsync(AuthorizationHandlerContext context, ManageRequirement requirement)
        {
            if (context.User.HasClaim(c => c.Type == "scope" && c.Value == HelpdeskScopes.Manage))
            {
                context.Succeed(requirement);
            }

            return Task.CompletedTask;
        }
    }
}