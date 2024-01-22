using Authentication.Client.Library.Validation;
using Authentication.Shared.Models;
using FluentValidation.Results;
using LibraryBlazorClient.Components;
using LibraryBlazorMvvm.Components;
using LibraryBlazorMvvm.ViewModels;
using LibraryClientServiceTemplate.ViewModelsTemplate;
using Microsoft.AspNetCore.Components;
using MudBlazor;

namespace Authentication.Client.Library.Components
{
    public partial class UserFullForm : MvvmItemComponentBase<User, MvvmCrudViewModelBase<User>>
    {
        private MudForm _form = new();
        private bool _isRegisteredCheckboxDisabled = false;

        [Parameter] public Guid Id { get; set; } = Guid.Empty;
        [Inject] private IListViewModel<UserRole>? UserRoleViewModel { get; set; }
        [Inject] private NavigationManager? Navigation { get; set; }
        [Inject] private MvvmItemFullUserValidation? Validation { get; set; }
        [Inject] private IShowConfirmationDialog? ShowConfirmationDialog { get; set; }
        [Inject] private ISnackbar? Snackbar { get; set; }

        private string _saveButtonText => ViewModel is not null && ViewModel.IsNewItemMode ? "Mentés" : "Módosítás";
        private string _cancelButtonText => ViewModel is not null && ViewModel.IsNewItemMode ? "Mégsem" : "Visszaállítás";

        protected async override Task OnParametersSetAsync()
        {
            if (ViewModel is not null && UserRoleViewModel is not null)
            {
                ViewModel.Id = Id;
                await UserRoleViewModel.GetAllDataToViewModelAsync();
                await ViewModel.Loading();
                if (ViewModel.IsNewItemMode)
                {
                    Guid? newRoleId = null;
                    if (UserRoleViewModel.Items is not null)
                    {
                         newRoleId= UserRoleViewModel.Items.FirstOrDefault()?.Id;
                    }
                    if (newRoleId is not null)
                        ViewModel.SelectedItem.UserRoleId = (Guid) newRoleId ;
                    ViewModel.SelectedItem.IsRegisteredUser = false;
                    _isRegisteredCheckboxDisabled = true;
                }
                else
                    await ViewModel.Loading();
            }
            await base.OnParametersSetAsync();
        }

        private async Task UpdateAsync()
        {
            if (ViewModel is not null && Validation is not null)
            {
                //ValidationResult results = Validation.Validate(ViewModel);
                await _form.Validate();
                if (!_form.IsValid)
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
                    {
                        Snackbar?.Add("A felhasználó mentése sikerült", Severity.Success);
                        GoBack();
                    }
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

        private void SetImgUrl(string imgUrl)
        {
            if (ViewModel is not null) ViewModel.SelectedItem.ProfilImageTimeStamp = imgUrl;
        }

        private void GoBack()
        {
            Navigation?.NavigateTo("/user/full");
        }
    }
}
