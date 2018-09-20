using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using WebApplication1.Models;
using WebApplication1.BLL;
using WebApplication1.Utils;

namespace WebApplication1.Controllers
{
    public class NotificationsController : ApiController
    {
        private readonly NotificationBLL BLL = new NotificationBLL();

        /// <summary>
        /// Получить для консультанта
        /// </summary>
        [HttpGet]
        [Route("api/notifications/consultant/{id}")]
        public Object Get(long id)
        {
            return Ok(BLL.GetVMs(id));
        }
    }
}