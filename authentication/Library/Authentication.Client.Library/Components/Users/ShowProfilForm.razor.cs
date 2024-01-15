using LibraryCore.Errors;
using LibraryBlazorMvvm.Components;
using Microsoft.AspNetCore.Components;
using MudBlazor;
using Authentication.Client.Library.ViewModels.User;
using Authentication.Client.Library.Validation;

namespace Authentication.Client.Library.Components
{
    public partial class ShowProfilForm : ComponentBase
	{
        private MudForm _form = new();
        [Inject] public ProfilViewModel? ViewModel { get; set; }
        [Parameter] public string? UserEmail { get; set; }
        [Inject] ISnackbar? Snackbar { get; set; }

        [Inject] private ProfilValidation? Validation { get; set; }

        protected async override Task OnParametersSetAsync()
        {
            if (!string.IsNullOrEmpty(UserEmail) && ViewModel is not null)
            {
                // Kezdőérték
                ViewModel.Email = UserEmail;
                // Adatok betöltése
                await ViewModel.Loading();
            }
            await base.OnParametersSetAsync();
        }

        protected async Task UpdateProfil()
        {

            if (ViewModel is null)
            {
                Snackbar?.Add("A profil frissítés nem lehetséges!", Severity.Error);
                return;
            }
            ErrorStore errorStore = await ViewModel.UpdateProfilAsync();
            if (errorStore.HasError)
            {
                Snackbar?.Add(errorStore.Message, Severity.Error);
            }
            else
            {
                Snackbar?.Add("A profil frissítés sikerült!", Severity.Success);
            }
        }
    }
}
