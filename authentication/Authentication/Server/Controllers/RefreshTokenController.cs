using Authentication.Server.Services;
using AuthenticationLibrary.Shared.Dtos;
using Microsoft.AspNetCore.Mvc;

namespace Authentication.Server.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class RefreshTokenController : ControllerBase
    {

        private readonly ILoginService _loginService;


        public RefreshTokenController(ILoginService loginService)
        {
            _loginService = loginService;
        }

        [HttpPost]
        [Route("refresh-token")]
        public async Task<IActionResult> Refresh([FromBody] RefreshTokenDto tokenDto)
        {

            if (_loginService is not null)
            {
                TokenResponseDto response = await _loginService.RenewTokenAsnyc(tokenDto);
                if (!response.HasError)
                {
                    return Ok(response);
                }
                return BadRequest(response);
            }
            return BadRequest();
        }
    }
}
