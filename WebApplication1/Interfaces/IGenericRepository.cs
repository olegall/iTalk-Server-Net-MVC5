using System.Collections.Generic;
using System.Threading.Tasks;

namespace WebApplication1
{
    public interface IGenericRepository<TEntity> where TEntity : class
    {
        IEnumerable<TEntity> Get();
        //Task<TEntity> GetAsync(long id);
        TEntity GetAsync(long id);
        //Task GetAsync(long id);
        Task CreateAsync(TEntity item);
        Task UpdateAsync(TEntity item);
        Task DeleteAsync(TEntity item);
        //void DeleteAsync(long id);
    }
}