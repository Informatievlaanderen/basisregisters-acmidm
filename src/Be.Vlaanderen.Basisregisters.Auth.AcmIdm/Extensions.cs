namespace Be.Vlaanderen.Basisregisters.Auth.AcmIdm
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using Microsoft.AspNetCore.Http;

    public static class Extensions
    {
        public static string? FindOvoCodeClaim(this HttpContext httpContext)
        {
            var voOvoValue = httpContext.User.FindFirst(AcmIdmClaimTypes.VoOvoCode)?.Value;

            if (voOvoValue is not null)
                return voOvoValue;

            var voOrgValue = httpContext.User.FindFirst(AcmIdmClaimTypes.VoOrgCode)?.Value;

            if (voOrgValue is not null && voOrgValue.StartsWith("ovo", StringComparison.OrdinalIgnoreCase))
                return voOrgValue;

            return null;
        }

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
