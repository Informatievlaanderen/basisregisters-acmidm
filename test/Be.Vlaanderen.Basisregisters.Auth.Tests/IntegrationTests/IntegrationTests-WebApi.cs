namespace Be.Vlaanderen.Basisregisters.Auth.Tests.IntegrationTests
{
    using System.Net;
    using System.Threading.Tasks;
    using FluentAssertions;
    using Xunit;

    public partial class IntegrationTests
    {
        [Theory]
        [InlineData(AdresBeheerScope)]
        public async Task GivenWebApiSecretsEndpoint_WhenClientIsDecentraleBijwerker_ThenRequestIsAuthorized(string scopes)
        {
            var accessToken = await _fixture.GetAccessToken(scopes);
            var response = await _fixture.GetWebApiAsync("/v1/secret", accessToken);

            response.StatusCode.Should().Be(HttpStatusCode.OK);
        }

        [Theory]
        [InlineData(AdresBeheerAndAdresUitzonderingenScopes)]
        public async Task GivenWebApiSecretsVeryEndpoint_WhenClientIsInterneBijwerker_ThenRequestIsAuthorized(string scopes)
        {
            var accessToken = await _fixture.GetAccessToken(scopes);
            var response = await _fixture.GetWebApiAsync("/v1/secret/very", accessToken);

            response.StatusCode.Should().Be(HttpStatusCode.OK);
        }

        [Theory]
        [InlineData(AdresBeheerScope)]
        [InlineData(AdresUitzonderingenScope)]
        public async Task GivenWebApiSecretsVeryEndpoint_WhenClientHasMissingScope_ThenRequestIsUnauthorized(string scopes)
        {
            var accessToken = await _fixture.GetAccessToken(scopes);
            var response = await _fixture.GetWebApiAsync("/v1/secret/very", accessToken);

            response.StatusCode.Should().Be(HttpStatusCode.Forbidden);
        }
    }
}
