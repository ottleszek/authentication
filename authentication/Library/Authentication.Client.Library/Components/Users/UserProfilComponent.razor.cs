namespace Authentication.Client.Library.Components.Users
{
    public partial class UserProfilComponent : UserIdentificationBase
    {
        private string email = string.Empty;

        protected override async Task OnInitializedAsync()
        {
            email = await GetUserEmail();
            await base.OnInitializedAsync();
        }
    }
}
