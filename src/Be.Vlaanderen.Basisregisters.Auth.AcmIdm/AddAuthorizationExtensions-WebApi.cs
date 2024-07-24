namespace Be.Vlaanderen.Basisregisters.Auth.AcmIdm
{
    using System.Collections.Generic;
    using AuthorizationHandlers;
    using Microsoft.AspNetCore.Authorization;
    using Microsoft.Extensions.DependencyInjection;

    public static partial class AddAuthorizationExtensions
    {
        public static IServiceCollection AddAcmIdmAuthorizationHandlers(this IServiceCollection services)
        {
            return services.AddSingleton<IAuthorizationHandler, AcmIdmAuthorizationHandler>();
        }

        public static AuthorizationOptions AddAddressPolicies(this AuthorizationOptions options, IEnumerable<string> blacklistedOvoCodes)
        {
            options.AddPolicy(PolicyNames.Adres.DecentraleBijwerker, Scopes.DvArAdresBeheer, blacklistedOvoCodes);
            options.AddPolicy(PolicyNames.Adres.InterneBijwerker, Scopes.DvArAdresUitzonderingen);

            return options;
        }

        public static AuthorizationOptions AddBuildingPolicies(this AuthorizationOptions options, IEnumerable<string> blacklistedOvoCodes)
        {
            options.AddPolicy(PolicyNames.GeschetstGebouw.DecentraleBijwerker, Scopes.DvGrGeschetstgebouwBeheer, blacklistedOvoCodes);
            options.AddPolicy(PolicyNames.GeschetstGebouw.InterneBijwerker, Scopes.DvGrGeschetstgebouwUitzonderingen);

            options.AddPolicy(PolicyNames.IngemetenGebouw.GrbBijwerker, Scopes.DvGrIngemetengebouwBeheer);
            options.AddPolicy(PolicyNames.IngemetenGebouw.InterneBijwerker, Scopes.DvGrIngemetengebouwUitzonderingen);

            return options;
        }

        public static AuthorizationOptions AddRoadPolicies(this AuthorizationOptions options, IEnumerable<string> blacklistedOvoCodes)
        {
            options.AddPolicy(PolicyNames.GeschetsteWeg.Beheerder, Scopes.DvWrGeschetsteWegBeheer, blacklistedOvoCodes);
            options.AddPolicy(PolicyNames.IngemetenWeg.Beheerder, Scopes.DvWrIngemetenWegBeheer);
            options.AddPolicy(PolicyNames.WegenAttribuutWaarden.Beheerder, Scopes.DvWrAttribuutWaardenBeheer);
            options.AddPolicy(PolicyNames.WegenUitzonderingen.Beheerder, Scopes.DvWrUitzonderingenBeheer);

            return options;
        }

        private static AuthorizationOptions AddPolicy(
            this AuthorizationOptions options,
            string policyName,
            string scope)
        {
            options.AddPolicy(
                policyName,
                policyBuilder => policyBuilder.AddRequirements(new AcmIdmAuthorizationRequirement(new[] { scope })));

            return options;
        }

        private static AuthorizationOptions AddPolicy(
            this AuthorizationOptions options,
            string policyName,
            string scope,
            IEnumerable<string> blacklistedOvoCodes)
        {
            options.AddPolicy(
                policyName,
                policyBuilder => policyBuilder.AddRequirements(
                    new AcmIdmAuthorizationRequirement(
                        new[] { scope },
                        blacklistedOvoCodes)));

            return options;
        }
    }
}
