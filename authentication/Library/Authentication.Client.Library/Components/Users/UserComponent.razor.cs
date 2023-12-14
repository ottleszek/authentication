using Authentication.Shared.Models;
using LibraryBlazorClient.Components;
using LibraryBlazorClient.Templates;
using Microsoft.AspNetCore.Components;
using MudBlazor;

namespace Authentication.Client.Library.Components
{
    public partial class UserComponent : ListViewComponentBase<User>
    {
		[Inject] private NavigationManager? Navigation { get; set; }
        [Inject] private IDialogService? DialogService { get; set; }
        [Inject] private ISnackbar? Snackbar { get; set; }

        private void GoToEditUser(User user)
		{
			if (Navigation is not null)
			{
				Navigation.NavigateTo($"/user/form/{user.Id}");
			}
		}

        private async Task DeleteAsync(User user)
        {
            if (ViewModel is not null && DialogService is not null)
            {
                var parameters = new DialogParameters();
                parameters.Add("ContentText", $"Valóban törölni akarja a {user.HungarianFullName} nevű felhasználót?");
                parameters.Add("ButtonText", "Törlés");
                parameters.Add("Color", Color.Error);

                var options = new DialogOptions() { CloseButton = true, MaxWidth = MaxWidth.ExtraSmall };

                IDialogReference dialog = DialogService.Show<UIConfirmationDialog>("Törlés", parameters, options);
                DialogResult confirmationResult = await dialog.Result;

                if (confirmationResult is not null && !confirmationResult.Cancelled)
                {
                    await ViewModel.DeleteAsync();
                    if (Snackbar is not null)
                    {
                        Snackbar.Add("A felhasználó törlése sikerült", Severity.Success);
                        GoBack();
                        return;
                    }

                }
            }
            if (Snackbar is not null)
                Snackbar.Add("A felhasználó törlése nem sikerült", Severity.Error);
        }
    }
}
