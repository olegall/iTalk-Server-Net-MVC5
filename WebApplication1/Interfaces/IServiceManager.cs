using System.Threading.Tasks;
using System.Web;
using System.Collections.Generic;
using System.Collections.Specialized;
using WebApplication1.Models;
using WebApplication1.Utils;

namespace WebApplication1.Interfaces
{
    public interface IServiceManager
    {
        Task<CRUDResult<Service>> CreateAsync(NameValueCollection formData);
        Task<CRUDResult<Service>> UpdateAsync(NameValueCollection formData);
        Task<CRUDResult<Service>> HideAsync(long id);
    }
}