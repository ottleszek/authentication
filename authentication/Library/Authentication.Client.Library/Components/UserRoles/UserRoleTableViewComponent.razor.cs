using LibraryBlazorClient.Components;
using LibraryBlazorClient.Templates;
using Authentication.Shared.Models;

namespace Authentication.Client.Library.Components
{
    public partial class UserRoleTableViewComponent : ListViewComponentBase<UserRole>
    {
        private bool _loading;
        private UIComponentState _state => _loading ? UIComponentState.Loading : UIComponentState.Loaded;

    }
}
