using LibraryLogging;
using Microsoft.AspNetCore.Mvc;
using System;
using System.IO;
using System.Net.Http.Headers;

namespace Authentication.Server.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public  class UploadController : ControllerBase
    {
        [HttpPost]
        public async Task<IActionResult> Upload(string path)
        {
            string? fileName = string.Empty;
            try
            {
                    var formCollection = await Request.ReadFormAsync();
                    var file = formCollection.Files.First();
                    var folderName = Path.Combine("StaticFiles", path);
                    var pathToSave = Path.Combine(Directory.GetCurrentDirectory(), folderName);
                if (file.Length > 0 && file.ContentDisposition is not null)
                {
                    fileName = ContentDispositionHeaderValue.Parse(file.ContentDisposition).FileName?.Trim('"');
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
