using System;
using WebApplication1.BLL;
using WebApplication1.Utils;
using WebApplication1.Misc;
using WebApplication1.Misc.Auth;
using System.Web.Http;

namespace WebApplication1.Controllers
{
    public class ClientsController : BaseApiController<Misc.Auth.ConsultantManager>
    {
        private readonly ClientBLL BLL = new ClientBLL();
        
        /// <summary>
        /// Зарегистрировать клиента
        /// </summary>
        public Object Post()
        {
            BLL.Create(ServiceUtil.Request.Form);
            return Ok(true);
        }

        /// <summary>
        /// Получить инфо о клиенте для карточки
        /// </summary>
        [UserApiAuthorize]
        [Route("api/clients/{adPush}")]
        public Object Get(bool adPush)
        {
            return Ok(BLL.Get(UserId.Value, adPush));
        }

        [UserApiAuthorize]
        /// <summary>
        /// Редактировать инфо клиента
        /// </summary>
        [Route("api/clients/{name}/{adPush}")]
        [HttpPut]
        public Object Update(string name, bool adPush)
        {
            BLL.Update(UserId.Value, name, adPush);
            return Ok(true);
        }

        /// <summary>
        /// Удалить клиента
        /// </summary>
        [HttpDelete]
        public Object Delete()
        {
            BLL.Delete(UserId.Value);
            return Ok(true);
        }
    }
}