using System;
using System.Linq;
using AcmIdmConsumer.MinimalApi;
using Be.Vlaanderen.Basisregisters.AcmIdm.Abstractions;
using IdentityModel.AspNetCore.OAuth2Introspection;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.Configuration;

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
builder.Services.AddAcmIdmAuthorization(acmIdmPolicyOptions.PolicyName, acmIdmPolicyOptions.AllowedScopeValues.ToArray());

var app = builder.Build();

app
    .MapGet("/secret", () => "Hello")
    .RequireAuthorization(acmIdmPolicyOptions.PolicyName);

app.UseAuthentication();
app.UseAuthorization();

app.Run();
