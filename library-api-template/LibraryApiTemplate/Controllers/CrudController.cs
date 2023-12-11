using LibraryApiTemplate.Repos;
using LibraryCore.Model;
using LibraryCore.Responses;
using LibraryDataBroker;
using Microsoft.AspNetCore.Mvc;

namespace LibraryApiTemplate.Controllers
{
    [Route("api/[controller]")]
    public abstract class CrudController<TEntity> : UpdateController<TEntity>, ICrudController<TEntity> where TEntity : class, IDbRecord<TEntity>, new()
    {
        private readonly ICrudDataBroker? _dataBroker;

        protected CrudController(ICrudDataBroker dataBroker, IUpdateDataBroker repoUpdate) : base(repoUpdate)
        {
            _dataBroker = dataBroker;
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteAsync(Guid id)
        {
            ControllerResponse response = new ControllerResponse();
            if (_dataBroker is not null)
            {
                response = await _dataBroker.DeleteAsync<TEntity>(id);

                if (!response.HasError)
                {
                    return Ok(response);
                }
                else
                {
                    return BadRequest(response);
                }
            }
            response.ClearAndAddError("Az adatok törlése nem lehetséges!");
            return BadRequest(response);
        }

        [HttpPost()]
        public async Task<IActionResult> InsertAsync(TEntity entity)
        {
            ControllerResponse response = new ControllerResponse();
            if (_dataBroker is not null)
            {
                response = await _dataBroker.InsertAsync(entity);

                if (!response.HasError)
                {
                    return Ok(response);
                }
                else
                {
                    return BadRequest(response);
                }
            }
            response.ClearAndAddError("Új adatok mentése nem lehetséges!");
            return BadRequest(response);
        }
    }
}
