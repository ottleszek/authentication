using Authentication.Shared.Models;
using Microsoft.AspNetCore.Components;

namespace Authentication.Client.Library.Components
{
    public partial class UserComponent
    {
		[Inject] private NavigationManager? Navigation { get; set; }

        private void GoToEditUser(User user)
		{
			if (Navigation is not null)
			{
				Navigation.NavigateTo($"/user/form/{user.Id}");
			}
		}
	}
}
