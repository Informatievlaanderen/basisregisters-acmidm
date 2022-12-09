namespace Be.Vlaanderen.Basisregisters.AcmIdm.Tests.AuthorizationHandlers
{
    using System.Linq;
    using System.Security.Claims;
    using System.Threading.Tasks;
    using Be.Vlaanderen.Basisregisters.AcmIdm.Abstractions.AuthorizationHandlers;
    using FluentAssertions;
    using Microsoft.AspNetCore.Authorization;
    using Xunit;

    public class RequiredScopesAuthorizationHandlerTests
    {
        private readonly string[] _allowedValues = {
            "dv_gr_geschetstgebouw_beheer",
            "dv_gr_geschetstgebouw_uitzonderingen"
        };

        private readonly RequiredScopesAuthorizationHandler _requiredScopesAuthorizationHandler;

        public RequiredScopesAuthorizationHandlerTests()
        {
            _requiredScopesAuthorizationHandler = new RequiredScopesAuthorizationHandler();
        }

        [Fact]
        public async Task WhenAtLeastOneScopesPresent_ThenAuthorized()
        {
            // Arrange
            var user = new ClaimsPrincipal(
                new ClaimsIdentity(
                    _allowedValues
                        .Select(x => new Claim(Abstractions.ClaimTypes.Scope, x))
                        .Take(1)
                        .ToArray(),
                    "Bearer")
            );
            var context = new AuthorizationHandlerContext(
                new IAuthorizationRequirement[] { new RequiredScopesAuthorizationRequirement(_allowedValues)},
                user,
                null);

            //Act
            await _requiredScopesAuthorizationHandler.HandleAsync(context);

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
                new IAuthorizationRequirement[] { new RequiredScopesAuthorizationRequirement(_allowedValues) },
                user,
                null);

            //Act
            await _requiredScopesAuthorizationHandler.HandleAsync(context);

            //Assert
            context.HasSucceeded.Should().BeFalse();
        }
    }
}
