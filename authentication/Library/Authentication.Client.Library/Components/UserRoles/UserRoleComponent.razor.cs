using AKSoftware.Blazor.Utilities;
using Authentication.Shared.Models;
using LibraryBlazorClient.Components;
using LibraryBlazorClient.Templates.ComponentsBase;
using Microsoft.AspNetCore.Components;
using MudBlazor;

namespace Authentication.Client.Library.Components
{
    public partial class UserRoleComponent : ListAndDeleteViewComponentBase<UserRole>
    {
        [Inject] private NavigationManager? Navigation { get; set; }
        [Inject] private ISnackbar? Snackbar { get; set; }
        [Inject] private IShowConfirmationDialog? ShowConfirmationDialog { get; set; }

        private void GoToEditUserRole(UserRole userRole)
        {
            Navigation?.NavigateTo($"/userrole/form/{userRole.Id}");
            
        }

        private void  GoToInsertUserRole()
        {
            Navigation?.NavigateTo("/userrole/form");
        }

        public async Task FetchDataToViewModel()
        {
            if (ViewModel is not null)
            {
                await ViewModel.GetAllDataToViewModelAsync();
            }
        }

        private async Task DeleteAsync(UserRole userRole)
        {
            if (ViewModel is not null && ShowConfirmationDialog is not null)
            {
                ShowConfirmationDialog.Question = $"Valóban törölni akarja a {userRole.Name} nevű szerepet?";
                DialogResult confirmationResult = await ShowConfirmationDialog.Show();

                if (confirmationResult.Cancelled)
                {
                    return;
                }
                else
                {
                    await ViewModel.DeleteAsync(userRole.Id);
                    MessagingCenter.Send(this, "userrole_deleted", userRole);
                    if (Snackbar is not null)
                    {
                        Snackbar.Add("A szerep sikeressen törölve", Severity.Success);
                    }
                }
            }
        }
    }
}
