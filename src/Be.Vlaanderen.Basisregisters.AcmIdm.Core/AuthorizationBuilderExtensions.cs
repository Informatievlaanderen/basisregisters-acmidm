namespace Be.Vlaanderen.Basisregisters.AcmIdm.Core;

using Microsoft.Extensions.DependencyInjection;

public static class AuthorizationBuilderExtensions
{
    public static AuthorizationBuilder AddAuthorizationBuilder(this IServiceCollection services)
        => new AuthorizationBuilder(services.AddAuthorization());
}
