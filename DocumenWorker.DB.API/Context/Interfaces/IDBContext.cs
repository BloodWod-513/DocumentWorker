using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;

namespace DocumenWorker.DB.API.Context.Interfaces
{
    public interface IDBContext
    {
        DbSet<TEntity> Set<TEntity>() where TEntity : class;

        int SaveChanges();
        EntityEntry Entry(object entity);

    }
}
