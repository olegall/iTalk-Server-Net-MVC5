using System.Threading.Tasks;
using WebApplication1.Models;
using System.Collections.Generic;
using WebApplication1.ViewModels;

namespace WebApplication1.Interfaces
{
    public interface ISubcategoryManager
    {
        IEnumerable<Subcategory> GetByCategoryId(int categoryId);
        IEnumerable<SubcategoryVM> GetVMs(int categoryId);
        Task HideAsync(long id);
    }
}