using System.Threading.Tasks;
using WebApplication1.Models;
using WebApplication1.Utils;
using System.Collections.Specialized;

namespace WebApplication1.Interfaces
{
    public interface IOrderManager
    {
        Task<CRUDResult<Order>> CreateAsync(NameValueCollection formData);
        Task<CRUDResult<Order>> ConfirmAsync(long id);
        Task<CRUDResult<Order>> CancelByClientAsync(long id);
        Task<CRUDResult<Order>> CancelByConsAsync(long id);
        Task<CRUDResult<Order>> UpdateTimeAsync(long id, long timestamp);
        //CRUDResult<Client> GetAsync(long id, bool adPush);
        //Task<CRUDResult<Client>> UpdateAsync(long id, string name, bool adPush);
        //Task<CRUDResult<Client>> DeleteAsync(long id);
    }
}