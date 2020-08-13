using System.Threading.Tasks;
using Central.Contracts.Policies;
using Microsoft.AspNetCore.Authorization;
using WebApi.Authorization.Requirements;

namespace WebApi.Authorization.Handlers
{
    public class HasReadScopeHandler : AuthorizationHandler<QueryRequirement>
    {
        protected override Task HandleRequirementAsync(AuthorizationHandlerContext context, QueryRequirement requirement)
        {
            if (context.User.HasClaim(c => c.Type == "scope" && c.Value == HelpdeskScopes.Read))
            {
                context.Succeed(requirement);
            }

            return Task.CompletedTask;
        }
    }
}