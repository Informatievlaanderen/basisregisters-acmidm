namespace Be.Vlaanderen.Basisregisters.AcmIdm.Abstractions.AuthenticationHandlers
{
    using System.Collections.Generic;
    using System.IdentityModel.Tokens.Jwt;
    using System.Linq;
    using System.Security.Claims;
    using System.Text.Encodings.Web;
    using System.Threading.Tasks;
    using Microsoft.AspNetCore.Authentication;
    using Microsoft.AspNetCore.Authentication.JwtBearer;
    using Microsoft.Extensions.Logging;
    using Microsoft.Extensions.Options;
    using Microsoft.IdentityModel.Tokens;
    using Microsoft.Net.Http.Headers;

    public class CustomAuthenticationHandler : AuthenticationHandler<JwtBearerOptions>
    {
        public CustomAuthenticationHandler(IOptionsMonitor<JwtBearerOptions> options, ILoggerFactory logger, UrlEncoder encoder, ISystemClock clock) : base(options, logger, encoder, clock)
        {
        }

        protected override Task<AuthenticateResult> HandleAuthenticateAsync()
        {
            var jwt = Request.Headers.Authorization.ToString().Replace("Bearer ", "");

            var token = new JwtSecurityTokenHandler().ReadToken(jwt) as JwtSecurityToken;

            IEnumerable<Claim> claims = token!.Claims.Where(x => x.Type == "scope");
            var claimsPrincipal = new ClaimsPrincipal(new ClaimsIdentity(claims));
            return Task.FromResult<AuthenticateResult>(AuthenticateResult.Success(new AuthenticationTicket(claimsPrincipal, "Bearer")));
        }
    }
}
