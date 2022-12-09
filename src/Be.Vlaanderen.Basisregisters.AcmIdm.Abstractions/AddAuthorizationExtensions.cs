namespace Be.Vlaanderen.Basisregisters.AcmIdm.Abstractions
{
    using AuthorizationHandlers;
    using Microsoft.AspNetCore.Authorization;
    using Microsoft.Extensions.DependencyInjection;

    public static class AddAuthorizationExtensions
    {
        public static IServiceCollection AddAcmIdmAuthorization(
            this IServiceCollection services,
            params string[] allowedValues)
        {
            services.AddAuthorizationBuilder()
                .AddPolicy("acm-idm-scopes", policy =>
                    policy
                        .RequireScope(allowedValues));

            services.AddSingleton<IAuthorizationHandler, RequiredScopesAuthorizationHandler>();

            return services;
        }

        public static AuthorizationOptions AddRequiredScopesPolicy(
            this AuthorizationOptions options,
            string policyName,
            params string[] allowedValues)
        {
            options.AddPolicy(
                policyName,
                b => b.RequireScope(allowedValues));

            return options;
        }

        public static AuthorizationPolicyBuilder RequireScope(
            this AuthorizationPolicyBuilder builder,
            params string[] allowedValues) => builder.AddRequirements(new RequiredScopesAuthorizationRequirement(allowedValues));

        public static AuthorizationPolicyBuilder AddOvoCodeRequirement(
            this AuthorizationPolicyBuilder builder,
            IOvoCodeValidator ovoCodeValidator) => builder.AddRequirements(new OvoCodeAuthorizationRequirement(ovoCodeValidator));
    }
}
