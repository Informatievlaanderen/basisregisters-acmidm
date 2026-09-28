namespace Be.Vlaanderen.Basisregisters.Auth.AcmIdm
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Security.Claims;
    using Microsoft.AspNetCore.Http;

    public static class Extensions
    {
        public static string? FindOvoCodeClaim(this ClaimsPrincipal user)
        {
            var voOvoValue = user.FindFirst(AcmIdmClaimTypes.VoOvoCode)?.Value;

            if (!string.IsNullOrWhiteSpace(voOvoValue))
                return voOvoValue;

            var voOrgValue = user.FindOrgCodeClaim();

            if (voOrgValue is not null && voOrgValue.StartsWith("ovo", StringComparison.OrdinalIgnoreCase))
                return voOrgValue;

            var customOrgValue = user.FindCustomOrgOvoCodeClaim();

            if (customOrgValue is not null && customOrgValue.StartsWith("ovo", StringComparison.OrdinalIgnoreCase))
                return customOrgValue;

            return null;
        }

        public static string? FindOvoCodeClaim(this HttpContext httpContext)
            => httpContext.User.FindOvoCodeClaim();

        public static string? FindOrgCodeClaim(this ClaimsPrincipal user)
        {
            return user.FindFirst(AcmIdmClaimTypes.VoOrgCode)?.Value;
        }

        public static string? FindOrgCodeClaim(this HttpContext httpContext)
            => httpContext.User.FindOrgCodeClaim();

        public static string? FindCustomOrgOvoCodeClaim(this ClaimsPrincipal user)
        {
            return user.FindFirst(AcmIdmClaimTypes.CustomOvoCode)?.Value;
        }

        public static string? FindCustomOrgOvoCodeClaim(this HttpContext httpContext)
            => httpContext.User.FindCustomOrgOvoCodeClaim();

        public static bool HasScope(this HttpContext httpContext, string scope)
        {
            return httpContext.User.HasClaim(AcmIdmClaimTypes.Scope, scope);
        }

        public static bool IsInterneBijwerker(this HttpContext httpContext)
        {
            return
                httpContext.HasScope(Scopes.DvArAdresUitzonderingen)
                || httpContext.HasScope(Scopes.DvGrGeschetstgebouwUitzonderingen)
                || httpContext.HasScope(Scopes.DvGrIngemetengebouwUitzonderingen)
                || httpContext.HasScope(Scopes.DvWrUitzonderingenBeheer);
        }

        public static bool IsNullOrEmpty<T>(this IEnumerable<T>? enumerable) => enumerable is null || !enumerable.Any();
    }
}
