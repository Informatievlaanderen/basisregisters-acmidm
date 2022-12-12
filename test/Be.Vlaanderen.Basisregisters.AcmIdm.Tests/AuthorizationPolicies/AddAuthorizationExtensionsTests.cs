namespace Be.Vlaanderen.Basisregisters.AcmIdm.Tests.AuthorizationPolicies
{
    using System.Linq;
    using Be.Vlaanderen.Basisregisters.AcmIdm.AuthorizationHandlers;
    using FluentAssertions;
    using Microsoft.AspNetCore.Authorization;
    using Xunit;

    public class AddAuthorizationExtensionsTests
    {
        [Fact]
        public void SetupRequiredScopesPolicy()
        {
            const string policyName = "MyPolicy";
            var allowedValues = new[] { "dv_gr_geschetstgebouw_beheer", "dv_gr_geschetstgebouw_uitzonderingen" };

            var authorizationOptions = new AuthorizationOptions()
                .AddAcmIdmAuthorization(policyName, allowedValues);

            var policy = authorizationOptions.GetPolicy(policyName);
            policy.Should().NotBeNull();
            policy!.Requirements.Should().ContainSingle();

            var policyRequirement = policy.Requirements.Single() as RequiredScopesAuthorizationRequirement;
            policyRequirement.Should().NotBeNull();
            policyRequirement!.AllowedValues.Should().BeEquivalentTo(allowedValues);
        }
    }
}
