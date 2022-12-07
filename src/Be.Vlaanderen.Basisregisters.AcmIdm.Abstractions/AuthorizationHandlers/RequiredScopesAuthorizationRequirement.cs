namespace Be.Vlaanderen.Basisregisters.AcmIdm.Abstractions.AuthorizationHandlers
{
    using System.Collections.Generic;
    using System.Linq;
    using Microsoft.AspNetCore.Authorization;

    public class RequiredScopesAuthorizationRequirement : IAuthorizationRequirement
    {
        public IReadOnlyList<string> AllowedValues { get; }

        public RequiredScopesAuthorizationRequirement(params string[] allowedValues)
        {
            AllowedValues = allowedValues.ToList();
        }
    }
}
