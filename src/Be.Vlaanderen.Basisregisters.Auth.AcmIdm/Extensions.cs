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

        /// <summary>
        ///     The scopes of the caller, whichever shape the scope claim arrived in.
        ///
        ///     A scope claim reaches us in one of two shapes, depending on the authentication scheme. A scheme that
        ///     builds the identity itself adds one claim per scope. The OAuth2 introspection scheme maps the
        ///     introspection response verbatim, and ACM/IDM answers that call with every scope of the token in a single
        ///     space separated value. Reading only the first shape silently refuses every token holding more than one
        ///     scope.
        /// </summary>
        public static IEnumerable<string> GetScopes(this ClaimsPrincipal user)
        {
            return user
                .FindAll(AcmIdmClaimTypes.Scope)
                .SelectMany(claim => claim.Value.Split(' ', StringSplitOptions.RemoveEmptyEntries));
        }

        public static IEnumerable<string> GetScopes(this HttpContext httpContext)
            => httpContext.User.GetScopes();

        /// <summary>
        ///     Scope values are case sensitive (RFC 6749 §3.3), so they are compared ordinally.
        /// </summary>
        public static bool HasScope(this ClaimsPrincipal user, string scope)
        {
            return user.GetScopes().Contains(scope, StringComparer.Ordinal);
        }

        public static bool HasScope(this HttpContext httpContext, string scope)
            => httpContext.User.HasScope(scope);

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
