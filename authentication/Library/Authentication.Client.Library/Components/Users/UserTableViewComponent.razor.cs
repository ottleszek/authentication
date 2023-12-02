using Authentication.Shared.Models;
using LibraryBlazorClient.Components;
using LibraryBlazorClient.Templates;

namespace Authentication.Client.Library.Components
{ 
    public partial class UserTableViewComponent : ListViewComponentBase<User>
    {
        private bool _loading = false;
        private UIComponentState _state => _loading ? UIComponentState.Loading : UIComponentState.Loaded;
    }
}
