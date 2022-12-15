namespace Be.Vlaanderen.Basisregisters.AcmIdm.Tests.AuthorizationHandlers
{
    using System;
    using System.Security.Claims;
    using System.Threading.Tasks;
    using Be.Vlaanderen.Basisregisters.AcmIdm.AuthorizationHandlers;
    using FluentAssertions;
    using Microsoft.AspNetCore.Authorization;
    using Xunit;

    public class OvoCodeAuthorizationHandlerTests
    {
        private readonly OvoCodeAuthorizationHandler _ovoCodeAuthorizationHandler;

        public OvoCodeAuthorizationHandlerTests()
        {
            _ovoCodeAuthorizationHandler = new OvoCodeAuthorizationHandler();
        }

        [Fact]
        public async Task WhenNoOvoCodeClaimPresent_ThenUnauthorized()
        {
            // Arrange
            var user = new ClaimsPrincipal(
                new ClaimsIdentity(
                    Array.Empty<Claim>(),
                    "Bearer")
            );
            var context = new AuthorizationHandlerContext(
                new IAuthorizationRequirement[] { new OvoCodeAuthorizationRequirement(new MockOvoCodeValidator("someOvoCode")) },
                user,
                null);

            //Act
            await _ovoCodeAuthorizationHandler.HandleAsync(context);

            //Assert
            context.HasSucceeded.Should().BeFalse();
        }

        [Fact]
        public async Task WhenInvalidOvoCodeClaimPresent_ThenUnauthorized()
        {
            // Arrange
            const string expectedOvoCode = "OVO002067";

            var user = new ClaimsPrincipal(
                new ClaimsIdentity(
                    new[] { new Claim(AcmIdm.AcmIdmClaimTypes.VoOrgCode, "OVO002068") },
                    "Bearer")
            );
            var context = new AuthorizationHandlerContext(
                new IAuthorizationRequirement[] { new OvoCodeAuthorizationRequirement(new MockOvoCodeValidator(expectedOvoCode)) },
                user,
                null);

            //Act
            await _ovoCodeAuthorizationHandler.HandleAsync(context);

            //Assert
            context.HasSucceeded.Should().BeFalse();
        }

        [Fact]
        public async Task WhenValidOvoCodeClaimPresent_ThenUnauthorized()
        {
            // Arrange
            const string expectedOvoCode = "OVO002067";

            var user = new ClaimsPrincipal(
                new ClaimsIdentity(
                    new[] { new Claim(AcmIdm.AcmIdmClaimTypes.VoOrgCode, expectedOvoCode) },
                    "Bearer")
            );
            var context = new AuthorizationHandlerContext(
                new IAuthorizationRequirement[] { new OvoCodeAuthorizationRequirement(new MockOvoCodeValidator(expectedOvoCode)) },
                user,
                null);

            //Act
            await _ovoCodeAuthorizationHandler.HandleAsync(context);

            //Assert
            context.HasSucceeded.Should().BeTrue();
        }
    }

    public class MockOvoCodeValidator : IOvoCodeValidator
    {
        private readonly OvoCode _expected;

        public MockOvoCodeValidator(string expectedOvoCode)
        {
            _expected = new OvoCode(expectedOvoCode);
        }

        public Task<bool> Validate(OvoCode ovoCode)
        {
            return Task.FromResult(_expected == ovoCode);
        }
    }
}
