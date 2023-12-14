using LibraryCore.Model;
using LibraryDataBroker;
using Microsoft.AspNetCore.Mvc;

namespace LibraryApiTemplate.Controllers
{
    public class ListAndDeleteController<TEntity> : ListController<TEntity>, IListDeleteController<TEntity> where TEntity : class, IDbRecord<TEntity>, new()
    {
        private readonly ICrudDataBroker _crudDataBroker;

        public ListAndDeleteController(ICrudDataBroker crudDataBroker, IListDataBroker repoList, IGetDataBroker repoGet) : base(repoList, repoGet)
        {
            _crudDataBroker = crudDataBroker;
        }

        public Task<IActionResult> DeleteAsync(Guid id)
        {
            return _crudDataBroker.DeleteAsync(id);
        }
    }
}
