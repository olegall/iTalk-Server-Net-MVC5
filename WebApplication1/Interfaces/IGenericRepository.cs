using System.Collections.Generic;
using System.Threading.Tasks;

namespace WebApplication1
{
    public interface IGenericRepository<TEntity> where TEntity : class
    {
        IEnumerable<TEntity> Get();
        Task<TEntity> GetAsync(long id);
        void CreateAsync(TEntity item);
        void UpdateAsync(TEntity item);
        void DeleteAsync(TEntity item);
    }
}