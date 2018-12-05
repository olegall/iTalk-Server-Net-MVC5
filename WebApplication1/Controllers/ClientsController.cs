using System;
using System.Web.Http;
using System.Threading.Tasks;
using System.Collections.Specialized;
using WebApplication1.Utils;
using WebApplication1.Misc;
using WebApplication1.Interfaces;
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

        public ClientsController() // ! убрать. пока нужен для тестов
        {
        }

        /// <summary>
        /// Зарегистрировать клиента
        /// </summary>
        [HttpPost]
        public async Task<IHttpActionResult> Post()
        {
            CRUDResult<Client> result = await mng.CreateAsync(form["name"], form["phone"]);
            return SendResult<Client>(result);
        }

        /// <summary>
        /// Получить инфо о клиенте для карточки
        /// </summary>
        //[UserApiAuthorize]
        [HttpGet]
        [Route("api/clients/{id}/{adPush}")]
        public IHttpActionResult Get(long id, bool adPush)
        {
            CRUDResult<Client> result = mng.GetAsync(/*UserId.Value*/id, adPush);
            if (result.Entity != null)
                return Ok(result.Entity);

            return BadRequest(result.Mistake.ToString());
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

            CRUDResult<Client> result = await mng.UpdateAsync(/*UserId.Value*/id, name, adPush);
            return SendResult<Client>(result);
        }

        /// <summary>
        /// Удалить клиента
        /// </summary>
        [HttpDelete]
        public async Task<IHttpActionResult> Delete()
        {
            CRUDResult<Client> result = await mng.DeleteAsync(/*UserId.Value*/Convert.ToInt32(ServiceUtil.Request.Form["id"]));
            return SendResult<Client>(result);
        }
    }
}