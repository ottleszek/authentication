using LibraryCore.Model;
using LibraryCore.Responses;
using LibraryDataBroker;
using Microsoft.AspNetCore.Mvc;

namespace LibraryApiTemplate.Controllers
{
    [Route("api/[controller]")]
    public abstract class UpdateController<TEntity> : ControllerBase, IUpdateController<TEntity> where TEntity : class, IDbRecord<TEntity>, new()
    {
        private readonly IGetDataBroker? _repo;
        private readonly IUpdateDataBroker? _repoUpdate;

        public UpdateController(IUpdateDataBroker repoUpdate)
        {
            _repoUpdate=repoUpdate;
        }

        public async Task<ActionResult> UpdateAsync(TEntity entity)
        {
            ControllerResponse response = new ControllerResponse();
            if (_repoUpdate is not null)
            {
                response = await _repoUpdate.UpdateAsync(entity);

                if (!response.HasError)
                {
                    return Ok(response);
                }
                else
                {
                    return BadRequest(response);
                }
            }
            response.ClearAndAddError("Az adatok frissítés nem lehetséges!");
            return BadRequest(response);
        }
    }
}
