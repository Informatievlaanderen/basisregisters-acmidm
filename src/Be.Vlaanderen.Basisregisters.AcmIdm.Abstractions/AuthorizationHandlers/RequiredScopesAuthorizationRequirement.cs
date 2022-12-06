namespace Be.Vlaanderen.Basisregisters.AcmIdm.Abstractions.AuthorizationHandlers
{
    using System.Collections.Generic;
    using System.Linq;
    using Microsoft.AspNetCore.Authorization;

    public class RequiredScopesAuthorizationRequirement : IAuthorizationRequirement
    {
        public IReadOnlyList<string> Scopes { get; }

        public RequiredScopesAuthorizationRequirement(params string[] scopes)
        {
            Scopes = scopes.ToList();
        }
    }
}
