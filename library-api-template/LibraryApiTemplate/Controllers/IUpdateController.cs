using LibraryCore.Model;
using Microsoft.AspNetCore.Mvc;

namespace LibraryApiTemplate.Controllers
{
    public interface IUpdateController<TEntity> where TEntity : class, IDbRecord<TEntity>, new()
    {
        public Task<ActionResult> UpdateAsync(TEntity entity);
    }
}
