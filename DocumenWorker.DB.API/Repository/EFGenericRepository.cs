using DocumenWorker.DB.API.Context.Interfaces;
using DocumenWorker.DB.API.Repository.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace DocumenWorker.DB.API.Repository
{
    public class EFGenericRepository<TEntity> : IGenericRepository<TEntity> 
        where TEntity : class
    {
        IDBContext _context;
        DbSet<TEntity> _dbSet;

        public EFGenericRepository(IDBContext context)
        {
            _context = context;
            _dbSet = context.Set<TEntity>();
        }

        public IEnumerable<TEntity> Get()
        {
            return _dbSet.AsNoTracking().ToList();
        }

        public IEnumerable<TEntity> Get(Func<TEntity, bool> predicate)
        {
            return _dbSet.AsNoTracking().Where(predicate).ToList();
        }
        public TEntity FindById(int id)
        {
            return _dbSet.Find(id);
        }

        public bool Add(TEntity item)
        {
            _dbSet.Add(item);
            return CheckSave();
        }
        public bool AddRange(List<TEntity> items)
        {
            _dbSet.AddRange(items);
            return CheckSave();
        }
        public bool UpdateRange(List<TEntity> items)
        {
            _dbSet.UpdateRange(items);
            return CheckSave();
        }
        public bool Update(TEntity item)
        {
            _context.Entry(item).State = EntityState.Modified;
            return CheckSave();
        }
        public bool Remove(TEntity item)
        {
            _dbSet.Remove(item);
            return CheckSave();
        }
        public bool Remove(int id)
        {
            var entity = FindById(id);
            _dbSet.Remove(entity);
            return CheckSave();
        }

        private bool CheckSave() => _context.SaveChanges() > 0;
    }
}
