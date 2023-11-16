using LibraryCore.Model;

namespace LibraryClientServiceTemplate.Extensions
{
    public class RelativeUrlExtension
    {
        public static string SetRelativeUrl<TEntity>() where TEntity : class, IDbRecord<TEntity>, new()
        {
            return $"/api/{typeof(TEntity).Name}";
        }
    }
}
