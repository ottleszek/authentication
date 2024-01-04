using LibraryBlazorClient.Components.Image;
using LibraryCore.Errors;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Forms;
using System.Net.Http.Headers;

namespace LibraryBlazorClient.Components
{
    public partial class UIImageUpload : ComponentBase
    {

        private string _imgUrl = string.Empty;
        private ErrorStore Error = new ();

        [Parameter]
        public string? ButtonText { get; set; } = "Kép felöltése";
        [Parameter]
        public EventCallback<string> OnChange { get; set; }        
        [Parameter]
        public string FilePath { get; set; } = string.Empty;
        [Parameter]
        public string FileName { get; set; } = string.Empty;


        [Inject] UploadHttpService? UploadHttpService { get; set; }

        private async Task UploadImage(InputFileChangeEventArgs eventArgs)
        {
            var imageFiles = eventArgs.GetMultipleFiles();
            foreach (var imageFile in imageFiles)
            {
                if (imageFile != null && UploadHttpService is not null)
                {

                    if (imageFile.Size>2097152)
                    {
                        Error.AppendNewError("Megengedett file méret 2 MB");
                        return;
                    }
                    string[] allowedExtensions = new[] { ".jpg", ".png",".svg" };
                    string imageFileExtenson = Path.GetExtension(imageFile.Name).ToLower();
                    if (!allowedExtensions.Contains(imageFileExtenson))
                    {
                        Error.AppendNewError("A profil kiterjesztése jpg, png vagy svg lehet!");
                        return;
                    }

                    //var resizedFile = await imageFile.RequestImageFileAsync("image/jpg", 300, 500);
                    //using (var ms = resizedFile.OpenReadStream(resizedFile.Size))
                    using (var ms=imageFile.OpenReadStream(imageFile.Size)) 
                    {
                        var playload = new
                        {
                            FilePath = FilePath,
                            FileName = FileName
                            FileExtension = imageFileExtenson
                        };


                        var content = new MultipartFormDataContent();
                        content.Headers.ContentDisposition = new ContentDispositionHeaderValue("form-data");                        
                        content.Add(new StreamContent(ms, Convert.ToInt32(imageFile.Size)), "image", imageFile.Name);
                        content.Add(new StringContent(playload.FilePath),"Data.FilePath");
                        content.Add(new StringContent(playload.FileName), "Data.FileName");

                        _imgUrl = await UploadHttpService.UploadImage(content);

                        await OnChange.InvokeAsync(_imgUrl);
                    }
                }
            }
        }
    }
}
