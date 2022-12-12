namespace Be.Vlaanderen.Basisregisters.AcmIdm.Tests.IntegrationTests
{
    using System.Net;
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
        [InlineData("dv_gr_geschetstgebouw_uitzonderingen")]
        [InlineData("dv_gr_geschetstgebouw_beheer")]
        public async Task GivenWebApi_WhenClientHasValidScope_ThenRequestIsAuthorized(string scope)
        {
            var accessToken = await GetAccessToken(
                ClientId,
                ClientSecret,
                scope);

            var webApiHttpClient = RunWebApiSample().CreateClient();
            webApiHttpClient.DefaultRequestHeaders.Authorization =
                new AuthenticationHeaderValue("Bearer", accessToken);

            var response = await webApiHttpClient.GetAsync("/v1/secret");

            response.StatusCode.Should().Be(HttpStatusCode.OK);
        }

        [Theory]
        [InlineData("another_scope")]
        public async Task GivenWebApi_WhenClientHasNoneOfTheValidScopes_ThenRequestIsUnauthorized(string scope)
        {
            var accessToken = await GetAccessToken(
                ClientId,
                ClientSecret,
                scope);

            var minimalApiHttpClient = RunWebApiSample().CreateClient();
            minimalApiHttpClient.DefaultRequestHeaders.Authorization =
                new AuthenticationHeaderValue("Bearer", accessToken);

            var response = await minimalApiHttpClient.GetAsync("/v1/secret");

            response.StatusCode.Should().Be(HttpStatusCode.Forbidden);
        }
    }
}
