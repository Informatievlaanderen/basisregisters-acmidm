namespace Be.Vlaanderen.Basisregisters.AcmIdm.Tests.AuthorizationHandlers
{
    using System.Linq;
    using System.Security.Claims;
    using System.Threading.Tasks;
    using Be.Vlaanderen.Basisregisters.AcmIdm.AuthorizationHandlers;
    using FluentAssertions;
    using Microsoft.AspNetCore.Authorization;
    using Xunit;

    public class RequiredScopesAuthorizationHandlerTests
    {
        private readonly string[] _allowedValues = {
            "dv_gr_geschetstgebouw_beheer",
            "dv_gr_geschetstgebouw_uitzonderingen"
        };

        private readonly AcmIdmAuthorizationHandler _acmIdmAuthorizationHandler;

        public RequiredScopesAuthorizationHandlerTests()
        {
            _acmIdmAuthorizationHandler = new AcmIdmAuthorizationHandler();
        }

        [Fact]
        public async Task WhenAtLeastOneScopesPresent_ThenAuthorized()
        {
            // Arrange
            var user = new ClaimsPrincipal(
                new ClaimsIdentity(
                    _allowedValues
                        .Select(x => new Claim(AcmIdm.AcmIdmClaimTypes.Scope, x))
                        .Take(1)
                        .ToArray(),
                    "Bearer")
            );
            var context = new AuthorizationHandlerContext(
                new IAuthorizationRequirement[] { new AcmIdmAuthorizationRequirement(_allowedValues)},
                user,
                null);

            //Act
            await _acmIdmAuthorizationHandler.HandleAsync(context);

            //Assert
            context.HasSucceeded.Should().BeTrue();
        }

        [Fact]
        public async Task WhenNoneOfAllowedScopesPresent_ThenUnauthorized()
        {
            // Arrange
            var user = new ClaimsPrincipal(
                new ClaimsIdentity(Enumerable.Empty<Claim>())
            );
            var context = new AuthorizationHandlerContext(
                new IAuthorizationRequirement[] { new AcmIdmAuthorizationRequirement(_allowedValues) },
                user,
                null);

            //Act
            await _acmIdmAuthorizationHandler.HandleAsync(context);

            //Assert
            context.HasSucceeded.Should().BeFalse();
        }
    }
}
