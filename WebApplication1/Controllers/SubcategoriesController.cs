using System;
using System.Web.Http;
using WebApplication1.BLL;

namespace WebApplication1.Controllers
{
    public class SubcategoriesController : ApiController
    {
        private readonly SubcategoryManager mng = new SubcategoryManager();

        /// <summary>
        /// Скрыть подкатегорию
        /// </summary>
        [HttpDelete]
        [Route("api/subcategories/{id}")]
        public Object Delete(long id)
        {
            mng.HideAsync(id);
            return Ok(true);
        }
    }
}