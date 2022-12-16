namespace Be.Vlaanderen.Basisregisters.AcmIdm.Tests.IntegrationTests
{
    using System.Net;
    using System.Net.Http;
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
            var accessToken = await GetAccessToken(scopes);
            var response = await RunMinimalApiSample().GetAsync("/secret", accessToken);

            response.StatusCode.Should().Be(HttpStatusCode.OK);
        }

        [Theory]
        [InlineData("dv_ar_adres_beheer dv_ar_adres_uitzonderingen")]
        public async Task GivenMinimalApiSecretsVeryEndpoint_WhenClientIsInterneBijwerker_ThenRequestIsAuthorized(string scopes)
        {
            var accessToken = await GetAccessToken(scopes);
            var response = await RunMinimalApiSample().GetAsync("/secret/very", accessToken);

            response.StatusCode.Should().Be(HttpStatusCode.OK);
        }

        [Theory]
        [InlineData("dv_ar_adres_beheer")]
        [InlineData("dv_ar_adres_uitzonderingen")]
        public async Task GivenMinimalApiSecretsVeryEndpoint_WhenClientHasMissingScope_ThenRequestIsUnauthorized(string scopes)
        {
            var accessToken = await GetAccessToken(scopes);
            var response = await RunMinimalApiSample().GetAsync("/secret/very", accessToken);

            response.StatusCode.Should().Be(HttpStatusCode.Forbidden);
        }
    }

    public static class MinimalApiExtensions
    {
        public static async Task<HttpResponseMessage> GetAsync(
            this WebApplicationFactory<AcmIdmConsumer.MinimalApi.Program> minimalApi,
            string requestUri,
            string accessToken)
        {
            var minimalApiHttpClient = minimalApi.CreateClient();
            minimalApiHttpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", accessToken);

            return await minimalApiHttpClient.GetAsync(requestUri);
        }
    }
}
