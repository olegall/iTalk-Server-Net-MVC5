using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebApplication1.Models;
using WebApplication1.ViewModels;
using WebApplication1.Interfaces;
using WebApplication1.Utils;

namespace WebApplication1.BLL
{
    public class SubcategoryManager : BaseManager, ISubcategoryManager
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
        public async Task<CRUDResult<Subcategory>> HideAsync(long id)
        {
            CRUDResult<Subcategory> result = TryGetEntity<Subcategory>(rep, id);
            if (result.Entity == null)
            {
                result.Mistake = (int)CRUDResult<Subcategory>.Mistakes.EntityNotFound;
                return result;
            }

            result.Entity.Deleted = true;
            try
            {
                await rep.UpdateAsync(result.Entity);
            }
            catch
            {
                result.Mistake = (int)CRUDResult<Subcategory>.Mistakes.ServerOrConnectionFailed;
            }
            return result;
        }
        #endregion
    }
}