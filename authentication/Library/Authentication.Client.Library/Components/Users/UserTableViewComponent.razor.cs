using Authentication.Shared.Models;
using LibraryBlazorClient.Components;
using Microsoft.AspNetCore.Components;
using MudBlazor;

namespace Authentication.Client.Library.Components
{ 
    public partial class UserTableViewComponent 
    {
        private bool _loading = false;
        private UIComponentState _state => _loading ? UIComponentState.Loading : UIComponentState.Loaded;

        [Parameter] public EventCallback FetchData { get; set; }
        [Parameter] public EventCallback<User> EditClick { get; set; }
        [Parameter] public EventCallback<User> DeleteClick { get; set; }

        protected override async Task OnInitializedAsync()
        {
            await FetchData.InvokeAsync();
            await base.OnInitializedAsync();
        }

        public async Task<TableData<User>> ReloadDataAsync(TableState state)
        {

                List<User> users = await ReloadDataAsync();
                TableData<User> data = new()
                {
                    Items = users,
                    TotalItems = users.Count,
                };
                return data;
        }
    }
}
