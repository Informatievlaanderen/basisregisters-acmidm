namespace Be.Vlaanderen.Basisregisters.Auth.AcmIdm.AuthorizationHandlers
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using Microsoft.AspNetCore.Authorization;

    public class AcmIdmAuthorizationRequirement : IAuthorizationRequirement
    {
        public IReadOnlyList<string> AllowedScopes { get; }
        public IReadOnlyList<string> BlacklistedOvoCodes { get; }

        public AcmIdmAuthorizationRequirement(
            IEnumerable<string> allowedScopes)
            : this(allowedScopes, [])
        { }

        public AcmIdmAuthorizationRequirement(
            IEnumerable<string> allowedScopes,
            IEnumerable<string> blacklistedOvoCodes)
        {
            var allowedScopesList = allowedScopes.ToList();
            if (allowedScopesList.IsNullOrEmpty())
            {
                throw new ArgumentNullException(nameof(allowedScopes));
            }

            AllowedScopes = allowedScopesList;
            BlacklistedOvoCodes = blacklistedOvoCodes.ToList();
        }
    }
}
