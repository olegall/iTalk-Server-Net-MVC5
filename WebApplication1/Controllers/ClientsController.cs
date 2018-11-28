using System;
using WebApplication1.Utils;
using WebApplication1.Misc;
using WebApplication1.Misc.Auth;
using System.Web.Http;
using WebApplication1.Interfaces;
using System.Linq;
using WebApplication1.DAL;

namespace WebApplication1.Controllers
{                // !!! в ед. числе ConsultantController - и далее
    public class ClientsController : BaseApiController<Misc.Auth.ConsultantManager>
    {
        private readonly IClientManager mng = new BLL.ClientManager(Reps.Clients);
        private readonly DataContext _db = new DataContext();

        //private readonly IClientManager mng;
        //public ClientsController(IClientManager mng)
        //{
        //    this.mng = mng;
        //}

        public ClientsController()
        {
        }


        /// <summary>
        /// Зарегистрировать клиента
        /// </summary>
        [HttpPost]
        public Object Post()
        {
            //var form = ServiceUtil.Request.Form;
            mng.CreateAsync("name", "phone");
            //mng.CreateAsync(form["name"], form["phone"]);
            return Ok(true);
        }

        /// <summary>
        /// Получить инфо о клиенте для карточки
        /// </summary>
        //[UserApiAuthorize]
        //[Route("api/clients/{adPush}")]
        [Route("api/clients/{id}/{adPush}")]
        public object Get(long id, bool adPush)
        {
            // !!! эксепшн если польз-ль с данным Id не существует (ошибка 500)
            var a2 = mng.GetAllTest();
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