using System;
using System.Web.Http;
using WebApplication1.BLL;
using WebApplication1.Misc;
using WebApplication1.Misc.Auth;

namespace WebApplication1.Controllers
{
    public class FavouritesController : BaseApiController<Misc.Auth.ConsultantManager>
    {
        private readonly FavoritesBLL BLL = new FavoritesBLL();

        /// <summary>
        /// Добавить консультанта в избранное (действие клиента)
        /// </summary>
        [Route("api/favorites/{consultantId}")]
        [HttpPost]
        [UserApiAuthorize]
        public Object Post(long consultantId)
        {
            BLL.Create(UserId.Value, consultantId);
            return Ok(true);
        }

        /// <summary>
        /// Получить избранных консультантов клиента
        /// </summary>
        [HttpGet]
        [Route("api/favorites/client/{id}")]
        public Object Get(long id)
        {
            return Ok(BLL.GetVMs(id));
        }

        /// <summary>
        /// Удалить из избранного (действие клиента)
        /// </summary>
        [Route("api/favorites/{consultantId}")]
        [HttpDelete]
        [UserApiAuthorize]
        public Object Delete(long consultantId)
        {
            BLL.Delete(UserId.Value, consultantId);
            return Ok(true);
        }
    }
}