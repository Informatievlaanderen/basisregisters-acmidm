namespace Be.Vlaanderen.Basisregisters.AcmIdm.Tests.AuthorizationHandlers
{
    using System.Collections.Generic;
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
            "dv_ar_adres_beheer",
            "dv_ar_adres_uitzonderingen"
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
            var context = new AuthorizationHandlerContext(
                new IAuthorizationRequirement[] { new AcmIdmAuthorizationRequirement(_allowedValues)},
                CreateUser(_allowedValues.Take(1)),
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
            var context = new AuthorizationHandlerContext(
                new IAuthorizationRequirement[] { new AcmIdmAuthorizationRequirement(_allowedValues) },
                CreateUser(_allowedValues.Take(0)),
                null);

            //Act
            await _acmIdmAuthorizationHandler.HandleAsync(context);

            //Assert
            context.HasSucceeded.Should().BeFalse();
        }

        private ClaimsPrincipal CreateUser(IEnumerable<string> scopes)
        {
            return new ClaimsPrincipal(new ClaimsIdentity(scopes.Select(x => new Claim(AcmIdmClaimTypes.Scope, x)).ToArray(), "Bearer"));
        }
    }
}
