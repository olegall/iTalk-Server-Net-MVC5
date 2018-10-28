using System;
using System.Collections.Generic;
using System.Linq;
using WebApplication1.Models;
using WebApplication1.Utils;

namespace WebApplication1.BLL
{
    public class SubcategoryBLL
    {
        private readonly DataContext _db = new DataContext();
        private readonly IList<CategoryVM> categoryVMs = new List<CategoryVM>();
        private readonly GenericRepository<Subcategory> rep;
        public SubcategoryBLL()
        {
            rep = new GenericRepository<Subcategory>(_db);
        }

        // !!! улучшить
        public IEnumerable<Subcategory> GetByCategoryId(int categoryId)
        {
            return rep.Get().Where(x => x.CategoryId == categoryId)
                            .ToArray();
        }

        // !!! join
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
        // !!! HideAsync
        public async void Hide(long id)
        {
            //Subcategory subcat = rep.Get().SingleOrDefault(x => x.Id == id);
            Subcategory subcat = await rep.GetAsync(id);
            subcat.Deleted = true;
            try
            {
                rep.UpdateAsync(subcat);
            }
            catch (Exception e)
            {
                throw new Exception(ServiceUtil.GetExMsg(e, "Не получилось скрыть подкатегорию"));
            }
        }
    }
}