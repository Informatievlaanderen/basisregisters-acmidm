namespace AcmIdmConsumer.MinimalApi
{
    using System.Collections.Generic;

    public class AcmIdmPolicyOptions
    {
        public IEnumerable<string> AllowedScopeValues { get; set; }
    }
}
