using LibraryClientServiceTemplate.ViewModelsTemplate;
using LibraryCore.Model;
using LibraryCore.Responses;
using Microsoft.AspNetCore.Components;

namespace LibraryBlazorClient.Templates.ComponentsBase
{
    public class ListAndDeleteViewComponentBase<TItem> : ComponentBase where TItem : class, IDbRecord<TItem>, new()
    {
        [Inject] protected IListAndDeleteViewModel<TItem>? ViewModel { get; set; }

        protected async Task<List<TItem>> ReloadDataAsync()
        {
            if (ViewModel is not null)
            {
                return await ViewModel.ReloadDataAsync();
            }
            return new List<TItem>();
        }

        protected async Task<ControllerResponse> DeleteAsync(Guid id)
        {
            if (ViewModel is not null)
            {
                return await ViewModel.DeleteAsync(id);
            }

            return new ControllerResponse
            {
                Error = "Az elem nem törölhető"
            };
        }
    }
}
