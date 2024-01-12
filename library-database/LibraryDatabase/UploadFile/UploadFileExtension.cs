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

        public static UploadFileData ToFileUploadData(this UploadFileDataDto dtoFileData)
        {
            return new UploadFileData
            {
                FilePath = dtoFileData.FilePath,
                FileExtension = dtoFileData.FileExtension,
                FileName = dtoFileData.FileName
            };

        }
    }
}
