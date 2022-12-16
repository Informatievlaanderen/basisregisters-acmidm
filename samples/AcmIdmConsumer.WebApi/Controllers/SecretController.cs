namespace AcmIdmConsumer.WebApi.Controllers
{
    using System;
    using System.Linq;
    using Be.Vlaanderen.Basisregisters.AcmIdm;
    using Be.Vlaanderen.Basisregisters.Api;
    using Microsoft.AspNetCore.Authentication.JwtBearer;
    using Microsoft.AspNetCore.Authorization;
    using Microsoft.AspNetCore.Mvc;

    public class SecretController : ControllerBase
    {
        [HttpGet]
        [ApiRoute("secret")]
        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme, Policy = PolicyNames.Adres.DecentraleBijwerker)]
        public IActionResult Get()
        {
            HttpContext.User.Claims.ToList().ForEach(x => Console.WriteLine($"{x.Type}: {x.Value}"));
            return Ok("Hello");
        }

        [HttpGet]
        [ApiRoute("secret/very")]
        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme, Policy = PolicyNames.Adres.DecentraleBijwerker)]
        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme, Policy = PolicyNames.Adres.InterneBijwerker)]
        public IActionResult GetVerySecret()
        {
            HttpContext.User.Claims.ToList().ForEach(x => Console.WriteLine($"{x.Type}: {x.Value}"));
            return Ok("Hello very secret");
        }
    }
}
