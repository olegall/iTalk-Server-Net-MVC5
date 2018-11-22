using System.Collections.Specialized;
using System.Threading.Tasks;
using WebApplication1.Models;
using System.Web;
using System.Collections.Generic;

namespace WebApplication1.Interfaces
{
    public interface ICategoryManager
    {
        Task CreateAsync(NameValueCollection formData);
        Task CreateImageAsync(HttpPostedFile file, long id);
        IEnumerable<Category> GetAll(int offset, int limit);
        Task HideAsync(long id);
    }
}