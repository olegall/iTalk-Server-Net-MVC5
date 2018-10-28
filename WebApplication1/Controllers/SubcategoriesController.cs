using System;
using System.Web.Http;
using WebApplication1.BLL;

namespace WebApplication1.Controllers
{
    public class SubcategoriesController : ApiController
    {
        private readonly SubcategoryBLL BLL = new SubcategoryBLL();

        /// <summary>
        /// Скрыть подкатегорию
        /// </summary>
        [HttpDelete]
        [Route("api/subcategories/{id}")]
        public Object Delete(long id)
        {
            BLL.HideAsync(id);
            return Ok(true);
        }
    }
}