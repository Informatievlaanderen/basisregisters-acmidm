namespace Be.Vlaanderen.Basisregisters.Auth.Tests
{
    using System.Security.Claims;
    using AcmIdm;
    using FluentAssertions;
    using Microsoft.AspNetCore.Http;
    using Xunit;

    public class ExtensionTests
    {
        private readonly DefaultHttpContext _httpContext = new()
        {
            User = new ClaimsPrincipal(new ClaimsIdentity(
                new[]
                {
                    new Claim(AcmIdmClaimTypes.Scope, Scopes.DvArAdresUitzonderingen),
                    new Claim("vo_orgcode", "0643634986")
                }))
        };

        [Fact]
        public void HasScope()
        {
            _httpContext.HasScope(Scopes.DvArAdresUitzonderingen).Should().BeTrue();
        }

        [Fact]
        public void DoesntHaveScope()
        {
            _httpContext.HasScope(Scopes.DvArAdresBeheer).Should().BeFalse();
        }

        [Fact]
        public void IsInterneBijwerker()
        {
            _httpContext.IsInterneBijwerker().Should().BeTrue();
        }

        [Fact]
        public void IsNotInterneBijwerker()
        {
            DefaultHttpContext httpContext = new()
            {
                User = new ClaimsPrincipal(new ClaimsIdentity(
                    new[]
                    {
                        new Claim(AcmIdmClaimTypes.Scope, Scopes.DvArAdresBeheer)
                    }))
            };

            httpContext.IsInterneBijwerker().Should().BeFalse();
        }

        [Fact]
        public void FindOrgCodeClaim()
        {
            _httpContext.FindOrgCodeClaim().Should().Be("0643634986");
        }
    }
}
