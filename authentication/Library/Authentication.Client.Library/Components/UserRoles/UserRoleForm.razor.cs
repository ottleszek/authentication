﻿using Authentication.Client.Library.Validation;
using Authentication.Shared.Models;
using LibraryBlazorClient.Components;
using LibraryBlazorMvvm.Components;
using LibraryBlazorMvvm.ViewModels;
using Microsoft.AspNetCore.Components;
using MudBlazor;

namespace Authentication.Client.Library.Components
{
    public partial class UserRoleForm : MvvmItemComponentBase<UserRole, MvvmCrudViewModelBase<UserRole>>
    {
        private MudForm _form = new();

        [Parameter] public Guid Id { get; set; } = Guid.Empty;
        [Inject] private NavigationManager? Navigation { get; set; }
        [Inject] private UserValidation? Validation { get; set; }
        [Inject] private IShowConfirmationDialog? ShowConfirmationDialog { get; set; }
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
                    Snackbar?.Add("A szerep sikeressen törölve", Severity.Success);
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
