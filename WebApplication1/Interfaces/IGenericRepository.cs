using System.Collections.Generic;

namespace WebApplication1
{
    public interface IGenericRepository<TEntity> where TEntity : class
    {
        void Create(TEntity item);
        IEnumerable<TEntity> Get();
        void Update(TEntity item);
        void Delete(TEntity item);
    }
}