namespace Be.Vlaanderen.Basisregisters.Auth.AcmIdm.AuthorizationHandlers
{
    using System.Linq;
    using System.Threading.Tasks;
    using Microsoft.AspNetCore.Authorization;

    public class AcmIdmAuthorizationHandler : AuthorizationHandler<AcmIdmAuthorizationRequirement>
    {
        protected override async Task HandleRequirementAsync(
            AuthorizationHandlerContext context,
            AcmIdmAuthorizationRequirement requirement)
        {
            if (requirement.AllowedValues.Any(scope => context.User.HasClaim(x => x.Type == AcmIdmClaimTypes.Scope && x.Value == scope)))
            {
                await Task.Yield();

                context.Succeed(requirement);
                return;
            }

            context.Fail();
        }
    }
}
