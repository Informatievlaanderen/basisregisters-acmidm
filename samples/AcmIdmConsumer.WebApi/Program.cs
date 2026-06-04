namespace AcmIdmConsumer.WebApi
{
    using Be.Vlaanderen.Basisregisters.Api;
    using Microsoft.AspNetCore.Hosting;
    using Microsoft.Extensions.Hosting;

    public static class Program
    {
        public static void Main(string[] args) => CreateWebHostBuilder(args).Build().Run();

        public static IHostBuilder CreateWebHostBuilder(string[] args)
        {
            return new HostBuilder()
                .ConfigureWebHost(webHostBuilder =>
                {
                    webHostBuilder
                        .UseDefaultForApi<Startup>(
                            new ProgramOptions
                            {
                                Hosting =
                                {
                                    HttpPort = 10001
                                },
                                Logging =
                                {
                                    WriteTextToConsole = false,
                                    WriteJsonToConsole = false
                                },
                                Runtime =
                                {
                                    CommandLineArgs = args
                                }
                            });
                });
        }
    }
}
