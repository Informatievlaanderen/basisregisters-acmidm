namespace Be.Vlaanderen.Basisregisters.Auth.AcmIdm.AuthorizationHandlers
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using Microsoft.AspNetCore.Authorization;

    public class AcmIdmAuthorizationRequirement : IAuthorizationRequirement
    {
        public IReadOnlyList<string> AllowedValues { get; }

        public AcmIdmAuthorizationRequirement(IEnumerable<string> allowedValues)
        {
            var allowedValuesList = allowedValues.ToList();
            if (allowedValuesList.IsNullOrEmpty())
            {
                throw new ArgumentNullException(nameof(allowedValues));
            }

            AllowedValues = allowedValuesList;
        }
    }
}
