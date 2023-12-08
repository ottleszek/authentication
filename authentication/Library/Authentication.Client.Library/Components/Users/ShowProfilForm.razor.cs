using LibraryCore.Errors;
using LibraryBlazorMvvm.Components;
using Microsoft.AspNetCore.Components;
using MudBlazor;
using Authentication.Client.Library.ViewModels.User;
using Authentication.Client.Library.Validation;

namespace Authentication.Client.Library.Components
{
    public partial class ShowProfilForm : MvvmComponentBase<ProfilViewModel>
	{
        [Parameter] public string? UserEmail { get; set; }
        [Inject] ISnackbar? Snackbar { get; set; }

        [Inject] private ProfilValidation? Validation { get; set; }
        private MudForm _form = new();

        private List<BreadcrumbItem> _items = new()
        {
            new("Home", href: "#"),
            new("Profil adatok", href: null, disabled: true)
        };

        protected async override Task OnParametersSetAsync()
        {
            if (!string.IsNullOrEmpty(UserEmail) && ViewModel is not null)
            {
                ViewModel.Email = UserEmail;
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
            ErrorStore errorStore = await ViewModel.UpdateProfil();
            if (errorStore.HasError)
            {
                Snackbar?.Add(errorStore.Error, Severity.Error);
            }
            Snackbar?.Add("A profil frissítés sikerült!", Severity.Success);
        }
    }
}
