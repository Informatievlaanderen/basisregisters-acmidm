namespace Be.Vlaanderen.Basisregisters.Auth.AcmIdm
{
    using IdentityModel.AspNetCore.OAuth2Introspection;
    using Microsoft.AspNetCore.Authentication;
    using Microsoft.AspNetCore.Authentication.JwtBearer;
    using Microsoft.Extensions.DependencyInjection;

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
            return services
                .AddAuthentication(options =>
                {
                    options.DefaultScheme = JwtBearerDefaults.AuthenticationScheme;
                    options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                    options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
                })
                .AddOAuth2Introspection(JwtBearerDefaults.AuthenticationScheme, options =>
                {
                    options.Authority = oAuth2IntrospectionOptions.Authority;
                    options.ClientId = oAuth2IntrospectionOptions.ClientId;
                    options.ClientSecret = oAuth2IntrospectionOptions.ClientSecret;
                    options.IntrospectionEndpoint = oAuth2IntrospectionOptions.IntrospectionEndpoint;

                    options.CacheDuration = oAuth2IntrospectionOptions.CacheDuration;
                    options.CacheKeyPrefix = oAuth2IntrospectionOptions.CacheKeyPrefix;
                    options.EnableCaching = oAuth2IntrospectionOptions.EnableCaching;
                    options.CacheKeyGenerator = oAuth2IntrospectionOptions.CacheKeyGenerator;
                });
        }
    }
}
