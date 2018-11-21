using System;
using System.Collections.Generic;
using System.Web.Http;
using WebApplication1.Models;
using WebApplication1.BLL;
using WebApplication1.Utils;
using WebApplication1.Misc;
using WebApplication1.ViewModels;

namespace WebApplication1.Controllers
{
    public class CategoriesController : ApiController
    {
        private readonly CategoryManager mng = new CategoryManager();
        private readonly SubcategoryManager subcategoryMng = new SubcategoryManager();

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
        public Object Post()
        {
            mng.CreateAsync(ServiceUtil.Request.Form); 
            return Ok(true);
        }

        /// <summary>
        /// Добавить изображение
        /// </summary>
        [HttpPost]
        [Route("api/categories/{id}")]
        public Object CreateImage(long id)
        {
            mng.CreateImageAsync(ServiceUtil.Request.Files["image"], id);
            return Ok(true);
        }

        /// <summary>
        /// Скрыть категорию
        /// </summary>
        [HttpDelete]
        [Route("api/categories/{id}")]
        public Object Delete(long id) // !!! Object - типизировать
        {
            mng.HideAsync(id);
            return Ok(true);
        }
    }
}