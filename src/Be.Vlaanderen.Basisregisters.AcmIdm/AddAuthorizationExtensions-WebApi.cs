namespace Be.Vlaanderen.Basisregisters.AcmIdm
{
    using AuthorizationHandlers;
    using Microsoft.AspNetCore.Authorization;
    using Microsoft.Extensions.DependencyInjection;

    public static partial class AddAuthorizationExtensions
    {
        public static AuthorizationOptions AddAcmIdmAuthorization(this AuthorizationOptions options)
        {
            options
                .AddAcmIdmPolicyAdresDecentraleBijwerker()
                .AddAcmIdmPolicyAdresInterneBijwerker()
                .AddAcmIdmPolicyGeschetstGebouwDecentraleBijwerker()
                .AddAcmIdmPolicyGeschetstGebouwOmgeving()
                .AddAcmIdmPolicyGeschetstGebouwInterneBijwerker()
                .AddAcmIdmPolicyIngemetenGebouwGrbBijwerker()
                .AddAcmIdmPolicyIngemetenGebouwInterneBijwerker();

            return options;
        }

        public static IServiceCollection AddAcmIdmAuthorizationHandlers(this IServiceCollection services)
        {
            return services.AddSingleton<IAuthorizationHandler, AcmIdmAuthorizationHandler>();
        }

        private static AuthorizationOptions AddAcmIdmPolicyAdresDecentraleBijwerker(this AuthorizationOptions authorizationOptions)
        {
            return authorizationOptions.AddPolicy(PolicyNames.Adres.DecentraleBijwerker, Scopes.DvArAdresBeheer);
        }

        private static AuthorizationOptions AddAcmIdmPolicyAdresInterneBijwerker(this AuthorizationOptions options)
        {
            return options.AddPolicy(PolicyNames.Adres.InterneBijwerker, Scopes.DvArAdresUitzonderingen);
        }

        private static AuthorizationOptions AddAcmIdmPolicyGeschetstGebouwDecentraleBijwerker(this AuthorizationOptions authorizationOptions)
        {
            return authorizationOptions.AddPolicy(PolicyNames.GeschetstGebouw.DecentraleBijwerker, Scopes.DvGrGeschetstgebouwBeheer);
        }

        private static AuthorizationOptions AddAcmIdmPolicyGeschetstGebouwOmgeving(this AuthorizationOptions authorizationOptions)
        {
            return authorizationOptions.AddPolicy(PolicyNames.GeschetstGebouw.Omgeving, Scopes.DvGrGeschetstgebouwBeheer);
        }

        private static AuthorizationOptions AddAcmIdmPolicyGeschetstGebouwInterneBijwerker(this AuthorizationOptions authorizationOptions)
        {
            return authorizationOptions.AddPolicy(PolicyNames.GeschetstGebouw.InterneBijwerker, Scopes.DvGrGeschetstgebouwUitzonderingen);
        }

        private static AuthorizationOptions AddAcmIdmPolicyIngemetenGebouwGrbBijwerker(this AuthorizationOptions authorizationOptions)
        {
            return authorizationOptions.AddPolicy(PolicyNames.IngemetenGebouw.GrbBijwerker, Scopes.DvGrIngemetengebouwBeheer);
        }

        private static AuthorizationOptions AddAcmIdmPolicyIngemetenGebouwInterneBijwerker(this AuthorizationOptions authorizationOptions)
        {
            return authorizationOptions.AddPolicy(PolicyNames.IngemetenGebouw.InterneBijwerker, Scopes.DvGrIngemetengebouwUitzonderingen);
        }

        private static AuthorizationOptions AddPolicy(this AuthorizationOptions options, string policyName, string scope)
        {
            options.AddPolicy(policyName, policyBuilder => policyBuilder.AddRequirements(new AcmIdmAuthorizationRequirement(new[] { scope })));

            return options;
        }
    }
}
