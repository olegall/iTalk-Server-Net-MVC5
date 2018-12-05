using System;
using System.Web.Http;
using WebApplication1.Interfaces;

namespace WebApplication1.Controllers
{
    public class NotificationsController : ApiController
    {
        private readonly INotificationManager mng;
        public NotificationsController(INotificationManager mng)
        {
            this.mng = mng;
        }

        /// <summary>
        /// Получить для консультанта
        /// </summary>
        [HttpGet]
        [Route("api/notifications/consultant/{id}")]
        public Object Get(long id) // ! обработать
        {
            return Ok(mng.GetVMs(id));
        }
    }
}