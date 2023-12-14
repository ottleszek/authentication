using LibraryCore.Model;
using LibraryCore.Responses;
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

        public async Task<IActionResult> DeleteAsync(Guid id)
        {
            ControllerResponse result =  await _crudDataBroker.DeleteAsync<TEntity>(id);
            if (result.HasError )
            {
                return BadRequest(result);
            }
            else
            {
                return Ok(result);
            }
        }
    }
}
