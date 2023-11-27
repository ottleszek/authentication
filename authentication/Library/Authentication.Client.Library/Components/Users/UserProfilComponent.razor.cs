using Authentication.Client.Library.ViewModels.User;
using Microsoft.AspNetCore.Components;

namespace Authentication.Client.Library.Components.Users
{
    public partial class UserProfilComponent : UserIdentificationBase
    {
        private string _userEmail = string.Empty;
        [Inject] IProfilViewModel? ProfilViewModel {get; set;}


        protected override async Task OnInitializedAsync()
        {
            _userEmail = await GetUserEmail();


            await base.OnInitializedAsync();
        }
    }
}
