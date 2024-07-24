namespace Be.Vlaanderen.Basisregisters.Auth.AcmIdm
{
    using System.Collections.Generic;
    using AuthorizationHandlers;
    using Microsoft.AspNetCore.Authorization;

    public static partial class AddAuthorizationExtensions
    {
        public static AuthorizationBuilder AddAddressPolicies(this AuthorizationBuilder builder, IEnumerable<string> blacklistedOvoCodes)
        {
            builder.AddPolicy(PolicyNames.Adres.DecentraleBijwerker, Scopes.DvArAdresBeheer, blacklistedOvoCodes);
            builder.AddPolicy(PolicyNames.Adres.InterneBijwerker, Scopes.DvArAdresUitzonderingen);

            return builder;
        }

        public static AuthorizationBuilder AddBuildingPolicies(this AuthorizationBuilder builder, IEnumerable<string> blacklistedOvoCodes)
        {
            builder.AddPolicy(PolicyNames.GeschetstGebouw.DecentraleBijwerker, Scopes.DvGrGeschetstgebouwBeheer, blacklistedOvoCodes);
            builder.AddPolicy(PolicyNames.GeschetstGebouw.InterneBijwerker, Scopes.DvGrGeschetstgebouwUitzonderingen);

            builder.AddPolicy(PolicyNames.IngemetenGebouw.GrbBijwerker, Scopes.DvGrIngemetengebouwBeheer);
            builder.AddPolicy(PolicyNames.IngemetenGebouw.InterneBijwerker, Scopes.DvGrIngemetengebouwUitzonderingen);

            return builder;
        }

        public static AuthorizationBuilder AddRoadPolicies(this AuthorizationBuilder builder, IEnumerable<string> blacklistedOvoCodes)
        {
            builder.AddPolicy(PolicyNames.GeschetsteWeg.Beheerder, Scopes.DvWrGeschetsteWegBeheer, blacklistedOvoCodes);
            builder.AddPolicy(PolicyNames.IngemetenWeg.Beheerder, Scopes.DvWrIngemetenWegBeheer);
            builder.AddPolicy(PolicyNames.WegenAttribuutWaarden.Beheerder, Scopes.DvWrAttribuutWaardenBeheer);
            builder.AddPolicy(PolicyNames.WegenUitzonderingen.Beheerder, Scopes.DvWrUitzonderingenBeheer);

            return builder;
        }

        private static AuthorizationBuilder AddPolicy(
            this AuthorizationBuilder builder,
            string policyName,
            string scope)
        {
            builder.AddPolicy(
                policyName,
                policyBuilder => policyBuilder.AddRequirements(new AcmIdmAuthorizationRequirement(new[] { scope })));

            return builder;
        }

        private static AuthorizationBuilder AddPolicy(
            this AuthorizationBuilder builder,
            string policyName,
            string scope,
            IEnumerable<string> blacklistedOvoCodes)
        {
            builder.AddPolicy(
                policyName,
                policyBuilder => policyBuilder.AddRequirements(
                    new AcmIdmAuthorizationRequirement(
                        new[] { scope },
                        blacklistedOvoCodes)));

            return builder;
        }
    }
}
