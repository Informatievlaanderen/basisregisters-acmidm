namespace Be.Vlaanderen.Basisregisters.AcmIdm.AuthorizationHandlers
{
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
