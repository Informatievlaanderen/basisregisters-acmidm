namespace Be.Vlaanderen.Basisregisters.Auth.Tests.IntegrationTests
{
    using System;
    using System.Collections.Generic;
    using System.IO;
    using System.Net.Http;
    using System.Net.Http.Headers;
    using System.Threading.Tasks;
    using AcmIdmConsumer.WebApi;
    using Be.Vlaanderen.Basisregisters.DockerUtilities;
    using Ductus.FluentDocker.Services;
    using IdentityModel;
    using IdentityModel.AspNetCore.OAuth2Introspection;
    using IdentityModel.Client;
    using Microsoft.AspNetCore.Hosting;
    using Microsoft.AspNetCore.Mvc.Testing;
    using Microsoft.AspNetCore.TestHost;
    using Microsoft.Extensions.Configuration;
    using Microsoft.Extensions.Logging;
    using Xunit;
    using Program = AcmIdmConsumer.MinimalApi.Program;

    public class IntegrationTestFixture : IAsyncLifetime
    {
        private const string ClientId = "acmClient";
        private const string ClientSecret = "secret";

        private OAuth2IntrospectionOptions _oAuth2IntrospectionOptions = null!;

        private ICompositeService _identityServerFake = null!;
        private WebApplicationFactory<Program> _minimalApiSample = null!;
        private TestServer _webApiSample = null!;

        public Task InitializeAsync()
        {
            var applicationConfiguration = new ConfigurationBuilder()
                .SetBasePath(Directory.GetCurrentDirectory())
                .AddJsonFile("appsettings.json")
                .AddJsonFile($"appsettings.{Environment.MachineName.ToLowerInvariant()}.json", optional: true)
                .Build();

            _identityServerFake = DockerComposer.Compose("identityserverfake_test.yml", "acm-idm-integrationtests");
            _minimalApiSample = RunMinimalApiSample(applicationConfiguration);
            _webApiSample = RunWebApiSample(applicationConfiguration);

            _oAuth2IntrospectionOptions = applicationConfiguration.GetSection(nameof(OAuth2IntrospectionOptions)).Get<OAuth2IntrospectionOptions>()!;

            return Task.CompletedTask;
        }

        private WebApplicationFactory<Program> RunMinimalApiSample(IConfiguration applicationConfiguration)
        {
            return new WebApplicationFactory<Program>()
                .WithWebHostBuilder(x => x.UseConfiguration(applicationConfiguration));
        }

        private TestServer RunWebApiSample(IConfiguration applicationConfiguration)
        {
            var hostBuilder = new WebHostBuilder()
                .UseConfiguration(applicationConfiguration)
                .UseStartup<Startup>()
                .ConfigureLogging(loggingBuilder => loggingBuilder.AddConsole())
                .UseTestServer();

            return new TestServer(hostBuilder);
        }

        public async Task<HttpResponseMessage> GetMinimalApiAsync(string requestUri, string accessToken)
        {
            var minimalApiHttpClient = _minimalApiSample.CreateClient();
            minimalApiHttpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", accessToken);

            return await minimalApiHttpClient.GetAsync(requestUri);
        }

        public async Task<HttpResponseMessage> GetWebApiAsync(string requestUri, string accessToken)
        {
            var webApiHttpClient = _webApiSample.CreateClient();
            webApiHttpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", accessToken);

            return await webApiHttpClient.GetAsync(requestUri);
        }

        public async Task<string> GetAccessToken(string scopes)
        {
            var tokenClient = new TokenClient(
                () => new HttpClient(),
                new TokenClientOptions
                {
                    Address = $"{_oAuth2IntrospectionOptions.Authority}/connect/token",
                    ClientId = ClientId,
                    ClientSecret = ClientSecret,
                    Parameters = new Parameters(new[] { new KeyValuePair<string, string>("scope", scopes) })
                });

            var response = await tokenClient.RequestTokenAsync(OidcConstants.GrantTypes.ClientCredentials);

            return response.AccessToken;
        }

        public Task DisposeAsync()
        {
            _identityServerFake.Dispose();
            _minimalApiSample.Dispose();
            _webApiSample.Dispose();

            return Task.CompletedTask;
        }
    }
}
