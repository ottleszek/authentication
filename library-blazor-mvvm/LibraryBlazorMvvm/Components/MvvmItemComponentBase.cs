using LibraryBlazorMvvm.ViewModels;
using LibraryCore.Model;
using Microsoft.AspNetCore.Components;

namespace LibraryBlazorMvvm.Components
{
    public class MvvmItemComponentBase<TItem,TViewModel> : ComponentBase
        where TItem : class, IDbRecord<TItem>, new()
        where TViewModel : IMvvmUserViewModelBase //IMvvmItemViewModelBase<TItem>

	{
        [Inject]
        protected TViewModel? ViewModel { get; set; }

        protected override void OnInitialized()
        {
            /*TItem u = new TItem();

            IHttpClientFactory factory = new HttpClient();
            IGetDataBroker broker = new GetHttpService(factory);
            IGetBrokerConnector<TItem> connector= new GetBrokerConnector<TItem>(broker);
            TViewModel ViewModel = new MvvmItemViewModelBase<TItem>(connector);

            IMvvmItemViewModelBase<TItem> VM=new MvvmItemViewModelBase<TItem>(connector);*/

            if (ViewModel is not null)
            {
                //ViewModel.PropertyChanged += (_, _) => StateHasChanged();
                base.OnInitialized();
            }
        }

        protected override Task OnInitializedAsync()
        {
            if (ViewModel is not null)
            {
                //return ViewModel.OnInitializedAsync();
            }
            else { return Task.CompletedTask; }
            return Task.CompletedTask;
        }
    }

    
}
