namespace Be.Vlaanderen.Basisregisters.AcmIdm.AuthorizationHandlers
{
    using System.Collections.Generic;
    using System.Linq;
    using Microsoft.AspNetCore.Authorization;

    public class RequiredScopesAuthorizationRequirement : IAuthorizationRequirement
    {
        public IReadOnlyList<string> AllowedValues { get; }

        public RequiredScopesAuthorizationRequirement(IEnumerable<string> allowedValues)
        {
            AllowedValues = allowedValues.ToList();
        }
    }
}
