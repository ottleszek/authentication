using LibraryCore.Model;
using Microsoft.EntityFrameworkCore;
using System.Reflection;

namespace LibraryDatabase.Model
{
    public static class DbContextExtension
    {
        public static DbSet<TEntity> GetDbSet<TEntity>(this DbContext context) where TEntity : class, IDbRecord<TEntity>, new()
        {
            string dbSetName = new TEntity().GetDbSetName();
            PropertyInfo? propertyInfo = context.GetType().GetProperty(dbSetName);
            if (propertyInfo is null)
                throw new InvalidOperationException($"{dbSetName} tábla kiolvasása sikertelen.");

            DbSet<TEntity>? set;
            try
            {
                if (propertyInfo is not null)
                {
                    var setValue = propertyInfo.GetValue(context);
                    if (setValue is not null)
                    {
                        set = (DbSet<TEntity>)setValue;
                    }
                    else
                        throw new InvalidOperationException($"{dbSetName} adattáblája nem megfelelő típusú.");
                }
                else
                    throw new InvalidOperationException($"{dbSetName} adattáblája nem megfelelő típusú.");
            }
            catch (Exception)
            {
                throw new InvalidOperationException($"{dbSetName} -nek nincs adatbázis táblája.");
            }
            if (set is not null)
            {
                return set;
            }
            else
                throw new InvalidOperationException($"{dbSetName} -nek nincs adatbázis tábla kiolvasása nem sikerült.");

        }
    }
}
