namespace DocumenWorker.DB.API.Repository.Interfaces
{
    public interface IGenericRepository<TEntity> 
        where TEntity : class
    {
        bool Add(TEntity item);
        bool Update(TEntity item);
        bool Remove(TEntity item);
        bool Remove(int id);
        TEntity FindById(int id);
        IEnumerable<TEntity> Get();
        IEnumerable<TEntity> Get(Func<TEntity, bool> predicate);
    }
}
