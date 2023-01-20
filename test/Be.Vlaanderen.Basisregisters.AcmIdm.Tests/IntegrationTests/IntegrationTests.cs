namespace Be.Vlaanderen.Basisregisters.AcmIdm.Tests.IntegrationTests
{
    using Xunit;

    public partial class IntegrationTests : IClassFixture<IntegrationTestFixture>
    {
        private const string AdresBeheerScope = "dv_ar_adres_beheer";
        private const string AdresUitzonderingenScope = "dv_ar_adres_uitzonderingen";
        private const string AdresBeheerAndAdresUitzonderingenScopes = "dv_ar_adres_beheer dv_ar_adres_uitzonderingen";

        private readonly IntegrationTestFixture _fixture;

        public IntegrationTests(IntegrationTestFixture fixture)
        {
            _fixture = fixture;
        }
    }
}
