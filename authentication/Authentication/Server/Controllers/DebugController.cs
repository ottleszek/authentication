using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace Authentication.Server.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class DebugController : ControllerBase
    {
        private UserManager<IdentityUser>? _userManager;

        public DebugController(UserManager<IdentityUser> userManager)
        {
            _userManager = userManager;
        }

        [HttpGet("{email}")]
        public async Task<IActionResult> GetUserRoles(string email)
        {
            IdentityUser? iUser = await _userManager.FindByNameAsync(email);
            var roles = await _userManager.GetRolesAsync(iUser);
            return Ok(roles);
        }
    }
}
