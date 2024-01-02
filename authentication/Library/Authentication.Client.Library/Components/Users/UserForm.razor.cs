using Authentication.Client.Library.Validation;
using Authentication.Shared.Models;
using FluentValidation.Results;
using LibraryBlazorClient.Components;
using LibraryBlazorMvvm.Components;
using LibraryBlazorMvvm.ViewModels;
using Microsoft.AspNetCore.Components;
using MudBlazor;

namespace Authentication.Client.Library.Components
{ 
    public partial class UserForm : MvvmItemComponentBase<User, MvvmCrudViewModelBase<User>>
    {
        private MudForm _form = new();

        [Parameter] public Guid Id { get; set; } = Guid.Empty;
        [Inject] private NavigationManager? Navigation { get; set; }
        [Inject] private MvvmItemUserValidation? Validation { get; set; }
        [Inject] private IShowConfirmationDialog? ShowConfirmationDialog { get; set; }
        [Inject] private ISnackbar? Snackbar { get; set; }

        private string _saveButtonText => ViewModel is not null && ViewModel.IsNewItemMode ? "Mentés" : "Módosítás";
        private string _cancelButtonText => ViewModel is not null && ViewModel.IsNewItemMode ? "Mégsem" : "Visszaállítás";

        protected async override Task OnParametersSetAsync()
        {
            if (ViewModel is not null)
            {
                ViewModel.Id = Id;
                if (!ViewModel.IsNewItemMode)
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
                        Snackbar?.Add("A felhasználó mentése nem sikerült", Severity.Error);
                    else
                        Snackbar?.Add("A felhasználó módosítás nem sikerült", Severity.Error);
                }
                else
                {
                    if (ViewModel.IsNewItemMode)
                        Snackbar?.Add("A felhasználó mentése sikerült", Severity.Success);
                    else
                        Snackbar?.Add("A felhasználó módosítása sikerült", Severity.Success);
                }
            }
        }

        private async Task DeleteAsync()
        {
            if (ViewModel is not null && ShowConfirmationDialog is not null)
            {
                ShowConfirmationDialog.Question = $"Valóban törölni akarja a {ViewModel.SelectedItem.HungarianFullName} nevű felhasználót?";
                DialogResult confirmationResult = await ShowConfirmationDialog.Show();

                if (confirmationResult.Cancelled)
                {
                    return;
                }
                else
                {
                    await ViewModel.DeleteItemAsync();
                    if (ViewModel.ErrorStore.HasError)
                        Snackbar?.Add("A felhasználó törlése nem sikerült", Severity.Error);
                    else
                        Snackbar?.Add("A felhasználó törlése sikerült", Severity.Success);
                    GoBack();
                }
            }
        }

        private void GoBack()
        {
            Navigation?.NavigateTo("/user");
        }
    }
}
