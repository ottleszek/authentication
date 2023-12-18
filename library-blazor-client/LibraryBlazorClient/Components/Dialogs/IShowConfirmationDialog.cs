using MudBlazor;

namespace LibraryBlazorClient.Components.Dialogs
{
    public interface IShowConfirmationDialog
    {
        public string Question { get; set; }
        public Task<DialogResult> Show();
    }
}
