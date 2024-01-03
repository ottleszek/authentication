using LibraryCore.Model;
using LibraryCore.Responses;
using LibraryDataBroker;
using Microsoft.AspNetCore.Mvc;

namespace LibraryApiTemplate.Controllers
{
    [Route("api/[controller]")]
    public abstract class UpdateController<TEntity> : ControllerBase, IUpdateController<TEntity> where TEntity : class, IDbRecord<TEntity>, new()
    {
        private readonly IUpdateDataBroker? _repoUpdate;

        public UpdateController(IUpdateDataBroker repoUpdate)
        {
            _repoUpdate=repoUpdate;
        }

        [HttpPut()]
        public async Task<ActionResult> UpdateAsync(TEntity entity)
        {
            ControllerResponse response = new();
            if (_repoUpdate is not null)
            {
                response = await _repoUpdate.UpdateAsync(entity);

                if (response.HasError)
                {
                    return BadRequest(response);
                }
                else
                {
                    return Ok(response);
                }
            }
            response.ClearAndAddError("Az adatok frissítés nem lehetséges!");
            return BadRequest(response);
        }
    }
}
