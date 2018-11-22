using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Data.Entity;
using System.Linq;
using System.Web;
using WebApplication1.Models;
using WebApplication1.Utils;
using System.Threading.Tasks;
using WebApplication1.Interfaces;

namespace WebApplication1.BLL
{
    public class CategoryManager : ICategoryManager
    {
        private readonly IGenericRepository<Category> rep;
        private readonly IGenericRepository<CategoryImage> categoryImagesRep;

        public CategoryManager(IGenericRepository<Category> rep,
                               IGenericRepository<CategoryImage> categoryImagesRep)
        {
            this.rep = rep;
            this.categoryImagesRep = categoryImagesRep;
        }

        // !!! убрать папку Packages
        #region Public methods
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
                await categoryImagesRep.CreateAsync(new CategoryImage(id, 
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
        #endregion
    }
}