# basisregisters-acmidm
ACM/IDM utilities for C#

# How to use?

## Web Api sample
* Using Be.Vlaanderen.Basisregisters.Api
* IServiceCollection.AddAcmIdmAuthentication
* MiddlewareHooks.Authorization -> options.AddAcmIdmAuthorization
* Separate registration of IAuthorizationHandler(s)
* [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme, Policy = "acm-idm-scopes")] on API controller or controller method(s)

## Minimal API sample
* Suffices to use extension methods on IServiceCollection
  * AddAcmIdmAuthentication
  * AddAcmIdmAuthorization
  * RequireAuthorization(string policyName)


## Docker Compose files