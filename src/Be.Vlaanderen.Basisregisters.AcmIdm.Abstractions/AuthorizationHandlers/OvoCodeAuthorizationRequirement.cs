namespace Be.Vlaanderen.Basisregisters.AcmIdm.Abstractions.AuthorizationHandlers
{
    using System;
    using Microsoft.AspNetCore.Authorization;

    public class OvoCodeAuthorizationRequirement : IAuthorizationRequirement
    {
        public IOvoCodeValidator OvoCodeValidator { get; }

        public OvoCodeAuthorizationRequirement(IOvoCodeValidator ovoCodeValidator)
        {
            OvoCodeValidator = ovoCodeValidator;
        }
    }
}
