using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Mvc;

namespace Todo.Api.Controllers
{
    [EnableCors("Open")]
    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
    [Route("api/[controller]")]
    [ApiController]
    public class TimezoneController : BaseApiController
    {
        [HttpGet("all", Name = "GetTimezones")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesDefaultResponseType]
        public ActionResult<List<string>> GetTimezones()
        {
            var timezones = new List<string>();

            foreach (TimeZoneInfo z in TimeZoneInfo.GetSystemTimeZones())
            {
                timezones.Add($"{z.Id}|||||{z.BaseUtcOffset}");
            }

            return Ok(timezones);
        }
    }
}

