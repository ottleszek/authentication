using LibraryBlazorClient.Components.Image;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Forms;
using System.Net.Http.Headers;

namespace LibraryBlazorClient.Components
{
    public partial class UIImageUpload : ComponentBase
    {

        private string _imgUrl = string.Empty;

        [Parameter]
        public string? ButtonText { get; set; } = "Kép felöltése";
        [Parameter]
        public EventCallback<string> OnChange { get; set; }        
        [Parameter]
        public string Path { get; set; } = string.Empty;
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
                    var resizedFile = await imageFile.RequestImageFileAsync("image/jpg", 300, 500);
                    using (var ms = resizedFile.OpenReadStream(resizedFile.Size))
                    {
                        var playload = new
                        {
                            Path = Path,
                        };


                        var content = new MultipartFormDataContent();
                        content.Headers.ContentDisposition = new ContentDispositionHeaderValue("form-data");                        
                        content.Add(new StreamContent(ms, Convert.ToInt32(resizedFile.Size)), "image", imageFile.Name);
                        content.Add(new StringContent(playload.Path),"Data.FilePath");
                        

                        _imgUrl = await UploadHttpService.UploadImage(content);

                        await OnChange.InvokeAsync(_imgUrl);
                    }
                }
            }
        }
    }
}
