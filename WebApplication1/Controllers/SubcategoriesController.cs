using System;
using System.Web.Http;
using WebApplication1.BLL;
using WebApplication1.DAL;
using WebApplication1.Interfaces;

namespace WebApplication1.Controllers
{
    public class SubcategoriesController : ApiController
    {
        private readonly ISubcategoryManager mng = new SubcategoryManager(Reps.Subcategories);

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