using System;
using System.Web.Http;
using WebApplication1.BLL;
using WebApplication1.Misc;
using WebApplication1.Misc.Auth;

namespace WebApplication1.Controllers
{
    public class FavouritesController : BaseApiController<Misc.Auth.ConsultantManager>
    {
        private readonly FavoritesManager mng = new FavoritesManager();

        /// <summary>
        /// Добавить консультанта в избранное (действие клиента)
        /// </summary>
        [Route("api/favorites/{consultantId}")]
        [HttpPost]
        [UserApiAuthorize]
        public Object Post(long consultantId)
        {
            mng.CreateAsync(UserId.Value, consultantId);
            return Ok(true);
        }

        /// <summary>
        /// Получить избранных консультантов клиента
        /// </summary>
        [HttpGet]
        [Route("api/favorites/client/{id}")]
        public Object Get(long id)
        {
            return Ok(mng.GetVMs(id));
        }

        /// <summary>
        /// Удалить из избранного (действие клиента)
        /// </summary>
        [Route("api/favorites/{consultantId}")]
        [HttpDelete]
        [UserApiAuthorize]
        public Object Delete(long consultantId)
        {
            mng.DeleteAsync(UserId.Value, consultantId);
            return Ok(true);
        }
    }
}