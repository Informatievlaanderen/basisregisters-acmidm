using Microsoft.Extensions.DependencyInjection;

namespace Be.Vlaanderen.Basisregisters.AcmIdm;

public static class AuthorizationBuilderExtensions
{
    public static AuthorizationBuilder AddAuthorizationBuilder(this IServiceCollection services)
        => new AuthorizationBuilder(services.AddAuthorization());
}
