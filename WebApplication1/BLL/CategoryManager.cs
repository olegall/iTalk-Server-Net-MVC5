using System;
using System.Web;
using System.Linq;
using System.Data.Entity;
using System.Threading.Tasks;
using System.Collections.Generic;
using System.Collections.Specialized;
using WebApplication1.Models;
using WebApplication1.Utils;
using WebApplication1.Interfaces;

namespace WebApplication1.BLL
{
    public class CategoryManager : BaseManager, ICategoryManager
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

        public async Task<CRUDResult<Category>> CreateAsync(NameValueCollection formData)
        {
            CRUDResult<Category> CRUDResult = new CRUDResult<Category>();
            try
            {
                await rep.CreateAsync(new Category(formData["title"], formData["description"]));
            }
            catch
            {
                CRUDResult.Mistake = (int)CRUDResult<Category>.Mistakes.ServerOrConnectionFailed;
            }
            return CRUDResult;
        }

        // !!! что за Id?
        public async Task<CRUDResult<CategoryImage>> CreateImageAsync(HttpPostedFile file, long id)
        {
            CRUDResult<CategoryImage> CRUDResult = new CRUDResult<CategoryImage>();
            try
            {
                await categoryImagesRep.CreateAsync(new CategoryImage(id, 
                                                                      ServiceUtil.GetBytesFromStream(file.InputStream), 
                                                                      file.FileName,
                                                                      file.ContentLength,
                                                                      DateTime.Now));
            }
            catch
            {
                CRUDResult.Mistake = (int)CRUDResult<CategoryImage>.Mistakes.ServerOrConnectionFailed;
            }
            return CRUDResult;
        }

        public IEnumerable<Category> GetAll(int offset, int limit) // ! как обработать?
        {
            return rep.Get().Skip(offset).Take(limit);
        }

        public async Task<CRUDResult<Category>> HideAsync(long id)
        {
            CRUDResult<Category> result = TryGetEntity<Category>(rep, id);
            if (result.Entity == null)
            {
                result.Mistake = (int)CRUDResult<Client>.Mistakes.EntityNotFound;
                return result;
            }
            result.Entity.Deleted = true;
            try
            {
                await rep.UpdateAsync(result.Entity);
            }
            catch
            {
                result.Mistake = (int)CRUDResult<Client>.Mistakes.ServerOrConnectionFailed;
            }
            return result;
        }
        #endregion
    }
}