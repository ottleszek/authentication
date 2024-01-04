namespace Authentication.Server.Datas
{
    public class UploadFileDataDto
    {
        public IFormFile? FileToUpload { get; set; }
        public FileUploadDataDto Data { get; set; } = new();
        public bool IsValidData => (Data.FilePath != string.Empty && Data.FileName != string.Empty && Data.Extension != string.Empty)
                                    ||
                                   (Data.Extension == string.Empty && Path.GetExtension(Data.FileName) != string.Empty && Path.GetFileNameWithoutExtension(Data.FileName) != string.Empty);
        public string NewFileName => $"{Data.FileName}.{Data.Extension}";
        public bool IsNewExtension => Data.Extension != string.Empty;
        public bool IsNewFileName => Data.FilePath != string.Empty;
    }

    public class FileUploadDataDto
    {
        public string FilePath { get; set; } = string.Empty;
        public string FileName { get; set; } = string.Empty;
        public string Extension { get; set; } = string.Empty;
    }
}
