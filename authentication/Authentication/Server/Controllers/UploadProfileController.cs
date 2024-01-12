using Authentication.Shared.Dtos;
using Authentication.Shared.Model;
using LibraryApiTemplate.Repos;
using LibraryApiTemplate.Upload;
using LibraryCore.Responses;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

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
        public IActionResult DeleteProfil(ProfilImageFileNameDto profilImageFileNameDto)
        {
            ProfilImageFileName fileNameData = profilImageFileNameDto.ToProfilImageFileName();
            string fileName = fileNameData.GetProfilImageFilelName();

            string folderName = Path.Combine("staticfiles", "profil");
            string currentDirectory = Directory.GetCurrentDirectory();
            string filepath = Path.Combine(currentDirectory, folderName);

            string fileToDelete= Path.Combine(filepath, fileName);
            try
            {
                if (System.IO.File.Exists(fileToDelete))
                {
                    System.IO.File.Delete(fileToDelete);
                    return Ok(new ControllerResponse());
                }
            }
            catch(Exception ex)
            {
                LibraryLogging.LoggingBroker.LogError(ex.Message);
                LibraryLogging.LoggingBroker.LogError($"{nameof(UploadProfileController)}", $"{nameof(DeleteProfil)}", $"Profil kép törlése nem sikerült:{fileName}");
            }
            return BadRequest(new ControllerResponse("Profil kép törlése nem lehetséges!"));
        }
    }
}
