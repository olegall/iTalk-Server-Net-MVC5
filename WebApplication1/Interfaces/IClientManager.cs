using System.Collections.Specialized;
using System.Threading.Tasks;
using WebApplication1.Models;

namespace WebApplication1.Interfaces
{
    public interface IClientManager
    {
        Task CreateAsync(NameValueCollection formData);
        Client GetAsync(long id, bool adPush);
        Task UpdateAsync(long id, string name, bool adPush);
        Task DeleteAsync(long id);
    }
}