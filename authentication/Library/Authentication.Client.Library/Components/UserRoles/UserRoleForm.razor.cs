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
        [Inject] private MvvmItemUserRoleValidation? Validation { get; set; }
        [Inject] private IShowConfirmationDialog? ShowConfirmationDialog { get; set; }
        [Inject] private ISnackbar? Snackbar { get; set; }

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
            if (ViewModel is not null)
            {
                await ViewModel.UpdateAsync();
                if (ViewModel.ErrorStore.HasError)
                    Snackbar?.Add("A szerep módosítás nem sikerült", Severity.Error);
                else
                    Snackbar?.Add("A szerep módosítása sikerült", Severity.Success);
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