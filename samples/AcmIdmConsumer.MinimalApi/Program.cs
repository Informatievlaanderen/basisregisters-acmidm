namespace AcmIdmConsumer.MinimalApi
{
    using System;
    using System.Linq;
    using System.Security.Claims;
    using Be.Vlaanderen.Basisregisters.Auth.AcmIdm;
    using IdentityModel.AspNetCore.OAuth2Introspection;
    using Microsoft.AspNetCore.Builder;
    using Microsoft.Extensions.Configuration;

    public class Program
    {
        protected Program()
        { }

        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            builder.Configuration
                .AddJsonFile("appsettings.json")
                .AddJsonFile($"appsettings.{builder.Environment.EnvironmentName.ToLowerInvariant()}.json", optional: true, reloadOnChange: false)
                .AddJsonFile($"appsettings.{Environment.MachineName.ToLowerInvariant()}.json", optional: true, reloadOnChange: false)
                .AddEnvironmentVariables()
                .AddCommandLine(args);

            var oAuth2IntrospectionOptions = builder.Configuration.GetSection(nameof(OAuth2IntrospectionOptions)).Get<OAuth2IntrospectionOptions>();

            builder.Services.AddAcmIdmAuthentication(oAuth2IntrospectionOptions!);
            builder.Services.AddAcmIdmAuthorization();

            var app = builder.Build();

            app
                .MapGet("/secret", (ClaimsPrincipal user) =>
                {
                    user.Claims.ToList()
                        .ForEach(x => Console.WriteLine($"{x.Type}: {x.Value}"));
                    return "Joow";
                })
                .RequireAuthorization(PolicyNames.Adres.DecentraleBijwerker);

            app
                .MapGet("/secret/very", (ClaimsPrincipal user) =>
                {
                    user.Claims.ToList()
                        .ForEach(x => Console.WriteLine($"{x.Type}: {x.Value}"));
                    return "Joow";
                })
                .RequireAuthorization(PolicyNames.Adres.DecentraleBijwerker)
                .RequireAuthorization(PolicyNames.Adres.InterneBijwerker);

            app.UseAuthentication();
            app.UseAuthorization();

            app.Run();
        }
    }
}
