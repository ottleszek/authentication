﻿using Microsoft.AspNetCore.Http;

namespace LibraryDatabase.UploadFile
{
    public class UploadFileDto
    {
        public IFormFile? FileToUpload { get; set; }
        public UploadFileDataDto Data { get; set; } = new();
    }

    public class UploadFileDataDto
    {
        public string FilePath { get; set; } = string.Empty;
        public string FileName { get; set; } = string.Empty;
        public string FileExtension { get; set; } = string.Empty;
    }
}
