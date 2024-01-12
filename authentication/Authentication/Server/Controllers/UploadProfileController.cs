using Authentication.Shared.Dtos;
using Authentication.Shared.Model;
using LibraryApiTemplate.Upload;
using Microsoft.AspNetCore.Mvc;

namespace Authentication.Server.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class UploadProfileController : UploadController
    {
        public UploadProfileController()
        {
                
        }

        [HttpPost("deleteprofil")]
        public async Task<IActionResult> DeleteProfil(ProfilImageFilenNameDataDto profilImageUrl)
        {
            ProfilImageFileName fileToDelete = profilImageUrl.ToProfilImageUrl();
            string fileName = fileToDelete.GetProfilImageUrlName();

        }
    }
}
