namespace Be.Vlaanderen.Basisregisters.Auth
{
    using Microsoft.Extensions.DependencyInjection;

    public static class AuthorizationBuilderExtensions
    {
        public static AuthorizationBuilder AddAuthorizationBuilder(this IServiceCollection services) => new AuthorizationBuilder(services.AddAuthorization());
    }
}
