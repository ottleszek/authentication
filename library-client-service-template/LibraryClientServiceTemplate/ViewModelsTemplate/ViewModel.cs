using LibraryCore.Model;

namespace LibraryClientServiceTemplate.ViewModelsTemplate
{
    public class ViewModel<TItem> : IItemViewModel<TItem> where TItem : class, IDbRecord<TItem>, new()
    {
        public TItem? SelectedItem { get; set; }
    }
}
