using System;
using System.Web.Http;
using WebApplication1.BLL;
using WebApplication1.Utils;

namespace WebApplication1.Controllers
{
    public class OrdersController : ApiController
    {
        private readonly OrderBLL BLL = new OrderBLL();

        /// <summary>
        /// Оформить (создать) клиентом
        /// </summary>
        [HttpPost]
        public Object Post()
        {
            BLL.Create(ServiceUtil.Request.Form);
            return Ok(true);
        }

        /// <summary>
        /// Подтвердить консультантом
        /// </summary>
        [HttpPut]
        [Route("api/orders/{id}/confirm")]
        public Object Confirm(long id)
        {
            BLL.ConfirmAsync(id);
            return Ok(true);
        }

        /// <summary>
        /// Отменить клиентом
        /// </summary>
        [HttpPut]
        [Route("api/orders/{id}/CancelByClient")]
        public Object CancelByClient(long id)
        {
            BLL.CancelByClientAsync(id);
            return Ok(true);
        }

        /// <summary>
        /// Отменить консультантом
        /// </summary>
        [HttpPut]
        [Route("api/orders/{id}/CancelByCons")]
        public Object CancelByConsAsync(long id)
        {
            BLL.CancelByConsAsync(id);
            return Ok(true);
        }

        /// <summary>
        /// Получить типы консультаций для оформления заказа
        /// </summary>
        [HttpGet]
        [Route("api/orders/ConsultationTypes")]
        public Object GetConsultationTypes()
        {
            return Ok(BLL.GetConsultationTypesAsync());
        }

        /// <summary>
        /// Получить заказы клиента
        /// </summary>
        [HttpGet]
        [Route("api/orders/client/{id}")]
        public Object GetByClientId(long id)
        {
            return Ok(BLL.GetClientVMs(id));
        }

        /// <summary>
        /// Получить клиентом инфо о заказе
        /// </summary>
        [HttpGet]
        [Route("api/orders/{id}")]
        public Object Get(long id)
        {
            return Ok(BLL.GetVMAsync(id));
        }

        /// <summary>
        /// Получить заказы консультанта
        /// </summary>
        [HttpGet]
        [Route("api/orders/consultant/{id}")]
        public Object GetByConsultantId(long id)
        {
            return Ok(BLL.GetConsVMs(id));
        }

        /// <summary>
        /// Получить заказы для клиента
        /// </summary>
        [HttpGet]
        [Route("api/orders/{offset}/{limit}")]
        public Object Get(int offset, int limit)
        {
            return Ok(BLL.GetVMs(offset, limit));
        }

        /// <summary>
        /// Обновить время
        /// </summary>
        [HttpPut]
        [Route("api/order/{id}/{timestamp}")]
        public Object UpdateTime(long id, long timestamp)
        {
            BLL.UpdateTimeAsync(id, timestamp);
            return Ok(true);
        }
    }
}