using System.ComponentModel;

namespace LibraryBlazorMvvm.Base
{
    public interface IViewModelBase : INotifyPropertyChanged
    {
        Task OnInitializedAsync();
        Task Loading();
    }
}
