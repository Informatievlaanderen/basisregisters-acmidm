namespace Be.Vlaanderen.Basisregisters.AcmIdm.Abstractions
{
    using System;
    using System.Collections.Generic;
    using AuthorizationHandlers;
    using Microsoft.AspNetCore.Authorization;
    using Microsoft.Extensions.DependencyInjection;

    public static class AddAuthorizationExtensions
    {
        // used by minimal api without Be.Vlaanderen.Basisregisters.Api
        public static IServiceCollection AddAcmIdmAuthorization(
            this IServiceCollection services,
            string policyName,
            IEnumerable<string> allowedValues,
            Action<AuthorizationPolicyBuilder>? optionalRequirements = null)
        {
            services
                .AddAuthorizationBuilder()
                .AddPolicy(policyName, policyBuilder =>
                {
                    policyBuilder.AddAllowedScopeRequirement(allowedValues);

                    optionalRequirements?.Invoke(policyBuilder);
                });

            services.AddSingleton<IAuthorizationHandler, RequiredScopesAuthorizationHandler>();

            return services;
        }

        // used by projects using Be.Vlaanderen.Basisregisters.Api
        public static AuthorizationOptions AddAcmIdmAuthorization(
            this AuthorizationOptions options,
            string policyName,
            IEnumerable<string> allowedValues,
            Action<AuthorizationPolicyBuilder>? optionalRequirements = null)
        {
            options.AddPolicy(
                policyName,
                policyBuilder =>
                {
                    policyBuilder.AddAllowedScopeRequirement(allowedValues);

                    optionalRequirements?.Invoke(policyBuilder);
                });

            return options;
        }

        public static AuthorizationPolicyBuilder AddAllowedScopeRequirement(
            this AuthorizationPolicyBuilder builder,
            IEnumerable<string> allowedValues) => builder.AddRequirements(new RequiredScopesAuthorizationRequirement(allowedValues));

        public static AuthorizationPolicyBuilder AddOvoCodeRequirement(
            this AuthorizationPolicyBuilder builder,
            IOvoCodeValidator ovoCodeValidator) => builder.AddRequirements(new OvoCodeAuthorizationRequirement(ovoCodeValidator));
    }
}
