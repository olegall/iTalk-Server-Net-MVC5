using System;
using System.Web.Http;
using WebApplication1.Interfaces;

namespace WebApplication1.Controllers
{
    public class SubcategoriesController : ApiController
    {
        private readonly ISubcategoryManager mng;
        public SubcategoriesController(ISubcategoryManager mng)
        {
            this.mng = mng;
        }

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