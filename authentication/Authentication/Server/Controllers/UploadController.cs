using Authentication.Server.Datas;
using LibraryLogging;
using Microsoft.AspNetCore.Mvc;
using System.Net.Http.Headers;

namespace Authentication.Server.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public  class UploadController : ControllerBase
    {
        [HttpPost]
        public async Task<IActionResult> Upload([FromForm] UploadFileDataDto dto)
        {
            string? fileName = string.Empty;
            if (!dto.IsValidData)
                return BadRequest();
            try
            {
                var formCollection = await Request.ReadFormAsync();
                var file = formCollection.Files.First();
                var folderName = Path.Combine("StaticFiles", dto.Data.FilePath);
                var pathToSave = Path.Combine(Directory.GetCurrentDirectory(), folderName);
                if (file.Length > 0 && file.ContentDisposition is not null)
                {
                    if (dto.IsNewFileName && dto.IsNewExtension)
                    {
                        fileName = dto.NewFileName;
                    }
                    else if (!dto.IsNewFileName && !dto.IsNewExtension)
                    {
                        fileName = ContentDispositionHeaderValue.Parse(file.ContentDisposition).FileName?.Trim('"');
                    }
                    else if (dto.IsNewFileName && !dto.IsNewExtension)
                    {
                        string? tempFileName = ContentDispositionHeaderValue.Parse(file.ContentDisposition).FileName?.Trim('"');
                        if (tempFileName is not null)
                        {
                            fileName = $"{dto.Data.FileName}.{Path.GetExtension(tempFileName)}";
                        }
                        else
                        {
                            fileName = $"{dto.Data.FileName}";
                        }
                    }
                    else 
                    {
                        string? tempFileName = ContentDispositionHeaderValue.Parse(file.ContentDisposition).FileName?.Trim('"');
                        if (tempFileName is not null)
                        {
                            fileName= $"{Path.GetFileNameWithoutExtension(tempFileName)}.{dto.Data.FileName}";
                        }
                        else
                        {
                            fileName = $"{Guid.NewGuid().ToString()}.{dto.Data.FileName}";
                        }
                    }
                    if (fileName is not null)
                    {
                        var fullPath = Path.Combine(pathToSave, fileName);
                        var dbPath = Path.Combine(folderName, fileName);
                        using (var stream = new FileStream(fullPath, FileMode.Create))
                        {
                            file.CopyTo(stream);
                        }
                        return Ok(dbPath);
                    }
                }                
            }
            catch (Exception ex)
            {
                if (fileName is not null)
                    LoggingBroker.LogError($"A {fileName} fájl mentése nem sikerült!");
                LoggingBroker.LogError(ex.Message);
            }
            return BadRequest();
        }
    }
}
