using System;
using WebApplication1.BLL;
using WebApplication1.Utils;
using WebApplication1.Misc;
using WebApplication1.Misc.Auth;
using System.Web.Http;

namespace WebApplication1.Controllers
{                // !!! в ед. числе ConsultantController - и далее
    public class ClientsController : BaseApiController<Misc.Auth.ConsultantManager>
    {
        private readonly BLL.ClientManager mng = new BLL.ClientManager();

        /// <summary>
        /// Зарегистрировать клиента
        /// </summary>
        [HttpPost]
        public Object Post()
        {
            mng.CreateAsync(ServiceUtil.Request.Form);
            return Ok(true);
        }

        /// <summary>
        /// Получить инфо о клиенте для карточки
        /// </summary>
        //[UserApiAuthorize]
        //[Route("api/clients/{adPush}")]
        [Route("api/clients/{id}/{adPush}")]
        public Object Get(long id, bool adPush)
        {
            // !!! эксепшн если польз-ль с данным Id не существует (ошибка 500)
            return Ok(mng.GetAsync(/*UserId.Value*/id, adPush));
        }

        //[UserApiAuthorize]
        /// <summary>
        /// Редактировать инфо клиента
        /// </summary>
        //[Route("api/clients/{name}/{adPush}")]
        [HttpPut]
        public Object Update()
        {
            int id = Convert.ToInt32(ServiceUtil.Request.Form["id"]);
            string name = ServiceUtil.Request.Form["name"];
            bool adPush = Convert.ToBoolean(ServiceUtil.Request.Form["adPush"]);
            mng.UpdateAsync(/*UserId.Value*/id, name, adPush);
            return Ok(true);
        }

        /// <summary>
        /// Удалить клиента
        /// </summary>
        [HttpDelete]
        public Object Delete()
        {
            mng.DeleteAsync(/*UserId.Value*/Convert.ToInt32(ServiceUtil.Request.Form["id"]));
            return Ok(true);
        }
    }
}