using System.Threading.Tasks;
using System.Web;
using System.Collections.Specialized; // ! перетасовать по типам коллекций
using System.Collections.Generic;
using WebApplication1.Models;
using WebApplication1.Utils;

namespace WebApplication1.Interfaces
{
    public interface ICategoryManager
    {
        Task<CRUDResult<Category>> CreateAsync(NameValueCollection formData);
        Task<CRUDResult<CategoryImage>> CreateImageAsync(HttpPostedFile file, long id);
        IEnumerable<Category> GetAll(int offset, int limit);
        Task<CRUDResult<Category>> HideAsync(long id);
    }
}