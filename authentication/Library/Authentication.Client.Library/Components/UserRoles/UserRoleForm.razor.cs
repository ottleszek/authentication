using Authentication.Client.Library.Validation;
using Authentication.Shared.Models;
using LibraryBlazorClient.Components;
using LibraryBlazorMvvm.Components;
using LibraryBlazorMvvm.ViewModels;
using Microsoft.AspNetCore.Components;
using MudBlazor;
using static MudBlazor.Icons;
using FluentValidation.Results;

namespace Authentication.Client.Library.Components
{
    public partial class UserRoleForm : MvvmItemComponentBase<UserRole, MvvmCrudViewModelBase<UserRole>>
    {
        private MudForm _form = new();

        [Parameter] public Guid Id { get; set; } = Guid.Empty;
        [Inject] private NavigationManager? Navigation { get; set; }
        [Inject] private MvvmItemUserRoleValidation? Validation { get; set; }
        [Inject] private IShowConfirmationDialog? ShowConfirmationDialog { get; set; }
        [Inject] private ISnackbar? Snackbar { get; set; }

        private string _saveButtonText => ViewModel is not null && ViewModel.IsNewItemMode ? "Mentés" : "Módosítás";
        private string _cancelButtonText => ViewModel is not null && ViewModel.IsNewItemMode ? "Mégsem" : "Visszaállítás";

        protected async override Task OnParametersSetAsync()
        {
            if (ViewModel is not null)
            {
                ViewModel.Id = Id;
                await ViewModel.Loading();
            }
            await base.OnParametersSetAsync();
        }

        private async Task UpdateAsync()
        {
            if (ViewModel is not null && Validation is not null)
            {
                ValidationResult results = Validation.Validate(ViewModel);
                if (!results.IsValid)
                    return;

                if (ViewModel.IsNewItemMode)
                    await ViewModel.InsertAsync();
                else    
                    await ViewModel.UpdateAsync();

                if (ViewModel.ErrorStore.HasError)
                {
                    if (ViewModel.IsNewItemMode)
                        Snackbar?.Add("A szerep mentése nem sikerült", Severity.Error);
                    else
                        Snackbar?.Add("A szerep módosítás nem sikerült", Severity.Error);
                }
                else
                {
                    if (ViewModel.IsNewItemMode)
                    {
                        Snackbar?.Add("A szerep mentése sikerült", Severity.Success);
                        GoBack();
                    }
                    else
                        Snackbar?.Add("A szerep módosítása sikerült", Severity.Success);
                }
            }
        }

        private async Task DeleteAsync()
        {
            if (ViewModel is not null && ShowConfirmationDialog is not null)
            {
                ShowConfirmationDialog.Question = $"Valóban törölni akarja a {ViewModel.SelectedItem.Name} nevű felhasználót?";
                DialogResult confirmationResult = await ShowConfirmationDialog.Show();

                if (confirmationResult.Cancelled)
                {
                    return;
                }
                else
                {
                    await ViewModel.DeleteItemAsync();
                    if (ViewModel.ErrorStore.HasError)
                        Snackbar?.Add("A szerep törlése nem sikerült", Severity.Error);
                    else
                        Snackbar?.Add("A szerep törlése sikerült", Severity.Success);
                    GoBack();
                }
            }
        }

        private void GoBack()
        {
            Navigation?.NavigateTo("/userrole");
        }
    }
}
