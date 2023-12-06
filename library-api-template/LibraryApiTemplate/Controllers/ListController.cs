using LibraryCore.Model;
using LibraryDataBroker;
using Microsoft.AspNetCore.Mvc;

namespace LibraryApiTemplate.Controllers
{
    [Route("api/[controller]")]
    public abstract class ListController<TEntity> : ControllerBase, IListController<TEntity> where TEntity : class, IDbRecord<TEntity>, new()
    {
        private readonly IListDataBroker? _repoList;

        public ListController(IListDataBroker repoList)
        {
            _repoList = repoList;
        }

        [HttpGet]
        public async Task<IActionResult> SelectAllRecordToListAsync()
        {
            List<TEntity>? users = new();

            if (_repoList != null)
            {
                users = await _repoList.SelectAllRecordAsync<TEntity>();
                return Ok(users);
            }
            return BadRequest("Az adatok elérhetetlenek!");
        }
    }
}
