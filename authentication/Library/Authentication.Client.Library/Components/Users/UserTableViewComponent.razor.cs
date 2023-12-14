using Authentication.Shared.Models;
using LibraryBlazorClient.Components;
using LibraryBlazorClient.Templates;
using Microsoft.AspNetCore.Components;
using MudBlazor;

namespace Authentication.Client.Library.Components
{ 
    public partial class UserTableViewComponent : ListViewComponentBase<User>
    {
        private bool _loading = false;
        private UIComponentState _state => _loading ? UIComponentState.Loading : UIComponentState.Loaded;

        [Parameter] public EventCallback<User> OnEditClick { get; set; }

        public async Task<TableData<User>> ReloadDataAsync(TableState state)
        {
            
            if (ViewModel is not null)
            {
                List<User> users = await ReloadDataAsync();
                TableData<User> data = new()
                {
                    Items = users,
                    TotalItems = users.Count,
                };
                return data;
            }
            return new TableData<User>();
        }
    }
}
