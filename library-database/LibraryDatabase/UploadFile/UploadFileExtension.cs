namespace LibraryDatabase.UploadFile
{
    public static class UploadFileExtension
    {
        public static UploadFileDto ToDto(this UploadFile file)
        {
            return new UploadFileDto
            {
                FileToUpload = file.FileToUpload,
                Data =
            }
        }

    }
}
