namespace LibraryClientServiceTemplate.ViewModelsTemplate
{
    public interface IItemViewModel<TItem> where TItem : class
    {
        public TItem? SelectedItem { get; set; }
        public bool HasSelectedItem => SelectedItem is not null;
    }
}
