using System.ComponentModel;

namespace LibraryBlazorMvvm.ViewModels
{
    public interface IViewModelBase : INotifyPropertyChanged
    {
        Task OnInitializedAsync();
        Task Loading();
    }
}
