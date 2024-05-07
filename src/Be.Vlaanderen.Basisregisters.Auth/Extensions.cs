namespace Be.Vlaanderen.Basisregisters.Auth
{
    using System.Collections.Generic;
    using Microsoft.Extensions.DependencyInjection;
    using NisCodeService.Abstractions;

    public static class Extensions
    {
        public static IServiceCollection AddNisCodeAuthorization<TId, TFinder>(this IServiceCollection services)
            where TFinder: class, INisCodeFinder<TId>
        {
            return services
                .AddSingleton<INisCodeAuthorizer<TId>, NisCodeAuthorizer<TId>>()
                .AddSingleton<INisCodeFinder<TId>, TFinder>();
        }

        public static IServiceCollection AddOvoCodeWhiteList(this IServiceCollection services, IList<string>? ovoCodeWhiteList)
        {
            return services.AddSingleton<IOvoCodeWhiteList>(new OvoCodeWhiteList(ovoCodeWhiteList));
        }

        public static IServiceCollection AddOrganisationWhiteList(this IServiceCollection services, IList<string>? whiteList)
        {
            return services.AddSingleton<IOrganisationWhiteList>(new OrganisationWhiteList(whiteList));
        }
    }
}
