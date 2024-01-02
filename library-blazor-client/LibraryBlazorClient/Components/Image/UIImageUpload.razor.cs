using LibraryBlazorClient.Components.Image;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Forms;
using System.Net.Http.Headers;

namespace LibraryBlazorClient.Components
{
    public partial class UIImageUpload : ComponentBase
    {
        [Parameter]
        public EventCallback<string> OnChange { get; set; }
        public string ImgUrl { get; set; }
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
                        var content = new MultipartFormDataContent();
                        content.Headers.ContentDisposition = new ContentDispositionHeaderValue("form-data");
                        content.Add(new StreamContent(ms, Convert.ToInt32(resizedFile.Size)), "image", imageFile.Name);
                        ImgUrl = await UploadHttpService.UploadImage(content);
                        await OnChange.InvokeAsync(ImgUrl);
                    }
                }
            }
        }
    }
}
