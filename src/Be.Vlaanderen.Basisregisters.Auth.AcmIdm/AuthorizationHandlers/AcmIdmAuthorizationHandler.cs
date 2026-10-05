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
            var ovoCode = context.User.FindOvoCodeClaim();

            if (!string.IsNullOrWhiteSpace(ovoCode)
                && requirement.BlacklistedOvoCodes.Any(x => string.Equals(x, ovoCode, StringComparison.InvariantCultureIgnoreCase)))
            {
                context.Fail();
                return;
            }

            if (requirement.AllowedScopes.Any(scope => context.User.HasScope(scope)))
            {
                await Task.Yield();

                context.Succeed(requirement);
                return;
            }

            context.Fail();
        }
    }
}
