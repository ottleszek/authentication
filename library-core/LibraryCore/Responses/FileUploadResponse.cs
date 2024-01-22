namespace LibraryCore.Responses
{
    public class FileUploadResponse
    {
        public string Url { get; set; } = string.Empty;
        public string TimeStamp { get; set; } = string.Empty;

        public bool IsTimeStampValid => TimeStamp != string.Empty;
    
    }
}
