using Authentication.Client.Library.ViewModels.User;
using Authentication.Shared.Dtos;
using LibraryMvvm.Base;
using Microsoft.AspNetCore.Components;
using MudBlazor;

namespace Authentication.Client.Library.Components
{
    public partial class ShowProfiForm : MvvmComponentBase<ProfilViewModel>
    {
        [Parameter] public string? UserEmail { get; set; }
        //[Inject] private IProfilViewModel? ProfilViewModel { get; set; }

        private bool _isReadOnly = true;
        private ProfilDto tempProfil = new();

        private List<BreadcrumbItem> _items = new()
        {
            new("Home", href: "#"),
            new("Profil adatok", href: null, disabled: true)
        };

        protected async override Task OnParametersSetAsync()
        {
            if (!string.IsNullOrEmpty(UserEmail) && ViewModel is not null)
            {
                await ViewModel.GetProfilBy(UserEmail);
                tempProfil = ViewModel.ProfilDto;
            }
            await base.OnParametersSetAsync();
        }

        private void ChangeToModify()
        {
            _isReadOnly = false;
        }

        private void ChangeToReadonly()
        {
            if (ViewModel is not null)
            {
                ViewModel.ProfilDto = tempProfil;
                _isReadOnly = true;
            }
        }
    }
}
