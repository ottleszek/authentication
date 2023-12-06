using Authentication.Client.Library.ViewModels.User;
using Authentication.Shared.Models;
using LibraryBlazorMvvm.Components;
using Microsoft.AspNetCore.Components;

namespace Authentication.Client.Library.Components
{ 
    public partial class UserForm : MvvmItemComponentBase<User, UserViewModel<User>>
    {
        [Parameter] public Guid Id { get; set; } = Guid.Empty;

        protected override Task OnParametersSetAsync()
        {
            if (ViewModel is not null)
            {
                ViewModel.Id = Id;
            }
            return base.OnParametersSetAsync();
        }

        private async Task SubmitFormAsync()
        { }
    }
}
