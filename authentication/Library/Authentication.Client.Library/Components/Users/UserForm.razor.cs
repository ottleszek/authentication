using Authentication.Client.Library.Validation;
using Authentication.Shared.Models;
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
        [Inject] private UserValidation? Validation { get; set; }
        [Inject] private IDialogService? DialogService { get; set; }
        [Inject] private ISnackbar? Snackbar { get; set; }


        protected async override Task OnParametersSetAsync()
        {
            if (ViewModel is not null)
            {
                ViewModel.Id = Id;
                Validation = new UserValidation();

                await ViewModel.Loading();
            }
            await base.OnParametersSetAsync();
        }

        private void GoBack()
        {
            if (Navigation is not null)
                Navigation.NavigateTo("/user");
        }

        private async Task DeleteAsync()
        {
            if (ViewModel is not null && DialogService is not null)
            {
                var parameters = new DialogParameters();
                parameters.Add("ContentText", $"Valóban törölni akarja a {ViewModel.SelectedItem.HungarianFullName} nevű felhasználót?");
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
