namespace Be.Vlaanderen.Basisregisters.Auth
{
    using System;
    using System.Collections.Generic;
    using System.Linq;

    public interface IOrganisationWhiteList
    {
        bool IsWhiteListed(string organisation);
    }

    public class OrganisationWhiteList : IOrganisationWhiteList
    {
        private readonly IList<string> _whiteList;

        public OrganisationWhiteList(IList<string>? whiteList)
        {
            _whiteList = whiteList ?? new List<string>();
        }

        public bool IsWhiteListed(string organisation)
        {
            return _whiteList.Contains(organisation, StringComparer.InvariantCultureIgnoreCase);
        }
    }
}
