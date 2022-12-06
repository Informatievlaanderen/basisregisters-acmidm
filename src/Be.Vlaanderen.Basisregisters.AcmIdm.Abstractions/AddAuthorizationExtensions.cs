namespace Be.Vlaanderen.Basisregisters.AcmIdm.Abstractions
{
    using AuthorizationHandlers;
    using Microsoft.AspNetCore.Authorization;

    public static class AddAuthorizationExtensions
    {
        public static AuthorizationOptions AddRequiredScopesPolicy(
            this AuthorizationOptions options,
            string policyName,
            params string[] scopes)
        {
            options.AddPolicy(
                policyName,
                b => b.AddScopesRequirement(scopes));

            return options;
        }

        public static AuthorizationPolicyBuilder AddScopesRequirement(
            this AuthorizationPolicyBuilder builder,
            params string[] scopes)
        {
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
