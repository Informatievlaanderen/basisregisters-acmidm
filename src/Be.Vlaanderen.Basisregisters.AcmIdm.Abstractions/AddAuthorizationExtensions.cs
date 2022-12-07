using Microsoft.Extensions.DependencyInjection;

namespace Be.Vlaanderen.Basisregisters.AcmIdm.Abstractions
{
    using AuthorizationHandlers;
    using Microsoft.AspNetCore.Authorization;
    using Microsoft.AspNetCore.Builder;

    public static class AddAuthorizationExtensions
    {
        //public static TBuilder RequireAcmIdmAuthorization<TBuilder>(this TBuilder builder, params string[] policyNames)
        //    where TBuilder : IEndpointConventionBuilder
        //{
        //    ArgumentNullException.ThrowIfNull(builder);
        //    ArgumentNullException.ThrowIfNull(policyNames);

        //    return builder.RequireAuthorization(policyNames.Select(n => new AuthorizeAttribute(n)).ToArray());
        //}

        public static IServiceCollection AddAcmIdmAuthorization(
            this IServiceCollection services,
            params string[] allowedValues)
        {
            services.AddAuthorizationBuilder()
                .AddPolicy("acm-idm-scopes", policy =>
                    policy
                        .RequireScope(allowedValues));

            return services;
        }

        public static IApplicationBuilder UseAcmIdmAuthorization(
            this IApplicationBuilder builder,
            params string[] allowedValues)
        {
            builder.UseAuthorization();
            AuthorizationPolicy.Combine(new AuthorizationPolicyBuilder()
                .RequireScope(allowedValues)
                .Build());

            return builder;
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

        public static AuthorizationPolicyBuilder RequireScope(this AuthorizationPolicyBuilder builder, params string[] allowedValues) => builder.AddRequirements(new RequiredScopesAuthorizationRequirement(allowedValues));

        public static AuthorizationPolicyBuilder AddOvoCodeRequirement(this AuthorizationPolicyBuilder builder, IOvoCodeValidator ovoCodeValidator) => builder.AddRequirements(new OvoCodeAuthorizationRequirement(ovoCodeValidator));
    }
}
