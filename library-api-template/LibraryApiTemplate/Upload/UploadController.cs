using LibraryDatabase.UploadFile;
using LibraryLogging;
using Microsoft.AspNetCore.Mvc;
using System.Net.Http.Headers;

namespace LibraryApiTemplate.Upload
{
    public abstract class UploadController : ControllerBase
    {
        [HttpPost]
        public async Task<IActionResult> Upload([FromForm] UploadFileDto uploadFileDto)
        {
            UploadFile uploadFile = uploadFileDto.ToUploadFile();
            string? fileName = string.Empty;
            if (!uploadFile.IsValidData)
                return BadRequest();
            try
            {
                var formCollection = await Request.ReadFormAsync();
                var file = formCollection.Files.First();                
                var folderName = Path.Combine("StaticFiles", uploadFile.Data.FilePath);
                string currentDirectory = Directory.GetCurrentDirectory();
                var pathToSave = Path.Combine(currentDirectory, folderName);
                if (file.Length > 0 && file.ContentDisposition is not null)
                {
                    if (uploadFile.IsNewFileName && uploadFile.IsNewExtension)
                    {
                        fileName = uploadFile.NewFileName;
                    }
                    else if (!uploadFile.IsNewFileName && !uploadFile.IsNewExtension)
                    {
                        fileName = ContentDispositionHeaderValue.Parse(file.ContentDisposition).FileName?.Trim('"');
                    }
                    else if (uploadFile.IsNewFileName && !uploadFile.IsNewExtension)
                    {
                        string? tempFileName = ContentDispositionHeaderValue.Parse(file.ContentDisposition).FileName?.Trim('"');
                        if (tempFileName is not null)
                        {
                            fileName = $"{uploadFile.Data.FileName}.{Path.GetExtension(tempFileName)}";
                        }
                        else
                        {
                            fileName = $"{uploadFile.Data.FileName}";
                        }
                    }
                    else
                    {
                        string? tempFileName = ContentDispositionHeaderValue.Parse(file.ContentDisposition).FileName?.Trim('"');
                        if (tempFileName is not null)
                        {
                            fileName = $"{Path.GetFileNameWithoutExtension(tempFileName)}.{uploadFile.Data.FileName}";
                        }
                        else
                        {
                            fileName = $"{Guid.NewGuid().ToString()}.{uploadFile.Data.FileName}";
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
