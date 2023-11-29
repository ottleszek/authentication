using LibraryCore.Model;

namespace LibraryClientServiceTemplate.ViewModelsTemplate
{
    public class BaseViewModel<TItem> : IItemViewModel<TItem> where TItem : class, IDbRecord<TItem>, new()
    {
        public TItem? SelectedItem { get; set; }
    }
}
