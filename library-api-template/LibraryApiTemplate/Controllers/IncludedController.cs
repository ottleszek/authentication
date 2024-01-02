using LibraryCore.Model;
using LibraryDataBroker;
using Microsoft.AspNetCore.Mvc;

namespace LibraryApiTemplate.Controllers
{
    public abstract class IncludedController<TEntity> : ListController<TEntity>, IIncludedController<TEntity> where TEntity : class, IDbRecord<TEntity>, new()
    {
        private readonly IIncludedDataBroker? _repoIncluded;

        protected IncludedController(IIncludedDataBroker repoIncluded, IListDataBroker repoList, IGetDataBroker repoGet) : base(repoList, repoGet)
        {
            _repoIncluded = repoIncluded;
        }

        [HttpGet("included")]
        public async Task<IActionResult> SelectAllIncludedRecordToListAsync()
        {
            if (_repoIncluded is not null)
            {
                List<TEntity> result = await _repoIncluded.SelectAllRecordIncludedToListAsync<TEntity>();
                return Ok(result);
            }
            else
                return Ok(new List<TEntity>());
        }
    }
}
