using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Data.Entity;
using System.Linq;
using System.Web;
using WebApplication1.Models;
using WebApplication1.Utils;
using WebApplication1.DAL;
using System.Threading.Tasks;

namespace WebApplication1.BLL
{
    public class CategoryBLL
    {
        private readonly static Repositories reps = new Repositories();
        private readonly GenericRepository<Category> rep = reps.Categories;
        private readonly GenericRepository<CategoryImage> imageRep = reps.CategoryImages;
        // !!! убрать папку Packages
        public async Task CreateAsync(NameValueCollection formData)
        {
            try
            {
                 await rep.CreateAsync(new Category(formData["title"], formData["description"]));
            }
            catch (Exception e)
            {
                throw new Exception(ServiceUtil.GetExMsg(e, "Не удалось добавить категорию"));
            }
        }
                                                                    // !!! что за Id?
        public async Task CreateImageAsync(HttpPostedFile file, long id)
        {
            try
            {
                await imageRep.CreateAsync(new CategoryImage(id, 
                                                       ServiceUtil.GetBytesFromStream(file.InputStream), 
                                                       file.FileName,
                                                       file.ContentLength,
                                                       DateTime.Now));
            }
            catch (Exception e)
            {
                throw new Exception(ServiceUtil.GetExMsg(e, "Не удалось добавить изображение в категорию"));
            }
        }

        public IEnumerable<Category> GetAll(int offset, int limit)
        {
            return rep.Get().Skip(offset).Take(limit);
        }

        public async Task HideAsync(long id)
        {
            Category category = rep.GetAsync(id);
            category.Deleted = true;
            try
            {
                await rep.UpdateAsync(category);
            }
            catch (Exception e)
            {
                throw new Exception(ServiceUtil.GetExMsg(e, "Не получилось скрыть категорию"));
            }
        }
    }
}