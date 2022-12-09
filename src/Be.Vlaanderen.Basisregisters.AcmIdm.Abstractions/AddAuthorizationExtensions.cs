namespace Be.Vlaanderen.Basisregisters.AcmIdm.Abstractions
{
    using AuthorizationHandlers;
    using Microsoft.AspNetCore.Authorization;
    using Microsoft.Extensions.DependencyInjection;

    public static class AddAuthorizationExtensions
    {
        // used by minimal api without Be.Vlaanderen.Basisregisters.Api
        public static IServiceCollection AddAcmIdmAuthorization(
            this IServiceCollection services,
            string policyName,
            params string[] allowedValues)
        {
            services
                .AddAuthorizationBuilder()
                .AddPolicy(policyName, policy =>
                    policy.AddAllowedScopeRequirement(allowedValues));

            services.AddSingleton<IAuthorizationHandler, RequiredScopesAuthorizationHandler>();

            return services;
        }

        // used by projects using Be.Vlaanderen.Basisregisters.Api
        public static AuthorizationOptions AddAcmIdmAuthorization(
            this AuthorizationOptions options,
            string policyName,
            params string[] allowedValues)
        {
            options.AddPolicy(
                policyName,
                b => b.AddAllowedScopeRequirement(allowedValues));

            return options;
        }

        public static AuthorizationPolicyBuilder AddAllowedScopeRequirement(
            this AuthorizationPolicyBuilder builder,
            params string[] allowedValues) => builder.AddRequirements(new RequiredScopesAuthorizationRequirement(allowedValues));

        public static AuthorizationPolicyBuilder AddOvoCodeRequirement(
            this AuthorizationPolicyBuilder builder,
            IOvoCodeValidator ovoCodeValidator) => builder.AddRequirements(new OvoCodeAuthorizationRequirement(ovoCodeValidator));
    }
}
