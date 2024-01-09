namespace LibraryDatabase.UploadFile
{
    public static class UploadFileExtension
    {
        public static UploadFile ToUploadFile(this UploadFileDto dtoFile)
        {
            return new UploadFile
            {
                FileToUpload = dtoFile.FileToUpload,
                Data = dtoFile.Data.ToFileUploadData()
            };
        }

        public static FileUploadData ToFileUploadData(this FileUploadDataDto dtoFileData)
        {
            return new FileUploadData
            {
                FilePath = dtoFileData.FilePath,
                FileExtension = dtoFileData.FileExtension,
                FileName = dtoFileData.FileName
            };

        }
    }
}
