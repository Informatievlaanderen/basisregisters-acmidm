namespace Be.Vlaanderen.Basisregisters.Auth.Tests.AuthorizationHandlers
{
    using System.Collections.Generic;
    using System.Linq;
    using System.Security.Claims;
    using System.Threading.Tasks;
    using AcmIdm;
    using AcmIdm.AuthorizationHandlers;
    using FluentAssertions;
    using Microsoft.AspNetCore.Authorization;
    using Xunit;

    public class AcmIdmAuthorizationHandlerTests
    {
        private readonly string[] _allowedScopes =
        [
            "dv_ar_adres_beheer",
            "dv_ar_adres_uitzonderingen"
        ];

        private readonly string[] _blacklistedOvoCodes =
        [
            "OVO001999"
        ];

        private readonly AcmIdmAuthorizationHandler _acmIdmAuthorizationHandler;

        public AcmIdmAuthorizationHandlerTests()
        {
            _acmIdmAuthorizationHandler = new AcmIdmAuthorizationHandler();
        }

        [Fact]
        public async Task WhenAtLeastOneScopesPresent_ThenAuthorized()
        {
            // Arrange
            var context = new AuthorizationHandlerContext(
                new IAuthorizationRequirement[] { new AcmIdmAuthorizationRequirement(_allowedScopes, _blacklistedOvoCodes) },
                CreateUser(
                [
                    (AcmIdmClaimTypes.Scope, _allowedScopes.First())
                ]),
                null);

            //Act
            await _acmIdmAuthorizationHandler.HandleAsync(context);

            //Assert
            context.HasSucceeded.Should().BeTrue();
        }

        [Theory]
        [InlineData(AcmIdmClaimTypes.VoOvoCode)]
        [InlineData(AcmIdmClaimTypes.VoOrgCode)]
        public async Task WhenAllowedScopePresentButBlacklisted_ThenAuthorized(string ovoCodeClaimType)
        {
            // Arrange
            var context = new AuthorizationHandlerContext(
                new IAuthorizationRequirement[] { new AcmIdmAuthorizationRequirement(_allowedScopes, _blacklistedOvoCodes) },
                CreateUser(
                [
                    (AcmIdmClaimTypes.Scope, _allowedScopes.First()),
                    (ovoCodeClaimType, _blacklistedOvoCodes.First())
                ]),
                null);

            //Act
            await _acmIdmAuthorizationHandler.HandleAsync(context);

            //Assert
            context.HasSucceeded.Should().BeFalse();
        }

        [Fact]
        public async Task WhenNoneOfAllowedScopesPresent_ThenUnauthorized()
        {
            // Arrange
            var context = new AuthorizationHandlerContext(
                new IAuthorizationRequirement[] { new AcmIdmAuthorizationRequirement(_allowedScopes, _blacklistedOvoCodes) },
                CreateUser(
                [
                    (AcmIdmClaimTypes.Scope, string.Empty)
                ]),
                null);

            //Act
            await _acmIdmAuthorizationHandler.HandleAsync(context);

            //Assert
            context.HasSucceeded.Should().BeFalse();
        }

        private ClaimsPrincipal CreateUser(IEnumerable<(string claimType, string claimValue)> claims)
        {
            return new ClaimsPrincipal(new ClaimsIdentity(
                claims
                    .Select(x => new Claim(x.claimType, x.claimValue))
                    .ToArray(),
                "Bearer"));
        }
    }
}
