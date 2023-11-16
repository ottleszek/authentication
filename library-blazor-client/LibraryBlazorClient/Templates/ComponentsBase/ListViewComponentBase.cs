using LibraryClientServiceTemplate.ViewModelsTemplate;
using LibraryCore.Model;
using Microsoft.AspNetCore.Components;

namespace LibraryBlazorClient.Templates
{
    public class ListViewComponentBase<TItem> : ComponentBase where TItem : class, IDbRecord<TItem>, new()
    {
        [Inject] protected IListViewModel<TItem>? ViewModel { get; set; }

        protected async override Task OnParametersSetAsync()
        {
            if (ViewModel is not null)
            {
                await ViewModel.GetAllDataToViewModel();
            }
        }
    }
}
