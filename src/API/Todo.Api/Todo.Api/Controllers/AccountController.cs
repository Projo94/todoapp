using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Mvc;
using Todo.Api.Application.Models.Authentication;
using IdentityApp = Todo.Api.Application.Contracts.Identity;

namespace Todo.Api.Controllers
{
    [EnableCors("Open")]
    [Route("api/[controller]")]
    [ApiController]
    public class AccountController : ControllerBase
    {
        private readonly IdentityApp.IAuthenticationService _authenticationService;
        public AccountController(IdentityApp.IAuthenticationService authenticationService)
        {
            _authenticationService = authenticationService;
        }

        [HttpPost("authenticate")]
        public async Task<ActionResult<AuthenticationResponse>> AuthenticateAsync(AuthenticationRequest request)
        {
            return Ok(await _authenticationService.AuthenticateAsync(request));
        }

        [HttpPost("register")]
        public async Task<ActionResult<RegistrationResponse>> RegisterAsync(RegistrationRequest request)
        {
            return Ok(await _authenticationService.RegisterAsync(request));
        }
    }
}