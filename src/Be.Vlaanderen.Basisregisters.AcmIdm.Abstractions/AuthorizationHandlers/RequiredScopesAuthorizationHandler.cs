namespace Be.Vlaanderen.Basisregisters.AcmIdm.Abstractions.AuthorizationHandlers
{
    using System.Linq;
    using System.Threading.Tasks;
    using Microsoft.AspNetCore.Authorization;

    public class RequiredScopesAuthorizationHandler: AuthorizationHandler<RequiredScopesAuthorizationRequirement>
    {
        protected override Task HandleRequirementAsync(
            AuthorizationHandlerContext context,
            RequiredScopesAuthorizationRequirement requirement)
        {
            if (requirement.Scopes.Any(
                    scope => !context.User.HasClaim(c => c.Type == ClaimTypes.Scope && c.Value == scope)))
            {
                return Task.CompletedTask;
            }

            context.Succeed(requirement);

            return Task.CompletedTask;
        }
    }
}
