using System;
using System.Net;
using System.Web.Http;
using WebApplication1.BLL;
using WebApplication1.Utils;

namespace WebApplication1.Controllers
{
    public class ServicesController : ApiController
    {
        private readonly ServiceManager mng = new ServiceManager();

        /// <summary>
        /// Получить список всех услуг для клиента, реестра услуг, оформления заказа (стр 13, 43)
        /// </summary>
        [HttpGet]
        public Object Get()
        {
            return Ok(mng.GetVMs());
        }

        /// <summary>
        /// Добавить услугу (стр 28)
        /// </summary>
        [HttpPost]
        public Object Post()
        {
            mng.CreateAsync(ServiceUtil.Request.Form);
            return Ok(true);
        }

        /// <summary>
        /// Добавить сразу много услуг
        /// </summary>
        [HttpPost]
        [Route("api/services/many")]
        public Object CreateMany()
        {
            mng.CreateManyAsync(ServiceUtil.Request.Form);
            return Ok(true);
        }
        /// <summary>
        /// Редактировать услугу
        /// </summary>
        [HttpPut]
        public Object Update()
        {
            mng.UpdateAsync(ServiceUtil.Request.Form);
            return Ok(true);
        }

        /// <summary>
        /// Скрыть услугу
        /// </summary>
        [HttpDelete]
        public Object Delete(long id)
        {
            mng.HideAsync(id);
            return Ok(true);
        }

        /// <summary>
        /// Создать изображение услуги
        /// </summary>
        [HttpPost]
        [Route("api/services/CreateImage")]
        public Object CreateImage()
        {
            mng.CreateImageAsync(ServiceUtil.Request);
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