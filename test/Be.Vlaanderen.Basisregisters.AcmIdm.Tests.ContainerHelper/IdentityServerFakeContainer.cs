namespace Be.Vlaanderen.Basisregisters.AcmIdm.Tests.ContainerHelper
{
    using System;
    using System.IO;
    using Ductus.FluentDocker.Builders;
    using Ductus.FluentDocker.Services;

    public static class IdentityServerFakeContainer
    {
        public static ICompositeService Compose(TimeSpan? timeout = null)
        {
            var fileName = Path.Combine(Directory.GetCurrentDirectory(), "identityserverfake_test.yml");

            const string waitForService = "acmidm-test";
            const string waitForPort = "5052";
            const string waitForProto = "tcp";

            var timeoutMs = timeout?.TotalMilliseconds ?? 30_000;

            return new Builder()
                .UseContainer()
                .UseCompose()
                .FromFile(fileName)
                .RemoveOrphans()
                //.WaitForPort(waitForService, $"{waitForPort}:{waitForProto}", Convert.ToInt64(timeoutMs))
                .Build()
                .Start();
        }
    }
}
