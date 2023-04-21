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

        public static bool IsNullOrEmpty<T>(this IEnumerable<T>? enumerable) => enumerable is null || !enumerable.Any();
    }
}
