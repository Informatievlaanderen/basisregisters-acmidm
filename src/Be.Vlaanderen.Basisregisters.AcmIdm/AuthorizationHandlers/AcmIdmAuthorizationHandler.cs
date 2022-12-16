namespace Be.Vlaanderen.Basisregisters.AcmIdm.AuthorizationHandlers
{
    using System;
    using System.Collections.Generic;
    using System.Collections.ObjectModel;
    using System.Linq;
    using System.Security.Claims;
    using System.Threading.Tasks;
    using Microsoft.AspNetCore.Authorization;

    public class AcmIdmAuthorizationHandler : AuthorizationHandler<AcmIdmAuthorizationRequirement>
    {
        private static readonly ReadOnlyDictionary<string, string> Cache = new ReadOnlyDictionary<string, string>(new Dictionary<string, string>(1000));

        protected override Task HandleRequirementAsync(
            AuthorizationHandlerContext context,
            AcmIdmAuthorizationRequirement requirement)
        {
            if (requirement.AllowedValues.Any(scope => context.User.HasClaim(x => x.Type == AcmIdmClaimTypes.Scope && x.Value == scope)))
            {
                AddNisCodeClaim(context);

                context.Succeed(requirement);
                return Task.CompletedTask;
            }

            context.Fail();
            return Task.CompletedTask;
        }

        private void AddNisCodeClaim(AuthorizationHandlerContext context)
        {
            var orgCodeClaim = context.User.Claims.SingleOrDefault(x => x.Type == AcmIdmClaimTypes.VoOrgCode);
            if (orgCodeClaim is null)
            {
                return;
            }

            var nisCodeClaim = Cache.ContainsKey(orgCodeClaim.Value)
                ? Cache[orgCodeClaim.Value]
                : FetchNisCode(orgCodeClaim.Value);

            if (!context.User.Claims.Any(x => x.Type.Equals(AcmIdmClaimTypes.NisCode, StringComparison.InvariantCultureIgnoreCase)))
            {
                context.User.Identities.FirstOrDefault()?.AddClaim(new Claim(AcmIdmClaimTypes.NisCode, nisCodeClaim));
            }
        }

        private string FetchNisCode(string orgCode)
        {
            return $"NisCode van {orgCode}";
        }
    }
}
