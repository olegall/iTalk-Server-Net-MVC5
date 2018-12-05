using System;
using System.Web.Http;
using System.Threading.Tasks;
using WebApplication1.BLL;
using WebApplication1.Interfaces;
using WebApplication1.Misc;
using WebApplication1.Misc.Auth;
using WebApplication1.DAL;
using WebApplication1.Utils;
using WebApplication1.Models;

namespace WebApplication1.Controllers
{
    public class FavoritesController : BaseApiController<Misc.Auth.ConsultantManager>
    {
        private readonly IFavoritesManager mng;

        FavoritesController()
        {
            mng = new FavoritesManager(Reps.Favorites, Reps.Privates, Reps.Juridics);
        }
        /// <summary>
        /// Добавить консультанта в избранное (действие клиента)
        /// </summary>
        [Route("api/favorites/{consultantId}")]
        [HttpPost]
        [UserApiAuthorize]
        public async Task<IHttpActionResult> Post(long consultantId)
        {
            CRUDResult<Favorite> result = await mng.CreateAsync(UserId.Value, consultantId);
            return SendResult<Favorite>(result);
        }

        /// <summary>
        /// Получить избранных консультантов клиента
        /// </summary>
        [HttpGet]
        [Route("api/favorites/client/{id}")]
        public object Get(long id)
        {
            return Ok(mng.GetVMs(id));
        }

        /// <summary>
        /// Удалить из избранного (действие клиента)
        /// </summary>
        [Route("api/favorites/{consultantId}")]
        [HttpDelete]
        [UserApiAuthorize]
        public async Task<IHttpActionResult> Delete(long consultantId)
        {
            CRUDResult<Favorite> result = await mng.DeleteAsync(UserId.Value, consultantId);
            return SendResult<Favorite>(result);
        }
    }
}