namespace Authentication.Server.Datas
{
    public class UploadFileDataDto
    {
        public IFormFile? FileToUpload { get; set; }
        public FileUploadDataDto Data { get; set; } = new();
    }

    public class FileUploadDataDto
    {
        public string FilePath { get; set; } = string.Empty;
    }
}
