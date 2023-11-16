using Authentication.Shared.Models;
using LibraryBlazorClient.Components;
using LibraryBlazorClient.Templates;

namespace Authentication.Client.Pages.RolePage
{
    public partial class RoleIndex : ListViewComponentBase<UserRole>
    {
        private bool _loading;
        private UIComponentState _state => _loading ? UIComponentState.Loading : UIComponentState.Loaded;

    }
}
