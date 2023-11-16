using LibraryCore.Model;

namespace LibraryApiTemplate.Repos
{
    public interface IRepoQueryble
    {
        public IQueryable<TEntity> SelectAllRecord<TEntity>() where TEntity : class, IDbRecord<TEntity>, new();
        public Task<TEntity> GetById<TEntity>(Guid id) where TEntity : class, IDbRecord<TEntity>, new(); 
    }
}
