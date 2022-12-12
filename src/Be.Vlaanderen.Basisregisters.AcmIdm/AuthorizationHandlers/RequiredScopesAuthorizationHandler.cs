namespace Be.Vlaanderen.Basisregisters.AcmIdm.AuthorizationHandlers
{
    using System.Linq;
    using System.Threading.Tasks;
    using Microsoft.AspNetCore.Authorization;

    public class RequiredScopesAuthorizationHandler : AuthorizationHandler<RequiredScopesAuthorizationRequirement>
    {
        protected override Task HandleRequirementAsync(
            AuthorizationHandlerContext context,
            RequiredScopesAuthorizationRequirement requirement)
        {
            if (requirement.AllowedValues.Any(
                    scope => context.User.HasClaim(x => x.Type == ClaimTypes.Scope && x.Value == scope)))
            {
                context.Succeed(requirement);
                return Task.CompletedTask;
            }

            context.Fail();
            return Task.CompletedTask;
        }
    }
}
