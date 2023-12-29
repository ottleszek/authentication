using Authentication.Shared.Models;
using LibraryApiTemplate.Repos;
using Microsoft.EntityFrameworkCore;
using LibraryDatabase.Model;

namespace Authentication.Server.Repos
{
    public class UserRepo<TDbContext> : RepoIncluded<TDbContext>, IUserRepo where TDbContext : DbContext
    {
        private readonly IDbContextFactory<TDbContext> _dbContextFactory;

        public UserRepo(IDbContextFactory<TDbContext> dbContextFactory) : base(dbContextFactory)
        {
            _dbContextFactory = dbContextFactory;
        }

        public async Task<List<TEntity>> SelectAllUserIncludedAsync<TEntity>(long schoolClassId) where TEntity : User, new()
        {
            IQueryable<TEntity>? entities = GetAllIncluded<TEntity>();
            List<TEntity> result = new List<TEntity>();
            if (entities is null)
                return result;
            else
                return await entities.ToListAsync();
        }

        protected override IQueryable<TEntity>? GetAllIncluded<TEntity>() where TEntity: class
        {
            return GetAllUserIncluded<TEntity>();
        }

        protected IQueryable<TEntity>? GetAllUserIncluded<TEntity>() where TEntity: class
        {
            DbSet<User>? entities = DbSet<User>();
            if (entities is null)
                return null;
            else
            {
                var result = entities.Include(user => user.UserRole);
                return (IQueryable<TEntity>?)result;
            }
        }

        protected DbSet<TEntity>? DbSet<TEntity>() where TEntity : class, new()
        {
            var dbContext = _dbContextFactory.CreateDbContext();
            return dbContext.GetDbSet<TEntity>();
        }
    }
}
