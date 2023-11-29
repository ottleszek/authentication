using Authentication.Client.Library.ViewModels.User;
using Authentication.Shared.Dtos;
using Microsoft.AspNetCore.Components;
using MudBlazor;

namespace Authentication.Client.Library.Components
{
    public partial class ShowProfiForm
    {
        [Parameter] public string? UserEmail { get; set; }
        [Inject] private IProfilViewModel? ProfilViewModel { get; set; }

        private bool _isReadOnly = true;
        private ProfilDto tempProfil = new();

        private List<BreadcrumbItem> _items = new()
        {
            new ("Home", href: "#"),
            new ("Profil adatok", href: null, disabled: true)
        };

        protected async override Task OnParametersSetAsync()
        {
            if (!string.IsNullOrEmpty(UserEmail) && ProfilViewModel is not null)
            {
                await ProfilViewModel.GetProfilBy(UserEmail);
                tempProfil = ProfilViewModel.ProfilDto;
            }
            await base.OnParametersSetAsync();
        }

        private void ChangeToModify()
        {
            _isReadOnly = false;
        }

        private void ChangeToReadonly()
        {
            if (ProfilViewModel is not null)
            {
                ProfilViewModel.ProfilDto = tempProfil;
                _isReadOnly = true;
            }
        }
    }
}
