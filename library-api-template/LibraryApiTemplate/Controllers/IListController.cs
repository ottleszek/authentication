using LibraryCore.Model;
using Microsoft.AspNetCore.Mvc;

namespace LibraryApiTemplate.Controllers
{
    public interface IListController<TEntity> where TEntity : class, IDbRecord<TEntity>, new()
    {
        public Task<IActionResult> SelectAllRecordToListAsync();
        public Task<IActionResult> GetBy(Guid id);
    }
}
