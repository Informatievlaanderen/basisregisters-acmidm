namespace AcmIdmConsumer.MinimalApi
{
    using System.Collections.Generic;

    public class AcmIdmPolicyOptions
    {
        public string PolicyName { get; set; }

        public IEnumerable<string> AllowedScopeValues { get; set; }
    }
}
