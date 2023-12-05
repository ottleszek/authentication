using System.ComponentModel;

namespace LibraryBlazorMvvm.ViewModels
{
    public interface IMvvmViewModelBase : INotifyPropertyChanged
    {
        Task OnInitializedAsync();
        Task Loading();
    }
}
