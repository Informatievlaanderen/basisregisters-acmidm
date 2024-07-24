namespace Be.Vlaanderen.Basisregisters.Auth.AcmIdm.AuthorizationHandlers
{
    using System;
    using System.Linq;
    using System.Threading.Tasks;
    using Microsoft.AspNetCore.Authorization;

    public class AcmIdmAuthorizationHandler : AuthorizationHandler<AcmIdmAuthorizationRequirement>
    {
        protected override async Task HandleRequirementAsync(
            AuthorizationHandlerContext context,
            AcmIdmAuthorizationRequirement requirement)
        {
            var ovoCode = FindOvoCode(context);

            if (!string.IsNullOrWhiteSpace(ovoCode)
                && requirement.BlacklistedOvoCodes.Any(x => string.Equals(x, ovoCode, StringComparison.InvariantCultureIgnoreCase)))
            {
                context.Fail();
                return;
            }

            if (requirement.AllowedScopes.Any(scope =>
                    context.User.HasClaim(x => x.Type == AcmIdmClaimTypes.Scope && x.Value == scope)))
            {
                await Task.Yield();

                context.Succeed(requirement);
                return;
            }

            context.Fail();
        }

        private static string? FindOvoCode(AuthorizationHandlerContext context)
        {
            var voOvoCodeValue = context.User.FindFirst(AcmIdmClaimTypes.VoOvoCode)?.Value;

            if (voOvoCodeValue is not null)
            {
                return voOvoCodeValue;
            }

            var voOrgCodeValue = context.User.FindFirst(AcmIdmClaimTypes.VoOrgCode)?.Value;

            if (voOrgCodeValue is not null && voOrgCodeValue.StartsWith("ovo", StringComparison.OrdinalIgnoreCase))
                return voOrgCodeValue;

            return null;
        }
    }
}
