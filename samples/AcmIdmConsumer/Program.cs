using Be.Vlaanderen.Basisregisters.AcmIdm.Abstractions;
using Be.Vlaanderen.Basisregisters.AcmIdm.Abstractions.AuthenticationHandlers;
using Be.Vlaanderen.Basisregisters.AcmIdm.Abstractions.AuthorizationHandlers;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.DependencyInjection;

var ourScope = new[] { "dv_gr_geschetstgebouw_beheer", "dv_gr_geschetstgebouw_uitzonderingen" };

var builder = WebApplication.CreateBuilder(args);

var clientId = "clientId";
var clientSecret = "clientSecret";
var authority = "authority";
var introspectionEndpoint = "introspectionEndpoint";

builder.Services.AddSingleton<IAuthorizationHandler, RequiredScopesAuthorizationHandler>();
builder.Services.AddSingleton<IAuthenticationHandler, CustomAuthenticationHandler>();
builder.Services.AddAuthentication("Bearer")
    .AddScheme<JwtBearerOptions, CustomAuthenticationHandler>("Bearer", null);
// builder.Services.AddAuthentication(options =>
// {
//     options.DefaultScheme = JwtBearerDefaults.AuthenticationScheme;
//     options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
//     options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
// }).AddJwtBearer();
//builder.Services.AddAcmIdmAuthentication(clientId, clientSecret, authority, introspectionEndpoint);
builder.Services.AddAcmIdmAuthorization(ourScope);

var app = builder.Build();

app.MapGet("/", (HttpContext httpContext) => { })
    .RequireAuthorization("acm-idm-scopes");

app.UseAuthentication();
app.UseAuthorization()
    .UseAcmIdmAuthorization(ourScope);

app.Run();
