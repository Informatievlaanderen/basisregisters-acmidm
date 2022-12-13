namespace Be.Vlaanderen.Basisregisters.AcmIdm.Tests.ContainerHelper
{
    using System;
    using System.IO;
    using Ductus.FluentDocker.Builders;
    using Ductus.FluentDocker.Services;

    public static class IdentityServerFakeContainer
    {
        public static ICompositeService Compose()
        {
            var fileName = Path.Combine(Directory.GetCurrentDirectory(), "identityserverfake_test.yml");

            return new Builder()
                .UseContainer()
                .UseCompose()
                .FromFile(fileName)
                .RemoveOrphans()
                .Build()
                .Start();
        }
    }
}
