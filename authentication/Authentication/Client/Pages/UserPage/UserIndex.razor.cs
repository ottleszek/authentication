using Authentication.Shared.Models;
using LibraryBlazorClient.Components;
using LibraryBlazorClient.Templates;

namespace Authentication.Client.Pages.UserPage
{
    public partial class UserIndex : ListViewComponentBase<User>
    {
        private bool _loading;
        private UIComponentState _state => _loading ? UIComponentState.Loading : UIComponentState.Loaded;
    }
}
