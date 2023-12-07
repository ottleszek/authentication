using System.ComponentModel;

namespace LibraryBlazorMvvm.ViewModels
{
    public interface IMvvmUserViewModelBase 
    {
    }


	public interface IMvvmViewModelBase : INotifyPropertyChanged
    {
        Task OnInitializedAsync();
        Task Loading();
    }
}
