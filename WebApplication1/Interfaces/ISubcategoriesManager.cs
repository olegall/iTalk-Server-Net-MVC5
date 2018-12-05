using System.Threading.Tasks;
using System.Collections.Generic;
using WebApplication1.Models;
using WebApplication1.Utils;

namespace WebApplication1.Interfaces
{
    public interface ISubcategoriesManager
    {
        Task<CRUDResult<Client>> CreateAsync(string name, string phone);
        CRUDResult<Client> GetAsync(long id, bool adPush);
        Task<CRUDResult<Client>> UpdateAsync(long id, string name, bool adPush);
        Task<CRUDResult<Client>> DeleteAsync(long id);

        IEnumerable<Client> GetAllTest();
    }
}