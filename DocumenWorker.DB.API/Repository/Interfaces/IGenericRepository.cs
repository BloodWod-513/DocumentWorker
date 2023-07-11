namespace DocumenWorker.DB.API.Repository.Interfaces
{
    public interface IGenericRepository<TEntity> 
        where TEntity : class
    {
        bool Add(TEntity item);
        bool AddRange(List<TEntity> entities);
        bool UpdateRange(List<TEntity> entities);
        bool Update(TEntity item);
        bool Remove(TEntity item);
        bool Remove(int id);
        bool RemoveRange(List<TEntity> item);
        TEntity FindById(int id);
        List<TEntity> Get();
        List<TEntity> Get(Func<TEntity, bool> predicate);
    }
}
