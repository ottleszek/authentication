using Authentication.Shared.Models;
using LibraryBlazorClient.Components;
using LibraryClientServiceTemplate.ViewModelsTemplate;
using Microsoft.AspNetCore.Components;
using MudBlazor;

namespace Authentication.Client.Library.Components
{
    public partial class UserTableFullDataViewComponent
    {
        private MudTable<User>? _table;
        private bool _loading = false;
        private UIComponentState _state => _loading ? UIComponentState.Loading : UIComponentState.Loaded;

        [Inject] public NavigationManager? Navigation { get; set; }
        [Inject] public IIncludedListViewModel<User>? ViewModel { get; set; }

        public async Task<TableData<User>> ReloadDataAsync(TableState state)
        {
            if (ViewModel is not null)
            {
                await ViewModel.GetAllDataIncludedToViewModelAsync();
                if (ViewModel.Items is not null)
                {
                    List<User> users = ViewModel.Items.OfType<User>().ToList();
                    TableData<User> data = new()
                    {
                        Items = users,
                        TotalItems = users.Count,
                    };
                    return data;
                }
            }
            return new TableData<User>();
        }

        public void GoToInsert()
        {
            if (Navigation is not null)
                Navigation.NavigateTo("/user/full/form");
        }
    }
}
