namespace Be.Vlaanderen.Basisregisters.AcmIdm
{
    using System.Collections.Generic;
    using AuthorizationHandlers;
    using Microsoft.AspNetCore.Authorization;
    using Microsoft.Extensions.DependencyInjection;

    public static class AddAuthorizationExtensions
    {
        private static IServiceCollection AddAcmIdmPolicyAdresDecentraleBijwerker(this IServiceCollection services)
        {
            services
                .AddAuthorizationBuilder()
                .AddPolicy(PolicyNames.AdresDecentraleBijwerker, policyBuilder =>
                {
                    policyBuilder.AddAllowedScopeRequirement(new[] { Scopes.DvArAdresBeheer });
                });

            return services;
        }

        private static IServiceCollection AddAcmIdmPolicyAdresInterneBijwerker(this IServiceCollection services)
        {
            services
                .AddAuthorizationBuilder()
                .AddPolicy(PolicyNames.AdresInterneBijwerker, policyBuilder =>
                {
                    policyBuilder.AddAllowedScopeRequirement(new[] { Scopes.DvArAdresUitzonderingen });
                });

            return services;
        }

        public static IServiceCollection AddAcmIdmAuthorization(this IServiceCollection services)
        {
            services
                .AddAcmIdmPolicyAdresDecentraleBijwerker()
                .AddAcmIdmPolicyAdresInterneBijwerker();

            services.AddSingleton<IAuthorizationHandler, AcmIdmAuthorizationHandler>();

            return services;
        }

        public static AuthorizationOptions AddAcmIdmAuthorization(this AuthorizationOptions options)
        {
            options
                .AddAcmIdmPolicyAdresDecentraleBijwerker()
                .AddAcmIdmPolicyAdresInterneBijwerker();

            return options;
        }

        private static AuthorizationOptions AddAcmIdmPolicyAdresDecentraleBijwerker(this AuthorizationOptions options)
        {
            options.AddPolicy(
                PolicyNames.AdresDecentraleBijwerker,
                policyBuilder => { policyBuilder.AddAllowedScopeRequirement(new[] { Scopes.DvArAdresBeheer }); });

            return options;
        }

        private static AuthorizationOptions AddAcmIdmPolicyAdresInterneBijwerker(this AuthorizationOptions options)
        {
            options.AddPolicy(
                PolicyNames.AdresInterneBijwerker,
                policyBuilder => { policyBuilder.AddAllowedScopeRequirement(new[] { Scopes.DvArAdresUitzonderingen }); });

            return options;
        }

        private static AuthorizationPolicyBuilder AddAllowedScopeRequirement(
            this AuthorizationPolicyBuilder builder,
            IEnumerable<string> allowedValues) =>
            builder.AddRequirements(new AcmIdmAuthorizationRequirement(allowedValues));
    }
}
