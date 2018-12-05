using System.Threading.Tasks;
using System.Collections.Generic;
using WebApplication1.Models;
using WebApplication1.Utils;
using WebApplication1.ViewModels;

namespace WebApplication1.Interfaces
{
    public interface ISubcategoryManager
    {
        Task<CRUDResult<Subcategory>> HideAsync(long id);
        IEnumerable<SubcategoryVM> GetVMs(int categoryId);
    }
}