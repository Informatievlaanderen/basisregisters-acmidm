namespace Be.Vlaanderen.Basisregisters.AcmIdm
{
    using AuthorizationHandlers;
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
                .AddAcmIdmPolicyIngemetenGebouwInterneBijwerker();

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

        private static AuthorizationBuilder AddPolicy(this AuthorizationBuilder authorizationBuilder, string policyName, string scope)
        {
            return authorizationBuilder.AddPolicy(policyName, policyBuilder => policyBuilder.AddRequirements(new AcmIdmAuthorizationRequirement(new[] { scope })));
        }
    }
}
