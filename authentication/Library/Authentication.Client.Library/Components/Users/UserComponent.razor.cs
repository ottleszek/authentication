using AKSoftware.Blazor.Utilities;
using Authentication.Shared.Models;
using LibraryBlazorClient.Components;
using LibraryBlazorClient.Templates.ComponentsBase;
using Microsoft.AspNetCore.Components;
using MudBlazor;

namespace Authentication.Client.Library.Components
{
    public partial class UserComponent : ListAndDeleteViewComponentBase<User>
    {
		[Inject] private NavigationManager? Navigation { get; set; }
        [Inject] private ISnackbar? Snackbar { get; set; }
        [Inject] private IShowConfirmationDialog? ShowConfirmationDialog { get; set; }

        private void GoToEditUser(User user)
		{
			if (Navigation is not null)
			{
				Navigation.NavigateTo($"/user/form/{user.Id}");
			}
		}

        public async Task FetchDataToViewModel()
        {
            if ( ViewModel is not null)
            {
                await ViewModel.GetAllDataToViewModelAsync();
            }
        }

        private async Task DeleteAsync(User user)
        {
            if (ViewModel is not null && ShowConfirmationDialog is not null)
            {
                ShowConfirmationDialog.Question = $"Valóban törölni akarja a {user.HungarianFullName} nevű felhasználót?";
                DialogResult confirmationResult = await ShowConfirmationDialog.Show();                

                if (confirmationResult.Cancelled)
                {
                    return;
                }
                else
                {
                    await ViewModel.DeleteAsync(user.Id);
                    MessagingCenter.Send(this, "user_deleted", user);
                    if (Snackbar is not null)
                    {
                        Snackbar.Add("A felhasználó törlése sikerült", Severity.Success);
                    }
                }
            }
        }
    }
}
