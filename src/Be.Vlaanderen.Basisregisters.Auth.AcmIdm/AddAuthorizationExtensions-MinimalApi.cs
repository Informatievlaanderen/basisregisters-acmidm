namespace Be.Vlaanderen.Basisregisters.Auth.AcmIdm
{
    using AuthorizationHandlers;
    using Be.Vlaanderen.Basisregisters.Auth;
    using Microsoft.AspNetCore.Authorization;
    using Microsoft.Extensions.DependencyInjection;

    public static partial class AddAuthorizationExtensions
    {
        public static IServiceCollection AddAcmIdmAuthorization(this IServiceCollection services)
        {
            services
                .AddAuthorizationBuilder()
                .AddAcmIdmPolicyAdresDecentraleBijwerker()
                .AddAcmIdmPolicyAdresInterneBijwerker()
                .AddAcmIdmPolicyGeschetstGebouwDecentraleBijwerker()
                .AddAcmIdmPolicyGeschetstGebouwOmgeving()
                .AddAcmIdmPolicyGeschetstGebouwInterneBijwerker()
                .AddAcmIdmPolicyIngemetenGebouwGrbBijwerker()
                .AddAcmIdmPolicyIngemetenGebouwInterneBijwerker()
                .AddAcmIdmPolicyGeschetsteWegBeheerder()
                .AddAcmIdmPolicyIngemetenWegBeheerder()
                .AddAcmIdmPolicyWegenAttribuutWaardenBeheerder()
                .AddAcmIdmPolicyWegenUitzonderingenBeheerder();

            services.AddSingleton<IAuthorizationHandler, AcmIdmAuthorizationHandler>();

            return services;
        }

        private static AuthorizationBuilder AddAcmIdmPolicyAdresDecentraleBijwerker(this AuthorizationBuilder authorizationBuilder)
        {
            return authorizationBuilder.AddPolicy(PolicyNames.Adres.DecentraleBijwerker, Scopes.DvArAdresBeheer);
        }

        private static AuthorizationBuilder AddAcmIdmPolicyAdresInterneBijwerker(this AuthorizationBuilder authorizationBuilder)
        {
            return authorizationBuilder.AddPolicy(PolicyNames.Adres.InterneBijwerker, Scopes.DvArAdresUitzonderingen);
        }

        private static AuthorizationBuilder AddAcmIdmPolicyGeschetstGebouwDecentraleBijwerker(this AuthorizationBuilder authorizationBuilder)
        {
            return authorizationBuilder.AddPolicy(PolicyNames.GeschetstGebouw.DecentraleBijwerker, Scopes.DvGrGeschetstgebouwBeheer);
        }

        private static AuthorizationBuilder AddAcmIdmPolicyGeschetstGebouwOmgeving(this AuthorizationBuilder authorizationBuilder)
        {
            return authorizationBuilder.AddPolicy(PolicyNames.GeschetstGebouw.Omgeving, Scopes.DvGrGeschetstgebouwBeheer);
        }

        private static AuthorizationBuilder AddAcmIdmPolicyGeschetstGebouwInterneBijwerker(this AuthorizationBuilder authorizationBuilder)
        {
            return authorizationBuilder.AddPolicy(PolicyNames.GeschetstGebouw.InterneBijwerker, Scopes.DvGrGeschetstgebouwUitzonderingen);
        }

        private static AuthorizationBuilder AddAcmIdmPolicyIngemetenGebouwGrbBijwerker(this AuthorizationBuilder authorizationBuilder)
        {
            return authorizationBuilder.AddPolicy(PolicyNames.IngemetenGebouw.GrbBijwerker, Scopes.DvGrIngemetengebouwBeheer);
        }

        private static AuthorizationBuilder AddAcmIdmPolicyIngemetenGebouwInterneBijwerker(this AuthorizationBuilder authorizationBuilder)
        {
            return authorizationBuilder.AddPolicy(PolicyNames.IngemetenGebouw.InterneBijwerker, Scopes.DvGrIngemetengebouwUitzonderingen);
        }

        private static AuthorizationBuilder AddAcmIdmPolicyGeschetsteWegBeheerder(this AuthorizationBuilder authorizationBuilder)
        {
            return authorizationBuilder.AddPolicy(PolicyNames.GeschetsteWeg.Beheerder, Scopes.DvWrGeschetsteWegBeheer);
        }

        private static AuthorizationBuilder AddAcmIdmPolicyIngemetenWegBeheerder(this AuthorizationBuilder authorizationBuilder)
        {
            return authorizationBuilder.AddPolicy(PolicyNames.IngemetenWeg.Beheerder, Scopes.DvWrIngemetenWegBeheer);
        }

        private static AuthorizationBuilder AddAcmIdmPolicyWegenAttribuutWaardenBeheerder(this AuthorizationBuilder authorizationBuilder)
        {
            return authorizationBuilder.AddPolicy(PolicyNames.WegenAttribuutWaarden.Beheerder, Scopes.DvWrAttribuutWaardenBeheer);
        }

        private static AuthorizationBuilder AddAcmIdmPolicyWegenUitzonderingenBeheerder(this AuthorizationBuilder authorizationBuilder)
        {
            return authorizationBuilder.AddPolicy(PolicyNames.WegenUitzonderingen.Beheerder, Scopes.DvWrUitzonderingenBeheer);
        }

        private static AuthorizationBuilder AddPolicy(this AuthorizationBuilder authorizationBuilder, string policyName, string scope)
        {
            return authorizationBuilder.AddPolicy(policyName, policyBuilder => policyBuilder.AddRequirements(new AcmIdmAuthorizationRequirement(new[] { scope })));
        }
    }
}
