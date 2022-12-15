namespace AcmIdmConsumer.WebApi.Controllers
{
    using Be.Vlaanderen.Basisregisters.AcmIdm;
    using Be.Vlaanderen.Basisregisters.Api;
    using Microsoft.AspNetCore.Authentication.JwtBearer;
    using Microsoft.AspNetCore.Authorization;
    using Microsoft.AspNetCore.Mvc;

    [ApiRoute("secret")]
    public class SecretController : ControllerBase
    {
        [HttpGet]
        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme, Policy = PolicyNames.AcmIdmPolicy)]
        public IActionResult Get()
        {
            return Ok("Hello");
        }
    }
}
