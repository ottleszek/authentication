using LibraryCore.Responses;

namespace LibraryClientServiceTemplate.ViewModelsTemplate
{
    public interface IListAndDeleteViewModel<TItem> : IListViewModel<TItem> where TItem : class
    {
        public Task<ControllerResponse> DeleteAsync(Guid id);
    }
}
