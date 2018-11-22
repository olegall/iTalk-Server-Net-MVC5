using System;
using System.Web.Http;
using WebApplication1.BLL;
using WebApplication1.DAL;
namespace WebApplication1.Controllers
{
    public class NotificationsController : ApiController
    {
        private readonly NotificationManager mng = new NotificationManager(Reps.Orders);

        /// <summary>
        /// Получить для консультанта
        /// </summary>
        [HttpGet]
        [Route("api/notifications/consultant/{id}")]
        public Object Get(long id)
        {
            return Ok(mng.GetVMs(id));
        }
    }
}