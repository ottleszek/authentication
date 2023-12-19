using LibraryBlazorClient.Components;
using Authentication.Shared.Models;
using LibraryClientServiceTemplate.ViewModelsTemplate;
using Microsoft.AspNetCore.Components;
using AKSoftware.Blazor.Utilities;
using MudBlazor;

namespace Authentication.Client.Library.Components
{
    public partial class UserRoleTableViewComponent
    {
        private MudTable<UserRole>? _table;
        private bool _loading;
        private UIComponentState _state => _loading ? UIComponentState.Loading : UIComponentState.Loaded;

        [CascadingParameter] public IListAndDeleteViewModel<UserRole>? ViewModel { get; set; }

        [Parameter] public EventCallback FetchData { get; set; }
        [Parameter] public EventCallback<UserRole> EditClick { get; set; }
        [Parameter] public EventCallback<UserRole> DeleteClick { get; set; }
        [Parameter] public EventCallback InsertClick { get; set; }


        protected override async Task OnInitializedAsync()
        {
            MessagingCenter.Subscribe<UserRoleComponent, UserRole>(this, "userrole_deleted", async (sender, args) =>
            {
                if (_table is not null)
                {
                    await _table.ReloadServerData();
                    StateHasChanged();
                }
            });
            await base.OnInitializedAsync();
        }

        public async Task<TableData<UserRole>> ReloadDataAsync(TableState state)
        {
            await FetchData.InvokeAsync();
            if (ViewModel is not null && ViewModel.Items is not null)
            {
                List<UserRole> userRoles = ViewModel.Items.OfType<UserRole>().ToList();
                TableData<UserRole> data = new()
                {
                    Items = userRoles,
                    TotalItems = userRoles.Count,
                };
                return data;
            }
            return new TableData<UserRole>();
        }
    }
}
