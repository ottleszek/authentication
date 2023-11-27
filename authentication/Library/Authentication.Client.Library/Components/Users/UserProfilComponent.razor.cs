using Authentication.Client.Library.ViewModels.User;
using Microsoft.AspNetCore.Components;
using MudBlazor;

namespace Authentication.Client.Library.Components
{
    public partial class UserProfilComponent : UserIdentificationBase
    {
        [Inject] private IProfilViewModel? ProfilViewModel { get; set; }

        private string _userEmail = string.Empty;
        private bool _isReadOnly = true;

        private List<BreadcrumbItem> _items = new List<BreadcrumbItem>
        {
            new BreadcrumbItem("Home", href: "#"),
            new BreadcrumbItem("Profil adatok", href: null, disabled: true)
        };

        protected override async Task OnInitializedAsync()
        {
            _userEmail = await GetUserEmail();
            if (ProfilViewModel is not null)
            {
                await ProfilViewModel.GetProfilBy(_userEmail);
            }

            await base.OnInitializedAsync();
        }
    }
}
