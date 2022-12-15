namespace Be.Vlaanderen.Basisregisters.AcmIdm.AuthorizationHandlers
{
    using System.Linq;
    using System.Security.Claims;
    using System.Threading.Tasks;
    using Microsoft.AspNetCore.Authorization;

    public class AcmIdmAuthorizationHandler : AuthorizationHandler<AcmIdmAuthorizationRequirement>
    {
        protected override Task HandleRequirementAsync(
            AuthorizationHandlerContext context,
            AcmIdmAuthorizationRequirement requirement)
        {
            if (requirement.AllowedValues.Any(
                    scope => context.User.HasClaim(x => x.Type == AcmIdmClaimTypes.Scope && x.Value == scope)))
            {
                var orgCodeClaim = context.User.Claims.SingleOrDefault(x => x.Type == AcmIdmClaimTypes.VoOrgCode);
                if (orgCodeClaim is not null)
                {
                    var niscodeClaim = $"niscode van {orgCodeClaim.Value}";
                    context.User.Identities.FirstOrDefault()?.AddClaim(new Claim("niscode", niscodeClaim.ToString() ?? "bad claim value"));
                }

                context.Succeed(requirement);
                return Task.CompletedTask;
            }

            context.Fail();
            return Task.CompletedTask;
        }
    }
}
