using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Threading.Tasks;
using WebApplication1.Models;
using WebApplication1.ViewModels;
using WebApplication1.Interfaces;

namespace WebApplication1.BLL
{
    public class SubcategoryManager : ISubcategoryManager 
    {
        private readonly IGenericRepository<Subcategory> rep;

        public SubcategoryManager(IGenericRepository<Subcategory> rep)
        {
            this.rep = rep;
        }

        #region Public methods
        // !!! улучшить
        public IEnumerable<Subcategory> GetByCategoryId(int categoryId)
        {
            return rep.Get().Where(x => x.CategoryId == categoryId)
                            .ToArray();
        }

        public IEnumerable<SubcategoryVM> GetVMs(int categoryId)
        {
            IList<SubcategoryVM> vm = new List<SubcategoryVM>();
            IEnumerable<Subcategory> subcats = GetByCategoryId(categoryId);
            foreach (Subcategory subcat in subcats)
            {
                vm.Add(new SubcategoryVM
                {
                    Id = subcat.Id,
                    Title = subcat.Title
                });
            }
            return vm;
        }

        public async Task HideAsync(long id)
        {
            Subcategory subcat = rep.GetAsync(id);
            subcat.Deleted = true;
            try
            {
                await rep.UpdateAsync(subcat);
            }
            catch (Exception e)
            {
                throw new HttpException(500, "Не получилось скрыть подкатегорию");
            }
        }
        #endregion
    }
}