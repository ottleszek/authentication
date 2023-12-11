using LibraryCore.Model;
using Microsoft.AspNetCore.Mvc;

namespace LibraryApiTemplate.Controllers
{
    public interface ICrudController<TEntity> where TEntity : class, IDbRecord<TEntity>, new()
    {
        public Task<IActionResult> InsertAsync(TEntity entity);
        public Task<IActionResult> DeleteAsync(Guid id);
    }
}
