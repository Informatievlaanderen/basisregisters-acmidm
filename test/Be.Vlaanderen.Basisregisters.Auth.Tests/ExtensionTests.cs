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

        [Theory]
        [InlineData("OVO000111", "OVO000222", "OVO000333", "OVO000111")]
        [InlineData("", "OVO000222", "OVO000333", "OVO000222")]
        [InlineData("", "12345678", "OVO000333", "OVO000333")]
        [InlineData("   ", "", "OVO000333", "OVO000333")]
        [InlineData("", "12345678", "", null)]
        public void FindOvoCodeClaim_RespectsClaimPriority(
            string voOvoCode,
            string voOrgCode,
            string customOvoCode,
            string? expected)
        {
            DefaultHttpContext httpContext = new()
            {
                User = new ClaimsPrincipal(new ClaimsIdentity(
                    new[]
                    {
                        new Claim(AcmIdmClaimTypes.VoOvoCode, voOvoCode),
                        new Claim(AcmIdmClaimTypes.VoOrgCode, voOrgCode),
                        new Claim(AcmIdmClaimTypes.CustomOvoCode, customOvoCode)
                    }))
            };

            httpContext.FindOvoCodeClaim().Should().Be(expected);
            httpContext.User.FindOvoCodeClaim().Should().Be(expected);
        }

        [Fact]
        public void FindCustomOrgOvoCodeClaim()
        {
            DefaultHttpContext httpContext = new()
            {
                User = new ClaimsPrincipal(new ClaimsIdentity(
                    new[]
                    {
                        new Claim(AcmIdmClaimTypes.CustomOvoCode, "OVO002949")
                    }))
            };

            httpContext.FindCustomOrgOvoCodeClaim().Should().Be("OVO002949");
            httpContext.User.FindCustomOrgOvoCodeClaim().Should().Be("OVO002949");
        }
    }
}
