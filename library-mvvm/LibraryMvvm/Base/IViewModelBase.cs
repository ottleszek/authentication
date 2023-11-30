using System.ComponentModel;

namespace LibraryMvvm.Base
{
    public interface IViewModelBase : INotifyPropertyChanged
    {
        Task OnInitializedAsync();
        Task Loading();
    }
}
