using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Data.Entity;
using System.Linq;
using System.Web;
using WebApplication1.Models;
using WebApplication1.Utils;
using WebApplication1.DAL;

namespace WebApplication1.BLL
{
    public class CategoryBLL
    {
        private readonly static Repositories reps = new Repositories();
        private readonly GenericRepository<Category> rep = reps.Categories;
        private readonly GenericRepository<CategoryImage> imageRep = reps.CategoryImages;
        // !!! убрать папку Packages
        public void Create(NameValueCollection formData)
        {
            try
            {
                 rep.CreateAsync(new Category(formData["title"], formData["description"]));
            }
            catch (Exception e)
            {
                throw new Exception(ServiceUtil.GetExMsg(e, "Не удалось добавить категорию"));
            }
        }

        public void CreateImage(HttpPostedFile file, long id)
        {
            try
            {
                imageRep.CreateAsync(new CategoryImage(id, 
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

        public void Hide(long id)
        {
            Category category = rep.Get().SingleOrDefault(x => x.Id == id);
            category.Deleted = true;
            try
            {
                rep.UpdateAsync(category);
            }
            catch (Exception e)
            {
                throw new Exception(ServiceUtil.GetExMsg(e, "Не получилось скрыть категорию"));
            }
        }
    }
}