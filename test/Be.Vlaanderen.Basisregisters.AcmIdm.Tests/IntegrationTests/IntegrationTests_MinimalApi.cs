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
        [InlineData("dv_ar_adres_beheer")]
        public async Task GivenMinimalApiSecretsEndpoint_WhenClientIsDecentraleBijwerker_ThenRequestIsAuthorized(string scopes)
        {
            var accessToken = await GetAccessToken(
                ClientId,
                ClientSecret,
                scopes);

            var minimalApiHttpClient = RunMinimalApiSample().CreateClient();
            minimalApiHttpClient.DefaultRequestHeaders.Authorization =
                new AuthenticationHeaderValue("Bearer", accessToken);

            var response = await minimalApiHttpClient.GetAsync("/secret");

            response.StatusCode.Should().Be(HttpStatusCode.OK);
        }

        [Theory]
        [InlineData("dv_ar_adres_beheer dv_ar_adres_uitzonderingen")]
        public async Task GivenMinimalApiSecretsVeryEndpoint_WhenClientIsInterneBijwerker_ThenRequestIsAuthorized(string scopes)
        {
            var accessToken = await GetAccessToken(
                ClientId,
                ClientSecret,
                scopes);

            var minimalApiHttpClient = RunMinimalApiSample().CreateClient();
            minimalApiHttpClient.DefaultRequestHeaders.Authorization =
                new AuthenticationHeaderValue("Bearer", accessToken);

            var response = await minimalApiHttpClient.GetAsync("/secret/very");

            response.StatusCode.Should().Be(HttpStatusCode.OK);
        }

        [Theory]
        [InlineData("dv_ar_adres_beheer")]
        [InlineData("dv_ar_adres_uitzonderingen")]
        public async Task GivenMinimalApiSecretsVeryEndpoint_WhenClientIsDecentraleBijwerker_ThenRequestIsUnauthorized(string scopes)
        {
            var accessToken = await GetAccessToken(
                ClientId,
                ClientSecret,
                scopes);

            var minimalApiHttpClient = RunMinimalApiSample().CreateClient();
            minimalApiHttpClient.DefaultRequestHeaders.Authorization =
                new AuthenticationHeaderValue("Bearer", accessToken);

            var response = await minimalApiHttpClient.GetAsync("/secret/very");

            response.StatusCode.Should().Be(HttpStatusCode.Forbidden);
        }
    }
}
