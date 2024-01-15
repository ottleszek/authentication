using Authentication.Server.Services;
using Authentication.Shared.Dtos;
using LibraryCore.Responses;
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

        [HttpGet("get-user-id/{email}")]
        public async Task<IActionResult> GetUserIdBy(string email)
        {
            if (_profilService is not null)
            {
                Guid id= await _profilService.GetUserIdBy(email);
                return Ok(id);
            }
            return BadRequest();
        }

        [HttpPost]
        public async Task<IActionResult> UpdateProfil([FromBody] ProfilDto profil)
        {
            ControllerResponse response = new ControllerResponse();
            if (_profilService is not null)
            {
                ServiceResponse serviceResponse = await _profilService.UpdateProfil(profil);

                if (! serviceResponse.HasError)
                {
                    return Ok(response);
                }
                else
                {
                    response.Message = serviceResponse.Message;
                    return BadRequest(response);
                }
            }
            response.ClearAndAddError("A profil frissítés nem lehetséges!");
            return BadRequest(response);
        }      
    }
}
