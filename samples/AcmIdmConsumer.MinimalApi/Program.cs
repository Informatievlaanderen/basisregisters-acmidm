namespace AcmIdmConsumer.MinimalApi
{
    using System;
    using System.Linq;
    using System.Security.Claims;
    using Be.Vlaanderen.Basisregisters.AcmIdm;
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

            var oAuth2IntrospectionOptions =
                builder.Configuration.GetSection(nameof(OAuth2IntrospectionOptions)).Get<OAuth2IntrospectionOptions>();
            var acmIdmPolicyOptions =
                builder.Configuration.GetSection(nameof(AcmIdmPolicyOptions)).Get<AcmIdmPolicyOptions>();

            builder.Services.AddAcmIdmAuthentication(oAuth2IntrospectionOptions!);
            builder.Services.AddAcmIdmAuthorization(PolicyNames.AcmIdmPolicy, acmIdmPolicyOptions.AllowedScopeValues);

            var app = builder.Build();

            app
                .MapGet("/secret", (ClaimsPrincipal user) =>
                {
                    user.Claims.ToList()
                        .ForEach(x => Console.WriteLine($"{x.Type}: {x.Value}"));
                    return "Joow";
                })
                .RequireAuthorization(PolicyNames.AcmIdmPolicy);

            app.UseAuthentication();
            app.UseAuthorization();

            app.Run();
        }
    }
}
