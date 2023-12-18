using Microsoft.AspNetCore.Components;
using MudBlazor;

namespace LibraryBlazorClient.Components
{
    public class ShowConfirmationDialog : IShowConfirmationDialog
    {
        [Inject] private IDialogService? DialogService { get; set; }
        public string Question { get; set; } = string.Empty;

        public async Task<DialogResult> Show()
        {
            var parameters = new DialogParameters();
            parameters.Add("ContentText", Question);
            parameters.Add("ButtonText", "Törlés");
            parameters.Add("Color", Color.Error);

            var options = new DialogOptions() { CloseButton = true, MaxWidth = MaxWidth.ExtraSmall };

            IDialogReference dialog = DialogService.Show<UIConfirmationDialog>("Törlés", parameters, options);
            return await dialog.Result;
        }
    }
}
