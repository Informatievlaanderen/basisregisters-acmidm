namespace Be.Vlaanderen.Basisregisters.AcmIdm.AuthorizationHandlers
{
    using System;
    using System.Linq;
    using System.Threading.Tasks;
    using Microsoft.AspNetCore.Authorization;

    public class OvoCodeAuthorizationHandler : AuthorizationHandler<OvoCodeAuthorizationRequirement>
    {
        protected override async Task HandleRequirementAsync(
            AuthorizationHandlerContext context,
            OvoCodeAuthorizationRequirement requirement)
        {
            if (!context.User.HasClaim(c => c.Type == AcmIdmClaimTypes.VoOrgCode))
            {
                return;
            }

            var ovoCode = new OvoCode(context.User.Claims.Single(x => x.Type == AcmIdmClaimTypes.VoOrgCode).Value);

            try
            {
                if (await requirement.OvoCodeValidator.Validate(ovoCode))
                {
                    context.Succeed(requirement);
                }
            }
            catch (Exception)
            {
                // ignored
            }
        }
    }
}
