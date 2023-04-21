namespace Be.Vlaanderen.Basisregisters.Auth.Tests
{
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading;
    using System.Threading.Tasks;
    using Be.Vlaanderen.Basisregisters.Auth;
    using FluentAssertions;
    using Moq;
    using NisCodeService.Abstractions;
    using Xunit;

    public class NisCodeAuthorizerTests
    {
        private readonly NisCodeAuthorizer<int> _authorizer;
        private readonly Mock<INisCodeFinder<int>> _nisCodeFinder;
        private readonly Mock<INisCodeService> _nisCodeService;
        private readonly Mock<IOvoCodeWhiteList> _ovoCodeWhiteList;

        private readonly IDictionary<string, string> _ovoCodeToNisCodes = new Dictionary<string, string>
        {
            { "OVO002000", "44000" },
            { "OVO002001", "44001" },
            { "OVO002002", "44002" },
        };

        public NisCodeAuthorizerTests()
        {
            _nisCodeService = new Mock<INisCodeService>();
            _nisCodeFinder = new Mock<INisCodeFinder<int>>();
            _ovoCodeWhiteList = new Mock<IOvoCodeWhiteList>();
            _authorizer = new NisCodeAuthorizer<int>(
                _nisCodeFinder.Object,
                _nisCodeService.Object,
                _ovoCodeWhiteList.Object);

            foreach (var (ovoCode, nisCode) in _ovoCodeToNisCodes)
            {
                _nisCodeService
                    .Setup(x => x.Get(ovoCode, CancellationToken.None))
                    .ReturnsAsync(nisCode);
            }
        }

        [Fact]
        public async Task WhenOvoCodeClaimIsMissing_ThenIsNotAuthorized()
        {
            var isAuthorized = await _authorizer.IsAuthorized(
                null,
                1,
                CancellationToken.None);

            isAuthorized.Should().BeFalse();
        }

        [Fact]
        public async Task WhenNoNisCodeFoundForOvoCode_ThenIsNotAuthorized()
        {
            var isAuthorized = await _authorizer.IsAuthorized(
                "OVO003000",
                1,
                CancellationToken.None);

            isAuthorized.Should().BeFalse();
        }

        [Fact]
        public async Task WhenNoNisCodeFoundForRequest_ThenIsNotAuthorized()
        {
            var isAuthorized = await _authorizer.IsAuthorized(
                _ovoCodeToNisCodes.First().Key,
                1,
                CancellationToken.None);

            isAuthorized.Should().BeFalse();
        }

        [Fact]
        public async Task WhenNisCodeFromOvoCodeDoesNotMatchNisCodeFoundForRequest_ThenIsNotAuthorized()
        {
            var ovoCodeFromClaim = _ovoCodeToNisCodes.First().Key;
            var nisCodeFromRequest = _ovoCodeToNisCodes.Skip(1).First().Value;

            var intFromRequest = 1;

            _nisCodeFinder
                .Setup(x => x.FindAsync(intFromRequest, CancellationToken.None))
                .ReturnsAsync(nisCodeFromRequest);

            var isAuthorized = await _authorizer.IsAuthorized(
                ovoCodeFromClaim,
                intFromRequest,
                CancellationToken.None);

            isAuthorized.Should().BeFalse();
        }

        [Fact]
        public async Task WhenOvoCodeIsWhiteListed_ThenIsAuthorized()
        {
            var ovoCodeFromClaim = _ovoCodeToNisCodes.First().Key;

            _ovoCodeWhiteList
                .Setup(x => x.IsWhiteListed(ovoCodeFromClaim))
                .Returns(true);

            var isAuthorized = await _authorizer.IsAuthorized(
                ovoCodeFromClaim,
                1,
                CancellationToken.None);

            isAuthorized.Should().BeTrue();
        }

        [Fact]
        public async Task WhenNisCodeFromOvoCodeMatchesNisCodeFoundForRequest_ThenIsAuthorized()
        {
            var ovoCodeFromClaim = _ovoCodeToNisCodes.First().Key;
            var nisCodeFromRequest = _ovoCodeToNisCodes.First().Value;

            var intFromRequest = 1;

            _nisCodeFinder
                .Setup(x => x.FindAsync(intFromRequest, CancellationToken.None))
                .ReturnsAsync(nisCodeFromRequest);

            var isAuthorized = await _authorizer.IsAuthorized(
                ovoCodeFromClaim,
                intFromRequest,
                CancellationToken.None);

            isAuthorized.Should().BeTrue();
        }
    }
}
