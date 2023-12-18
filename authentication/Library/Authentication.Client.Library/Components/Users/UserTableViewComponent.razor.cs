using Authentication.Shared.Models;
using LibraryBlazorClient.Components;
using LibraryClientServiceTemplate.ViewModelsTemplate;
using Microsoft.AspNetCore.Components;
using MudBlazor;

namespace Authentication.Client.Library.Components
{
    public partial class UserTableViewComponent
    {
        private bool _loading = false;
        private UIComponentState _state => _loading ? UIComponentState.Loading : UIComponentState.Loaded;

        [CascadingParameter] public ListAndDeleteViewModel<User>? ViewModel {get; set;}                                                        
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
            if (ViewModel is not null && ViewModel.Items is not null)
            {
                await FetchData.InvokeAsync();

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
