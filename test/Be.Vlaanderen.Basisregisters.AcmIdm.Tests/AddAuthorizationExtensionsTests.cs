namespace Be.Vlaanderen.Basisregisters.AcmIdm.Tests
{
    using System.Linq;
    using Abstractions;
    using Abstractions.AuthorizationHandlers;
    using FluentAssertions;
    using Microsoft.AspNetCore.Authorization;
    using Xunit;

    public class AddAuthorizationExtensionsTests
    {
        [Fact]
        public void SetupRequiredScopesPolicy()
        {
            const string policyName = "MyPolicy";
            var requiredScopes = new[] { "dv_gr_geschetstgebouw_beheer", "dv_gr_geschetstgebouw_uitzonderingen" };

            var authorizationOptions = new AuthorizationOptions()
                .AddRequiredScopesPolicy(policyName, requiredScopes);

            var policy = authorizationOptions.GetPolicy(policyName);
            policy.Should().NotBeNull();
            policy!.Requirements.Should().ContainSingle();

            var policyRequirement = policy.Requirements.Single() as RequiredScopesAuthorizationRequirement;
            policyRequirement.Should().NotBeNull();
            policyRequirement!.AllowedValues.Should().BeEquivalentTo(requiredScopes);
        }
    }
}
