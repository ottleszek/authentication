namespace LibraryClientServiceTemplate.ViewModelsTemplate
{
    public interface IIncludedListViewModel<TItem> : IListViewModel<TItem> where TItem : class
    {
        public Task GetAllDataIncludedToViewModelAsync();
        public Task<List<TItem>> ReloadIncludedDataAsync();
    }
}
