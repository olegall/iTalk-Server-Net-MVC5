using System;
using System.Collections.Generic;
using System.Web.Http;
using WebApplication1.Models;
using WebApplication1.BLL;
using WebApplication1.Utils;
using WebApplication1.Misc;

namespace WebApplication1.Controllers
{
    public class CategoriesController : ApiController
    {
        private readonly CategoryBLL BLL = new CategoryBLL();
        private readonly SubcategoryBLL subcategoryBLL = new SubcategoryBLL();

        /// <summary>
        /// Получить категории с подкатегориями
        /// </summary>
        [HttpGet]
        [Route("api/categories/{offset}/{limit}")]
        public Object Get(int offset, int limit)
        {
            IList<CategoryVM> VMs = new List<CategoryVM>();
            foreach (Category category in BLL.GetAll(offset, limit))
            {
                IList<SubcategoryVM> subcategoryVMs = new List<SubcategoryVM>();
                VMs.Add(new CategoryVM
                {
                    Id = category.Id,
                    Title = category.Title,
                    Image = BaseUrl.Get($"categoryimage/{category.Id}"),
                    Subcategories = subcategoryBLL.GetVMs(category.Id)
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
            BLL.Create(ServiceUtil.Request.Form); 
            return Ok(true);
        }

        /// <summary>
        /// Добавить изображение
        /// </summary>
        [HttpPost]
        [Route("api/categories/{id}")]
        public Object CreateImage(long id)
        {
            BLL.CreateImage(ServiceUtil.Request.Files["image"], id);
            return Ok(true);
        }

        /// <summary>
        /// Скрыть категорию
        /// </summary>
        [HttpDelete]
        [Route("api/categories/{id}")]
        public Object Delete(long id)
        {
            BLL.Hide(id);
            return Ok(true);
        }
    }
}