using Authentication.Server.Services;
using Authentication.Shared.Dtos;
using Authentication.Shared.Models;
using LibraryDatabase.Model;
using Microsoft.AspNetCore.Mvc;

namespace Authentication.Server.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ProfilController : ControllerBase
    {
        private readonly IProfilService? _profilService;

        public ProfilController(IProfilService profilService)
        {
            _profilService = profilService;
        }

        [HttpGet("{email}")]
        public async Task<IActionResult> GetUserBy(string email)
        {
            if (_profilService is not null)
            {
                ProfilDto result = await _profilService.GetUserBy(email);
                if (result.IsValidUser)
                {
                    return Ok(result);
                }
            }
            return BadRequest();
        }

        [HttpPost]
        public async Task<IActionResult> UpdateProfil([FromBody] ProfilDto profil)
        {
            if (_profilService is not null)
            {
                ServiceResponse response = await _profilService.UpdateProfil(profil);
                if (! response.HasError)
                {
                    return Ok(response);
                }
                else
                {
                    return BadRequest(response);
                }
            }
            return BadRequest();
        }

    }
}
