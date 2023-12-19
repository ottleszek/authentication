using LibraryCore.Model;
using Microsoft.AspNetCore.Mvc;

namespace LibraryApiTemplate.Controllers
{
    public interface IListDeleteController <TEntity> where TEntity : class, IDbRecord<TEntity>, new()
    {
        public Task<IActionResult> DeleteAsync(Guid id);
    }
}
