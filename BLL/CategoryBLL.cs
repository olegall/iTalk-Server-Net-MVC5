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

        public void Create(NameValueCollection formData)
        {
            Category category = new Category();
            category.Title = formData["title"];
            category.Description = formData["description"];
            try
            {
                rep.Create(category);
            }
            catch (Exception e)
            {
                throw new Exception(ServiceUtil.GetExMsg(e, "Не удалось добавить категорию"));
            }
        }

        public void CreateImage(HttpPostedFile file, long id)
        {
            CategoryImage image = new CategoryImage();
            image.CategoryId = id;
            image.Bytes = ServiceUtil.GetBytesFromStream(file.InputStream);
            image.FileName = file.FileName;
            image.Size = file.ContentLength;
            image.Date = DateTime.Now;
            try
            {
                imageRep.Create(image);
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
                rep.Update(category);
            }
            catch (Exception e)
            {
                throw new Exception(ServiceUtil.GetExMsg(e, "Не получилось скрыть категорию"));
            }
        }
    }
}