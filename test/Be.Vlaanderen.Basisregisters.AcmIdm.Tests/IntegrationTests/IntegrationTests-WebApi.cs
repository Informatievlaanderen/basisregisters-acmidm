namespace Be.Vlaanderen.Basisregisters.AcmIdm.Tests.IntegrationTests
{
    using System.Net;
    using System.Net.Http;
    using System.Net.Http.Headers;
    using System.Threading.Tasks;
    using AcmIdmConsumer.WebApi;
    using Microsoft.AspNetCore.Hosting;
    using FluentAssertions;
    using Microsoft.AspNetCore.TestHost;
    using Microsoft.Extensions.Logging;
    using Xunit;

    public partial class IntegrationTests
    {
        private TestServer RunWebApiSample()
        {
            var hostBuilder = new WebHostBuilder();
            hostBuilder.UseConfiguration(_configuration);
            hostBuilder.UseStartup<Startup>();
            hostBuilder.ConfigureLogging(loggingBuilder => loggingBuilder.AddConsole());
            hostBuilder.UseTestServer();
            return new TestServer(hostBuilder);
        }

        [Theory]
        [InlineData("dv_ar_adres_beheer")]
        public async Task GivenWebApiSecretsEndpoint_WhenClientIsDecentraleBijwerker_ThenRequestIsAuthorized(string scopes)
        {
            var accessToken = await GetAccessToken(scopes);
            var response = await RunWebApiSample().GetAsync("/v1/secret", accessToken);

            response.StatusCode.Should().Be(HttpStatusCode.OK);
        }

        [Theory]
        [InlineData("dv_ar_adres_beheer dv_ar_adres_uitzonderingen")]
        public async Task GivenWebApiSecretsVeryEndpoint_WhenClientIsInterneBijwerker_ThenRequestIsAuthorized(string scopes)
        {
            var accessToken = await GetAccessToken(scopes);
            var response = await RunWebApiSample().GetAsync("/v1/secret/very", accessToken);

            response.StatusCode.Should().Be(HttpStatusCode.OK);
        }

        [Theory]
        [InlineData("dv_ar_adres_beheer")]
        [InlineData("dv_ar_adres_uitzonderingen")]
        public async Task GivenWebApiSecretsVeryEndpoint_WhenClientHasMissingScope_ThenRequestIsUnauthorized(string scopes)
        {
            var accessToken = await GetAccessToken(scopes);
            var response = await RunWebApiSample().GetAsync("/v1/secret/very", accessToken);

            response.StatusCode.Should().Be(HttpStatusCode.Forbidden);
        }
    }

    public static class WebApiExtensions
    {
        public static async Task<HttpResponseMessage> GetAsync(this TestServer webApi, string requestUri, string accessToken)
        {
            var minimalApiHttpClient = webApi.CreateClient();
            minimalApiHttpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", accessToken);

            return await minimalApiHttpClient.GetAsync(requestUri);
        }
    }
}
