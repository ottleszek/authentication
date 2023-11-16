namespace LibraryCore.Model
{
    public interface IDbRecord<TEntity> where TEntity : class,new()
    {
        public string GetDbSetName() => new TEntity().GetType().Name; // typeof(TEntity).Name;
        public Guid Id { get; set; }
    }
}
