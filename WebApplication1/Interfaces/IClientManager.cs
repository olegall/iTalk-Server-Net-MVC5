using System.Collections.Specialized;
using System.Threading.Tasks;
using WebApplication1.Models;
using System.Collections.Generic;

namespace WebApplication1.Interfaces
{
    public interface IClientManager
    {
        Task CreateAsync(string name, string phone);
        Client GetAsync(long id, bool adPush);
        Task UpdateAsync(long id, string name, bool adPush);
        Task DeleteAsync(long id);
        IEnumerable<Client> GetAllTest();
    }
}