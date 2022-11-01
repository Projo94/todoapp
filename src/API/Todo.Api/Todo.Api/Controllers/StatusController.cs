using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Todo.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class StatusController : ControllerBase
    {
        [HttpGet]
        [HttpPost]
        [Route("")]
        public IActionResult Get()
        {
            return Ok();
        }
    }
}
