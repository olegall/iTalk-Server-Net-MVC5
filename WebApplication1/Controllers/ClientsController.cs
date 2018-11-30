using System;
using WebApplication1.Utils;
using WebApplication1.Misc;
using System.Web.Http;
using WebApplication1.Interfaces;
using System.Threading.Tasks;
using System.Collections.Specialized;
using WebApplication1.Models;

namespace WebApplication1.Controllers
{                // !!! в ед. числе ConsultantController - и далее
    public class ClientsController : BaseApiController<Misc.Auth.ConsultantManager>
    {
        //private readonly IClientManager mng = new BLL.ClientManager(Reps.Clients);
        private readonly DataContext _db = new DataContext();

        private readonly NameValueCollection form = ServiceUtil.Form;

        private readonly IClientManager mng;
        public ClientsController(IClientManager mng)
        {
            this.mng = mng;
        }

        //public ClientsController()
        //{
        //}

        /// <summary>
        /// Зарегистрировать клиента
        /// </summary>
        [HttpPost]
        public async Task<IHttpActionResult> Post()
        {
            CRUD.Result result =  await mng.CreateAsync(form["name"], form["phone"]);
            return SendResult(result);
        }

        /// <summary>
        /// Получить инфо о клиенте для карточки
        /// </summary>
        //[UserApiAuthorize]
        [Route("api/clients/{id}/{adPush}")]
        public object Get(long id, bool adPush)
        {
            object result = mng.GetAsync(/*UserId.Value*/id, adPush);
            if (result.GetType() == typeof(Client))
                return Ok(result);

            return BadRequest(result.ToString());
            //return mng.GetAsync(/*UserId.Value*/id, adPush);
        }

        //[UserApiAuthorize]
        /// <summary>
        /// Редактировать инфо клиента
        /// </summary>
        //[Route("api/clients/{name}/{adPush}")]
        [HttpPut]
        public async Task<IHttpActionResult> Update()
        {
            int id = Convert.ToInt32(form["id"]);
            string name = form["name"];
            bool adPush = Convert.ToBoolean(form["adPush"]);

            CRUD.Result result = await mng.UpdateAsync(/*UserId.Value*/id, name, adPush);
            return SendResult(result);
        }

        /// <summary>
        /// Удалить клиента
        /// </summary>
        [HttpDelete]
        public async Task<IHttpActionResult> Delete()
        {
            CRUD.Result result = await mng.DeleteAsync(/*UserId.Value*/Convert.ToInt32(ServiceUtil.Request.Form["id"]));
            return SendResult(result);
        }
    }
}