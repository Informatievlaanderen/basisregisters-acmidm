namespace Be.Vlaanderen.Basisregisters.Auth
{
    using System.Threading;
    using System.Threading.Tasks;
    using NisCodeService.Abstractions;

    public interface INisCodeAuthorizer<TId>
    {
        Task<bool> IsAuthorized(string? ovoCode, TId id, CancellationToken ct);
    }

    public class NisCodeAuthorizer<T> : INisCodeAuthorizer<T>
    {
        private readonly INisCodeFinder<T> _nisCodeFinder;
        private readonly INisCodeService _nisCodeService;
        private readonly IOrganisationWhiteList? _organisationWhiteList;

        public NisCodeAuthorizer(
            INisCodeFinder<T> nisCodeFinder,
            INisCodeService nisCodeService,
            IOrganisationWhiteList? organisationWhiteList = null)
        {
            _nisCodeFinder = nisCodeFinder;
            _nisCodeService = nisCodeService;
            _organisationWhiteList = organisationWhiteList;
        }

        public async Task<bool> IsAuthorized(string? ovoCode, T id, CancellationToken ct)
        {
            if (ovoCode is null)
            {
                return false;
            }

            if (_organisationWhiteList is not null && _organisationWhiteList.IsWhiteListed(ovoCode))
            {
                return true;
            }

            var requestNisCode = await _nisCodeService.Get(ovoCode, ct);
            var streetNameNisCode = await _nisCodeFinder.FindAsync(id, ct);

            return !string.IsNullOrEmpty(requestNisCode) && requestNisCode == streetNameNisCode;
        }
    }
}
