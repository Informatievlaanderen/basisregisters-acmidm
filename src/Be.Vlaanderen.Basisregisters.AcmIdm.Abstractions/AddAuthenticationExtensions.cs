namespace Be.Vlaanderen.Basisregisters.AcmIdm.Abstractions
{
    using IdentityModel.AspNetCore.OAuth2Introspection;
    using Microsoft.AspNetCore.Authentication;
    using Microsoft.AspNetCore.Authentication.JwtBearer;
    using Microsoft.Extensions.DependencyInjection;

    public static class AddAuthenticationExtensions
    {
        public static AuthenticationBuilder AddAcmIdmAuthentication(this IServiceCollection services)
        {
            return services.AddAuthentication(
                options =>
                {
                    options.DefaultScheme = JwtBearerDefaults.AuthenticationScheme;
                    options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                    options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
                });
        }

        public static AuthenticationBuilder AddAcmIdmIntrospection(
            this AuthenticationBuilder builder,
            OAuth2IntrospectionOptions configOptions)
        {
            return builder.AddOAuth2Introspection(
                JwtBearerDefaults.AuthenticationScheme,
                options =>
                {
                    options.ClientId = configOptions.ClientId;
                    options.ClientSecret = configOptions.ClientSecret;
                    options.Authority = configOptions.Authority;
                    options.IntrospectionEndpoint = configOptions.IntrospectionEndpoint;
                }
            );
        }
    }
}
