using LibraryCore.Model;
using LibraryDataBroker;
using Microsoft.AspNetCore.Mvc;

namespace LibraryApiTemplate.Controllers
{
    [Route("api/[controller]")]
    public abstract class GetController<TEntity> : ControllerBase, IGetController<TEntity> where TEntity : class, IDbRecord<TEntity>, new()
    {
        private readonly IGetDataBroker? _repo;

        public GetController(IGetDataBroker repo)
        {
            this._repo = repo;
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetBy(Guid id)
        {
            TEntity entity = new();
            if (_repo is not null)
            {
                entity = await _repo.GetByAsnyc<TEntity>(id);
                return Ok(entity);
            }
            return BadRequest("Az adatok elérhetetlenek!");
        }
    }
}
