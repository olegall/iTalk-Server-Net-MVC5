using System;
using System.Collections.Generic;
using System.Linq;
using WebApplication1.Models;
using WebApplication1.Utils;
using System.Threading.Tasks;
using WebApplication1.DAL;

namespace WebApplication1.BLL
{
    public class SubcategoryManager
    {
        #region Fields
        private readonly IList<CategoryVM> categoryVMs = new List<CategoryVM>();
        private readonly GenericRepository<Subcategory> rep = Reps.Subcategories;
        #endregion

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
                throw new Exception(ServiceUtil.GetExMsg(e, "Не получилось скрыть подкатегорию"));
            }
        }
        #endregion
    }
}