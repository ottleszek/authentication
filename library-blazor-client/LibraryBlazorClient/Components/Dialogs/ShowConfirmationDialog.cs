using MudBlazor;

namespace LibraryBlazorClient.Components
{
    public class ShowConfirmationDialog : IShowConfirmationDialog
    {
        //[Inject] private IDialogService? DialogService { get; set; }

        private readonly IDialogService? _dialogService;

        public ShowConfirmationDialog(IDialogService? dialogService)
        {
            _dialogService = dialogService;
        }

        public string Question { get; set; } = string.Empty;

        public async Task<DialogResult> Show()
        {
            DialogParameters parameters = new();
            parameters.Add("ContentText", Question);
            parameters.Add("ButtonText", "Törlés");
            parameters.Add("Color", Color.Error);

            var options = new DialogOptions() { CloseButton = true, MaxWidth = MaxWidth.ExtraSmall };

            if (_dialogService is not null)
            {
                IDialogReference dialog = _dialogService.Show<UIConfirmationDialog>("Törlés", parameters, options);
                return await dialog.Result;
            }
            else
                return DialogResult.Cancel();
        }
    }
}
