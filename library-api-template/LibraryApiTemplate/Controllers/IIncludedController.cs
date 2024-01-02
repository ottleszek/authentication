using LibraryCore.Model;
using Microsoft.AspNetCore.Mvc;

namespace LibraryApiTemplate.Controllers
{
    public interface IIncludedController<TEntity> where TEntity : class, IDbRecord<TEntity>, new()
    {
        public Task<IActionResult> SelectAllIncludedRecordToListAsync();
    }
}
