using MudBlazor;

namespace LibraryBlazorClient.Components
{
    public interface IShowConfirmationDialog
    {
        public string Question { get; set; }
        public Task<DialogResult> Show();
    }
}
