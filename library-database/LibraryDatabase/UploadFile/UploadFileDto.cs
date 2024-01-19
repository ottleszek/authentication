using Microsoft.AspNetCore.Http;

namespace LibraryDatabase.UploadFile
{
    public class UploadFileDto
    {
        public IFormFile? FileToUpload { get; set; }
        public UploadFileDataDto Data { get; set; } = new();
    }
}
