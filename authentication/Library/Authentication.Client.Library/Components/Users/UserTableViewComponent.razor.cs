using AKSoftware.Blazor.Utilities;
using Authentication.Shared.Models;
using LibraryBlazorClient.Components;
using LibraryClientServiceTemplate.ViewModelsTemplate;
using Microsoft.AspNetCore.Components;
using MudBlazor;

namespace Authentication.Client.Library.Components
{
    public partial class UserTableViewComponent
    {
        private MudTable<User>? _table;
        private bool _loading = false;
        private UIComponentState _state => _loading ? UIComponentState.Loading : UIComponentState.Loaded;

        [CascadingParameter] public IListAndDeleteViewModel<User>? ViewModel {get; set;}
        [CascadingParameter] public int Adat { get; set;}

        [Parameter] public EventCallback FetchData { get; set; }
        [Parameter] public EventCallback<User> EditClick { get; set; }
        [Parameter] public EventCallback<User> DeleteClick { get; set; }

        protected override async Task OnInitializedAsync()
        {
            MessagingCenter.Subscribe<UserComponent, User>(this, "user_deleted", async (sender, args) =>
            {
                if (_table is not null)
                {
                    await _table.ReloadServerData();
                    StateHasChanged();
                }
            });
            await base.OnInitializedAsync();
        }

        public async Task<TableData<User>> ReloadDataAsync(TableState state)
        {
            await FetchData.InvokeAsync();
            if (ViewModel is not null && ViewModel.Items is not null)
            {
                List<User> users = ViewModel.Items.OfType<User>().ToList();
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
