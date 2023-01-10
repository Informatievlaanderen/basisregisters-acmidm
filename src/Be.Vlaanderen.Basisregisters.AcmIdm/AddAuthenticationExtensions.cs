namespace Be.Vlaanderen.Basisregisters.AcmIdm
{
    using System.Configuration;
    using IdentityModel.AspNetCore.OAuth2Introspection;
    using Microsoft.AspNetCore.Authentication;
    using Microsoft.AspNetCore.Authentication.JwtBearer;
    using Microsoft.Extensions.Configuration;
    using Microsoft.Extensions.DependencyInjection;
    using NisCodeService.Proxy.HttpProxy;

    public static class AddAuthenticationExtensions
    {
        public static AuthenticationBuilder AddAcmIdmAuthentication(
            this IServiceCollection services,
            string clientId,
            string clientSecret,
            string authority,
            string introspectionEndpoint)
        {
            return services.AddAcmIdmAuthentication(new OAuth2IntrospectionOptions
            {
                ClientId = clientId,
                ClientSecret = clientSecret,
                Authority = authority,
                IntrospectionEndpoint = introspectionEndpoint
            });
        }

        public static AuthenticationBuilder AddAcmIdmAuthentication(
            this IServiceCollection services,
            OAuth2IntrospectionOptions oAuth2IntrospectionOptions)
        {
            var nisCodeServiceUrl = services
                .BuildServiceProvider()
                .GetRequiredService<IConfiguration>()
                .GetValue<string>("NisCodeServiceUrl");
            if (string.IsNullOrWhiteSpace(nisCodeServiceUrl))
            {
                throw new ConfigurationErrorsException("Configuration should have a value for \"NisCodeServiceUrl\".");
            }

            return services
                .AddHttpProxyNisCodeService(nisCodeServiceUrl)
                .AddAuthentication(options =>
                {
                    options.DefaultScheme = JwtBearerDefaults.AuthenticationScheme;
                    options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                    options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
                })
                .AddOAuth2Introspection(JwtBearerDefaults.AuthenticationScheme, options =>
                {
                    options.ClientId = oAuth2IntrospectionOptions.ClientId;
                    options.ClientSecret = oAuth2IntrospectionOptions.ClientSecret;
                    options.Authority = oAuth2IntrospectionOptions.Authority;
                    options.IntrospectionEndpoint = oAuth2IntrospectionOptions.IntrospectionEndpoint;
                });
        }
    }
}
