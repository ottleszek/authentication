using Microsoft.AspNetCore.Http;

namespace LibraryDatabase.UploadFile
{
    public class UploadFile
    {
        public IFormFile? FileToUpload { get; set; }
        public UploadFileData Data { get; set; } = new();
        public bool IsValidData => (Data.FilePath != string.Empty && Data.FileName != string.Empty && Data.FileExtension != string.Empty)
                                    ||
                                   (Data.FileExtension == string.Empty && Path.GetExtension(Data.FileName) != string.Empty && Path.GetFileNameWithoutExtension(Data.FileName) != string.Empty);
        public string NewFileName => $"{Data.FileName}.{Data.FileExtension}";
        public bool IsNewExtension => Data.FileExtension != string.Empty;
        public bool IsNewFileName => Data.FilePath != string.Empty;
    }
}
