using System.Threading.Tasks;
using WebApplication1.Models;
using System.Collections.Generic;
using WebApplication1.Utils;

namespace WebApplication1.Interfaces
{
    public interface IClientManager
    {
        Task<CRUD.Result> CreateAsync(string name, string phone);
        object GetAsync(long id, bool adPush);
        Task<CRUD.Result> UpdateAsync(long id, string name, bool adPush);
        Task<CRUD.Result> DeleteAsync(long id);

        IEnumerable<Client> GetAllTest();
    }
}