namespace Be.Vlaanderen.Basisregisters.AcmIdm.Tests.IntegrationTests
{
    using System.Net;
    using System.Net.Http.Headers;
    using System.Threading.Tasks;
    using Microsoft.AspNetCore.Hosting;
    using FluentAssertions;
    using Microsoft.AspNetCore.Mvc.Testing;
    using Xunit;

    public partial class IntegrationTests
    {
        private WebApplicationFactory<AcmIdmConsumer.MinimalApi.Program> RunMinimalApiSample()
        {
            return new WebApplicationFactory<AcmIdmConsumer.MinimalApi.Program>()
                .WithWebHostBuilder(x => x.UseConfiguration(_configuration));
        }

        [Theory]
        [InlineData("dv_gr_geschetstgebouw_uitzonderingen")]
        [InlineData("dv_gr_geschetstgebouw_beheer")]
        public async Task GivenMinimalApi_WhenClientHasValidScopes_ThenRequestIsAuthorized(string scope)
        {
            var accessToken = await GetAccessToken(
                ClientId,
                ClientSecret,
                scope);

            var minimalApiHttpClient = RunMinimalApiSample().CreateClient();
            minimalApiHttpClient.DefaultRequestHeaders.Authorization =
                new AuthenticationHeaderValue("Bearer", accessToken);

            var response = await minimalApiHttpClient.GetAsync("/secret");

            response.StatusCode.Should().Be(HttpStatusCode.OK);
        }

        [Fact]
        public async Task GivenMinimalApi_WhenClientHasNoneOfTheValidScopes_ThenRequestIsUnauthorized()
        {
            var accessToken = await GetAccessToken(
                ClientId,
                ClientSecret,
                "another_scope");

            var minimalApiHttpClient = RunMinimalApiSample().CreateClient();
            minimalApiHttpClient.DefaultRequestHeaders.Authorization =
                new AuthenticationHeaderValue("Bearer", accessToken);

            var response = await minimalApiHttpClient.GetAsync("/secret");

            response.StatusCode.Should().Be(HttpStatusCode.Forbidden);
        }
    }
}
