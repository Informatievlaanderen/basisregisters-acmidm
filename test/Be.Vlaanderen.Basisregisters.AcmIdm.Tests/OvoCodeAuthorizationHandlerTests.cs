namespace Be.Vlaanderen.Basisregisters.AcmIdm.Tests
{
    using System;
    using System.Security.Claims;
    using System.Threading.Tasks;
    using Abstractions.AuthorizationHandlers;
    using FluentAssertions;
    using Microsoft.AspNetCore.Authorization;
    using Xunit;

    public class OvoCodeAuthorizationHandlerTests
    {
        private readonly OvoCodeAuthorizationHandler _sut;

        public OvoCodeAuthorizationHandlerTests()
        {
            _sut = new OvoCodeAuthorizationHandler();
        }

        [Fact]
        public async Task WhenNoOvoCode_ThenUnauthorized()
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
            await _sut.HandleAsync(context);

            //Assert
            context.HasSucceeded.Should().BeFalse();
        }

        [Fact]
        public async Task WhenInvalidOvoCode_ThenUnauthorized()
        {
            // Arrange
            const string expectedOvoCode = "OVO002067";

            var user = new ClaimsPrincipal(
                new ClaimsIdentity(
                    new[] { new Claim(Abstractions.ClaimTypes.VoOrgCode, "OVO002068") },
                    "Bearer")
            );
            var context = new AuthorizationHandlerContext(
                new IAuthorizationRequirement[] { new OvoCodeAuthorizationRequirement(new MockOvoCodeValidator(expectedOvoCode)) },
                user,
                null);

            //Act
            await _sut.HandleAsync(context);

            //Assert
            context.HasSucceeded.Should().BeFalse();
        }

        [Fact]
        public async Task WhenValidOvoCode_ThenUnauthorized()
        {
            // Arrange
            const string expectedOvoCode = "OVO002067";

            var user = new ClaimsPrincipal(
                new ClaimsIdentity(
                    new[] { new Claim(Abstractions.ClaimTypes.VoOrgCode, expectedOvoCode) },
                    "Bearer")
            );
            var context = new AuthorizationHandlerContext(
                new IAuthorizationRequirement[] { new OvoCodeAuthorizationRequirement(new MockOvoCodeValidator(expectedOvoCode)) },
                user,
                null);

            //Act
            await _sut.HandleAsync(context);

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
