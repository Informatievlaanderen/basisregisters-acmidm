namespace Be.Vlaanderen.Basisregisters.AcmIdm
{
    using System.Collections.Generic;
    using System.Linq;

    public class AcmIdmPolicyOptions
    {
        public IEnumerable<string> AllowedScopeValues { get; set; } = Enumerable.Empty<string>();
    }
}
