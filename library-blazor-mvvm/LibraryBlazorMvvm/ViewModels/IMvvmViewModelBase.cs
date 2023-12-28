using System.ComponentModel;

namespace LibraryBlazorMvvm.ViewModels
{
	public interface IMvvmViewModelBase : INotifyPropertyChanged
    {
        public bool IsBusy { get; }
        Task OnInitializedAsync();
        Task Loading();
    }
}
