using System;
using Be.Vlaanderen.Basisregisters.AcmIdm.Abstractions;
using IdentityModel.AspNetCore.OAuth2Introspection;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.Configuration;

var ourScope = new[]
{
    "dv_gr_geschetstgebouw_beheer",
    "dv_gr_geschetstgebouw_uitzonderingen"
};

var builder = WebApplication.CreateBuilder(args);

builder.Configuration
    .AddJsonFile("appsettings.json")
    .AddJsonFile($"appsettings.{builder.Environment.EnvironmentName.ToLowerInvariant()}.json", optional: true, reloadOnChange: false)
    .AddJsonFile($"appsettings.{Environment.MachineName.ToLowerInvariant()}.json", optional: true, reloadOnChange: false)
    .AddEnvironmentVariables()
    .AddCommandLine(args);

var oAuth2IntrospectionOptions =
    builder.Configuration.GetSection(nameof(OAuth2IntrospectionOptions)).Get<OAuth2IntrospectionOptions>();

builder.Services.AddAcmIdmAuthentication(oAuth2IntrospectionOptions!);
builder.Services.AddAcmIdmAuthorization(ourScope);

var app = builder.Build();

app.MapGet("/", () => { })
    .RequireAuthorization("acm-idm-scopes");

app.UseAuthentication();
app.UseAuthorization();
    //.UseAcmIdmAuthorization(ourScope);

app.Run();
