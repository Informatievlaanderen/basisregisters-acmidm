# Basisregisters ACM/IDM
ACM/IDM utilities for C#

Adds authentication and authorization to your ASP.NET API following the OAuth2 Client Credentials Grant using [ACM/IDM](https://overheid.vlaanderen.be/acm/idm-standaard-aansluitingsproces) as identity provider. 
More information of the OAuth2 flow implemented can be found [here](https://authenticatie.vlaanderen.be/docs/beveiligen-van-api/oauth-rest/rest-server2server/) (Dutch). 

https://www.nuget.org/packages?q=Be.Vlaanderen.Basisregisters.Auth.AcmIdm

# How to use?

## Web Api sample
The web API sample uses ASP.NET controllers and the nuget [Be.Vlaanderen.Basisregisters.Api](https://github.com/Informatievlaanderen/api) package to perform service and middleware registrations. 
I.a. the nuget package provides a middleware hook to register your authorization policies.

To add ACM/IDM based authentication/authorization following the OAuth2 Client Credentials grant, you have to perform following registrations:

1. Call IServiceCollection.AddAcmIdmAuthentication in `StartUp`
2. Hook into the `Be.Vlaanderen.Basisregisters.Api` middleware as following: `MiddlewareHooks.Authorization -> options.AddAcmIdmAuthorization`
3. You need a separate registration of the used IAuthorizationHandler, namely `RequiredScopesAuthorizationHandler`.
4. Use the `Authorize` attribute on your controller or controller methods as illustrated below:

`[Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme, Policy = PolicyNames.AcmIdmPolicy)]`
`public class SecretController : ControllerBase`

## Minimal API sample
The minimal API sample doesn't make use of nuget package `Be.Vlaanderen.Basisregisters.Api`. And its service registrations are more straightforward.

It suffices to use the following extension methods on IServiceCollection:
  * `AddAcmIdmAuthentication`
  * `AddAcmIdmAuthorization`

Followed by using authentication/authorization on the `WebApplication`:

* `app.UseAuthentication();`
* `app.UseAuthorization();`

On your mapped HTTP methods, you can then call `RequireAuthorization(PolicyNames.AcmIdmPolicy)`

## Docker Compose files

The repository includes two `docker-compose` files. The first is located at the root of the repo and is used to manually test the above described samples. 

The second `docker-compose` file is located under `test\Be.Vlaanderen.Basisregisters.Auth.Tests.ContainerHelper` under the name `identityserverfake_test.yml`. This file is used to run the integration tests. 

Both files run a docker image provided by the [identity-server-fake
](https://github.com/Informatievlaanderen/identity-server-fake) repo. The fake identity server is configured by the acm.json files under \identityserver next to the `docker-compose` files.