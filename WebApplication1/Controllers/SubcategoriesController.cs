using System;
using System.Web.Http;
using WebApplication1.Interfaces;
using System.Threading.Tasks;
using WebApplication1.Utils;
using WebApplication1.Misc;
using WebApplication1.Models;

namespace WebApplication1.Controllers
{
    public class SubcategoriesController : BaseApiController<Misc.Auth.ConsultantManager>
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
        public async Task<IHttpActionResult> Delete(long id)
        {
            CRUDResult<Subcategory> result = await mng.HideAsync(id);
            return SendResult<Subcategory>(result);
        }
    }
}