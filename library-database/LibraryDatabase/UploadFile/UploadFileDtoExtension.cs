namespace LibraryDatabase.UploadFile
{
    public static class UploadFileDtoExtension
    {
        public static UploadFile ToUploadFile(this UploadFileDto dtoFile)
        {
            return new UploadFile
            {
                FileToUpload = dtoFile.FileToUpload,
                Data = dtoFile.Data.ToUploadFileData()
            };
        }

        public static UploadFileData ToUploadFileData(this UploadFileDataDto dtoFileData)
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
