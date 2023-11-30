using Authentication.Client.Library.ViewModels.User;
using LibraryMvvm.Base;
using Microsoft.AspNetCore.Components;
using MudBlazor;

namespace Authentication.Client.Library.Components
{
    public partial class ShowProfiForm : MvvmComponentBase<ProfilViewModel>
    {
        [Parameter] public string? UserEmail { get; set; }

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
    }
}
