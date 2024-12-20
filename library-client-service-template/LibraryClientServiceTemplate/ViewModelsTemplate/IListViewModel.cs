﻿namespace LibraryClientServiceTemplate.ViewModelsTemplate
{
    public interface IListViewModel<TItem> : IItemViewModel<TItem> where TItem : class
    {
        public List<TItem>? Items { get; set; }
        public bool HasItems { get; }
        public abstract Task GetAllDataToViewModelAsync();
        public Task<List<TItem>> ReloadDataAsync();
    }
}
