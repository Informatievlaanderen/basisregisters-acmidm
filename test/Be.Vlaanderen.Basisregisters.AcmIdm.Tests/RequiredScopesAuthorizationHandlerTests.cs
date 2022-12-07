namespace Be.Vlaanderen.Basisregisters.AcmIdm.Tests
{
    using System.Linq;
    using System.Security.Claims;
    using System.Threading.Tasks;
    using Abstractions.AuthorizationHandlers;
    using FluentAssertions;
    using Microsoft.AspNetCore.Authorization;
    using Xunit;

    public class RequiredScopesAuthorizationHandlerTests
    {
        private readonly RequiredScopesAuthorizationHandler _requiredScopesAuthorizationHandler;
        private readonly string[] _allowedValues = {
            "dv_gr_geschetstgebouw_beheer",
            "dv_gr_geschetstgebouw_uitzonderingen"
        };

        public RequiredScopesAuthorizationHandlerTests()
        {
            _requiredScopesAuthorizationHandler = new RequiredScopesAuthorizationHandler();
        }

        [Fact]
        public async Task WhenAllRequiredScopesPresent_ThenAuthorized()
        {
            // Arrange
            var user = new ClaimsPrincipal(
                new ClaimsIdentity(
                    _allowedValues
                        .Select(x => new Claim(Abstractions.ClaimTypes.Scope, x))
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
        public async Task WhenMissingRequiredScopesPresent_ThenUnauthorized()
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
