namespace Be.Vlaanderen.Basisregisters.AcmIdm.AuthorizationHandlers
{
    using System.Collections.Generic;
    using System.Linq;
    using Microsoft.AspNetCore.Authorization;

    public class AcmIdmAuthorizationRequirement : IAuthorizationRequirement
    {
        public IReadOnlyList<string> AllowedValues { get; }

        public AcmIdmAuthorizationRequirement(IEnumerable<string> allowedValues)
        {
            AllowedValues = allowedValues.ToList();
        }
    }
}
