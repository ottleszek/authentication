using Authentication.Server.Services;
using Authentication.Shared.Dtos;
using Authentication.Shared.Model;
using LibraryApiTemplate.Upload;
using LibraryCore.Responses;
using Microsoft.AspNetCore.Mvc;

namespace Authentication.Server.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ProfilImageController : UploadController
    {
        private readonly IProfilService? _profilService;
        public ProfilImageController(IProfilService profilService)
        {
            _profilService = profilService;
        }

        [HttpPost("is-profil-image-exsist")]
        public async Task<IActionResult> IsProfilImageExsist([FromBody] ProfilImageFileNameDto profilImageFileNameDto)
        {
            ProfilImageFileName fileNameData = profilImageFileNameDto.ToProfilImageFileName();
            string fileNameToCheck = await GetProfilImageFullPath(fileNameData);
            try
            {
                if (System.IO.File.Exists(fileNameToCheck))
                {
                    return Ok(true);
                }                
            }
            catch(Exception ex)
            {
                LibraryLogging.LoggingBroker.LogError(ex.Message);
                LibraryLogging.LoggingBroker.LogError($"{nameof(ProfilImageController)}", $"{nameof(IsProfilImageExsist)}", $"Profil kép létezésének ellenőrzése nem sikerült, file név:{fileNameData.FileName}");

            }
            return Ok(false);
        }

        [HttpPost("delete-profil")]
        public async Task<ActionResult> DeleteProfil([FromBody] ProfilImageFileNameDto profilImageFileNameDto)
        {
            ProfilImageFileName fileNameData = profilImageFileNameDto.ToProfilImageFileName();
            string fileToDeleteFullPath = await GetProfilImageFullPath(fileNameData);
            try
            {
                if (System.IO.File.Exists(fileToDeleteFullPath))
                {
                    System.IO.File.Delete(fileToDeleteFullPath);
                    return Ok(new ControllerResponse());
                }
            }
            catch(Exception ex)
            {
                LibraryLogging.LoggingBroker.LogError(ex.Message);
                LibraryLogging.LoggingBroker.LogError($"{nameof(ProfilImageController)}", $"{nameof(DeleteProfil)}", $"Profil kép törlése nem sikerült:{fileNameData.FileName}");
            }
            return BadRequest(new ControllerResponse("Profil kép törlése nem lehetséges!"));
        }

        private async Task<string> GetProfilImageFullPath(ProfilImageFileName profilImageFileName)
        {
            if (_profilService is null)
            {
                return string.Empty;
            }
            else
            {
                profilImageFileName.ProfilImageTimeStamp = await _profilService.GetProfilImageTimeStamp(profilImageFileName.Email);

                string fileName = $"{profilImageFileName.FileNameWithoutExtension}.{profilImageFileName.ProfilImageTimeStamp}.{profilImageFileName.FileNameExtension}";
                string folderName = Path.Combine("staticfiles", "profil");
                string currentDirectory = Directory.GetCurrentDirectory();
                string filepath = Path.Combine(currentDirectory, folderName);

                return Path.Combine(filepath, fileName);
            }
        }
    }
}
