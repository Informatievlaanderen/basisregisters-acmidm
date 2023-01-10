namespace Be.Vlaanderen.Basisregisters.AcmIdm.Tests.IntegrationTests
{
    using System;
    using System.Collections.Generic;
    using System.IO;
    using System.Net.Http;
    using System.Threading.Tasks;
    using ContainerHelper;
    using Ductus.FluentDocker.Services;
    using IdentityModel;
    using IdentityModel.AspNetCore.OAuth2Introspection;
    using IdentityModel.Client;
    using Microsoft.Extensions.Configuration;

    public partial class IntegrationTests
    {
        private const string ClientId = "acmClient";
        private const string ClientSecret = "secret";

        private readonly IConfigurationRoot _configuration;
        private readonly OAuth2IntrospectionOptions _oAuth2IntrospectionOptions;

        private static readonly Lazy<ICompositeService> IdentityServerFake =
            new Lazy<ICompositeService>(IdentityServerFakeContainer.Compose);

        public IntegrationTests()
        {
            _configuration = new ConfigurationBuilder()
                .SetBasePath(Directory.GetCurrentDirectory())
                .AddJsonFile("appsettings.json")
                .AddJsonFile($"appsettings.{Environment.MachineName.ToLowerInvariant()}.json", optional: true)
                .Build();

            _oAuth2IntrospectionOptions =
                _configuration.GetSection(nameof(OAuth2IntrospectionOptions)).Get<OAuth2IntrospectionOptions>()!;

            // Invoke the lazy member for it be initialized and run the docker container.
            var _ = IdentityServerFake.Value;
        }

        private async Task<string> GetAccessToken(string scopes)
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
    }
}
