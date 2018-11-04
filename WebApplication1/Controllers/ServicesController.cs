using System;
using System.Net;
using System.Web.Http;
using WebApplication1.BLL;
using WebApplication1.Utils;

namespace WebApplication1.Controllers
{
    public class ServicesController : ApiController
    {
        private readonly ServiceBLL BLL = new ServiceBLL();

        /// <summary>
        /// Получить список всех услуг для клиента, реестра услуг, оформления заказа (стр 13, 43)
        /// </summary>
        [HttpGet]
        public Object Get()
        {
            return Ok(BLL.GetVMs());
        }

        /// <summary>
        /// Добавить услугу (стр 28)
        /// </summary>
        [HttpPost]
        public Object Post()
        {
            BLL.CreateAsync(ServiceUtil.Request.Form);
            return Ok(true);
        }

        /// <summary>
        /// Добавить сразу много услуг
        /// </summary>
        [HttpPost]
        [Route("api/services/many")]
        public Object CreateMany()
        {
            BLL.CreateManyAsync(ServiceUtil.Request.Form);
            return Ok(true);
        }
        /// <summary>
        /// Редактировать услугу
        /// </summary>
        [HttpPut]
        public Object Update()
        {
            BLL.UpdateAsync(ServiceUtil.Request.Form);
            return Ok(true);
        }

        /// <summary>
        /// Скрыть услугу
        /// </summary>
        [HttpDelete]
        public Object Delete(long id)
        {
            BLL.HideAsync(id);
            return Ok(true);
        }

        /// <summary>
        /// Создать изображение услуги
        /// </summary>
        [HttpPost]
        [Route("api/services/CreateImage")]
        public Object CreateImage()
        {
            BLL.CreateImageAsync(ServiceUtil.Request);
            return Ok(true);
        }

        /// <summary>
        /// Оплатить услугу
        /// </summary>
        [HttpPost]
        [Route("api/services/pay/{orderId}")]
        public Object Pay(long orderId)
        {
            return new System.Web.Mvc.HttpStatusCodeResult(HttpStatusCode.OK);
        }
    }
}