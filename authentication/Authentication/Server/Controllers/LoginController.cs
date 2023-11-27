using Authentication.Server.Services;
using AuthenticationLibrary.Shared.Dtos;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace Authentication.Server.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class LoginController : ControllerBase
    {
        private readonly ILoginService? _loginService;

        public LoginController(ILoginService loginService, UserManager<IdentityUser> userManager)
        {
            _loginService = loginService;
        }

        [HttpPost("login")]
        public async Task<IActionResult> Login([FromBody] UserLoginDto loginPlayload)
        {
            if (_loginService is not null)
            {
                TokenResponseDto response = await _loginService.Login(loginPlayload);
                if (response.IsLoggedIn)
                {
                    return Ok(response);
                }
                return BadRequest(response);
            }
            return BadRequest();
        }
    }
}
