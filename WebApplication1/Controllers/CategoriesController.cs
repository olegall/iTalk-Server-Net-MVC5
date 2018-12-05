using System;
using System.Collections.Generic;
using System.Web.Http;
using WebApplication1.Models;
using WebApplication1.BLL;
using WebApplication1.Utils;
using WebApplication1.Misc;
using WebApplication1.ViewModels;
using WebApplication1.Interfaces;
using WebApplication1.DAL;
using System.Threading.Tasks;

namespace WebApplication1.Controllers
{
    public class CategoriesController : BaseApiController<Misc.Auth.ConsultantManager>
    {
        private readonly ICategoryManager mng;
        private readonly ISubcategoryManager subcategoryMng;

        CategoriesController(ICategoryManager mng, ISubcategoryManager subcategoryMng)
        {
            this.mng = new CategoryManager(Reps.Categories, Reps.CategoryImages);
            this.subcategoryMng = subcategoryMng;
        }

        /// <summary>
        /// Получить категории с подкатегориями
        /// </summary>
        [HttpGet]
        [Route("api/categories/{offset}/{limit}")]
        public Object Get(int offset, int limit)
        {
            IList<CategoryVM> VMs = new List<CategoryVM>();
            foreach (Category category in mng.GetAll(offset, limit))
            {
                VMs.Add(new CategoryVM
                {
                    Id = category.Id,
                    Title = category.Title,
                    Image = BaseUrl.Get($"categoryimage/{category.Id}"),
                    Subcategories = subcategoryMng.GetVMs(category.Id)
                });
            }
            return Ok(VMs);
        }

        /// <summary>
        /// Добавить категорию
        /// </summary>
        [HttpPost]
        public async Task<IHttpActionResult> Post()
        {
            CRUDResult<Category> result = await mng.CreateAsync(ServiceUtil.Request.Form);
            return SendResult(result);
        }

        /// <summary>
        /// Добавить изображение
        /// </summary>
        [HttpPost]
        [Route("api/categories/{id}")]
        public async Task<IHttpActionResult> CreateImage(long id)
        {
            CRUDResult<CategoryImage> result = await mng.CreateImageAsync(ServiceUtil.Request.Files["image"], id);
            return SendResult(result);
        }

        /// <summary>
        /// Скрыть категорию
        /// </summary>
        [HttpDelete]
        [Route("api/categories/{id}")]
        public async Task<IHttpActionResult> Delete(long id) // !!! Object - типизировать
        {
            CRUDResult<Category> result = await mng.HideAsync(id);
            return SendResult(result);
        }
    }
}