namespace Be.Vlaanderen.Basisregisters.AcmIdm.Abstractions
{
    using System;
    using System.Linq;
    using AuthorizationHandlers;
    using Microsoft.AspNetCore.Authorization;
    using Microsoft.AspNetCore.Builder;

    public static class AddAuthorizationExtensions
    {
        public static TBuilder RequireAcmIdmAuthorization<TBuilder>(this TBuilder builder, params string[] policyNames)
            where TBuilder : IEndpointConventionBuilder
        {
            if (builder == null)
            {
                throw new ArgumentNullException(nameof(builder));
            }

            if (policyNames == null)
            {
                throw new ArgumentNullException(nameof(policyNames));
            }

            return builder.RequireAuthorization(policyNames.Select(n => new AuthorizeAttribute(n)).ToArray());
        }

        public static IApplicationBuilder UseAcmIdmAuthorization(
            this IApplicationBuilder applicationBuilder)
        {
            applicationBuilder.UseAuthorization();
            AuthorizationPolicy.Combine(new AuthorizationPolicyBuilder().AddScopesRequirement().Build());

            return applicationBuilder;
        }

        public static AuthorizationOptions AddRequiredScopesPolicy(
            this AuthorizationOptions options,
            string policyName,
            params string[] allowedValues)
        {
            options.AddPolicy(
                policyName,
                b => b.AddScopesRequirement(allowedValues));

            // // Same as
            // options.AddPolicy("", b => b.RequireClaim(ClaimTypes.Scope, allowedValues));

            return options;
        }

        private static AuthorizationPolicyBuilder AddScopesRequirement(
            this AuthorizationPolicyBuilder builder,
            params string[] scopes)
        {
            // Same as
            // builder.RequireClaim(ClaimTypes.Scope, scopes);

            return builder.AddRequirements(new RequiredScopesAuthorizationRequirement(scopes));
        }

        public static AuthorizationPolicyBuilder AddOvoCodeRequirement(
            this AuthorizationPolicyBuilder builder,
            IOvoCodeValidator ovoCodeValidator)
        {
            return builder.AddRequirements(new OvoCodeAuthorizationRequirement(ovoCodeValidator));
        }
    }
}
