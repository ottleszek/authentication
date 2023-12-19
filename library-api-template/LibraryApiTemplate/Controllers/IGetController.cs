using LibraryCore.Model;
using Microsoft.AspNetCore.Mvc;

namespace LibraryApiTemplate.Controllers
{
    public interface IGetController<TEntity> where TEntity : class, IDbRecord<TEntity>, new()
    {
        public Task<IActionResult> GetBy(Guid id);
    }
}
