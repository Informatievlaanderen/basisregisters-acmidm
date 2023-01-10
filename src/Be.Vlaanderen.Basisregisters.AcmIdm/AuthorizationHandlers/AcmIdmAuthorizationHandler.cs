namespace Be.Vlaanderen.Basisregisters.AcmIdm.AuthorizationHandlers
{
    using System;
    using System.Linq;
    using System.Security.Claims;
    using System.Threading.Tasks;
    using Microsoft.AspNetCore.Authorization;
    using NisCodeService.Abstractions;

    public class AcmIdmAuthorizationHandler : AuthorizationHandler<AcmIdmAuthorizationRequirement>
    {
        private readonly INisCodeService? _nisCodeService;

        public AcmIdmAuthorizationHandler(INisCodeService? nisCodeService = default)
        {
            _nisCodeService = nisCodeService;
        }

        protected override async Task HandleRequirementAsync(
            AuthorizationHandlerContext context,
            AcmIdmAuthorizationRequirement requirement)
        {
            if (requirement.AllowedValues.Any(scope => context.User.HasClaim(x => x.Type == AcmIdmClaimTypes.Scope && x.Value == scope)))
            {
                await AddNisCodeClaim(context);

                context.Succeed(requirement);
                return;
            }

            context.Fail();
        }

        private async Task AddNisCodeClaim(AuthorizationHandlerContext context)
        {
            var orgCodeClaim = context.User.Claims.SingleOrDefault(x => x.Type == AcmIdmClaimTypes.VoOrgCode);
            if (orgCodeClaim is null)
            {
                return;
            }

            var nisCodeClaim = context.User.Claims.SingleOrDefault(x => x.Type == AcmIdmClaimTypes.NisCode) ??
                new Claim(AcmIdmClaimTypes.NisCode, await FetchNisCode(orgCodeClaim.Value));

            if (!context.User.Claims.Any(x => x.Type.Equals(AcmIdmClaimTypes.NisCode, StringComparison.InvariantCultureIgnoreCase)))
            {
                context.User.Identities.FirstOrDefault()?.AddClaim(nisCodeClaim);
            }
        }

        private async Task<string> FetchNisCode(string orgCode)
        {
            if (_nisCodeService is not null)
            {
                var nisCodes = await _nisCodeService.GetAll();
                return nisCodes[orgCode.WithoutOvoPrefix()!];
            }

            return string.Empty;
        }
    }
}
