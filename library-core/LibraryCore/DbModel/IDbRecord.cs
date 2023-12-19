namespace LibraryCore.Model
{
    public interface IDbRecord<TEntity> : ICloneable where TEntity : class,new()
    {
        public string GetDbSetName() => new TEntity().GetType().Name; // typeof(TEntity).Name;
        public Guid Id { get; set; }

        public bool HasId => Id==Guid.Empty ? false : true;

    }
}
