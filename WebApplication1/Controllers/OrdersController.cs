using System;
using System.Web.Http;
using System.Threading.Tasks;
using WebApplication1.BLL;
using WebApplication1.Utils;
using WebApplication1.DAL;
using WebApplication1.Models;
using WebApplication1.Misc;

namespace WebApplication1.Controllers
{
    public class OrdersController : BaseApiController<Misc.Auth.ConsultantManager>
    {
        private readonly OrderManager mng = new OrderManager(Reps.Orders, 
                                                             Reps.ConsultationTypes, 
                                                             Reps.OrderStatuses, 
                                                             Reps.PaymentStatuses);

        /// <summary>
        /// Оформить (создать) клиентом
        /// </summary>
        [HttpPost]
        public async Task<IHttpActionResult> Post()
        {
            CRUDResult<Order> result = await mng.CreateAsync(ServiceUtil.Request.Form);
            return SendResult<Order>(result);
        }

        /// <summary>
        /// Подтвердить консультантом
        /// </summary>
        [HttpPut]
        [Route("api/orders/{id}/confirm")]
        public async Task<IHttpActionResult> Confirm(long id)
        {
            CRUDResult<Order> result = await mng.ConfirmAsync(id);
            return SendResult<Order>(result);
        }

        /// <summary>
        /// Отменить клиентом
        /// </summary>
        [HttpPut]
        [Route("api/orders/{id}/CancelByClient")]
        public async Task<IHttpActionResult> CancelByClient(long id)
        {
            CRUDResult<Order> result = await mng.CancelByClientAsync(id);
            return SendResult<Order>(result);
        }

        /// <summary>
        /// Отменить консультантом
        /// </summary>
        [HttpPut]
        [Route("api/orders/{id}/CancelByCons")]
        public async Task<IHttpActionResult> CancelByConsAsync(long id)
        {
            CRUDResult<Order> result = await mng.CancelByConsAsync(id);
            return SendResult<Order>(result);
        }

        /// <summary>
        /// Получить типы консультаций для оформления заказа
        /// </summary>
        [HttpGet]
        [Route("api/orders/ConsultationTypes")]
        public Object GetConsultationTypes()
        {
            return Ok(mng.GetConsultationTypes());
        }

        /// <summary>
        /// Получить заказы клиента
        /// </summary>
        [HttpGet]
        [Route("api/orders/client/{id}")]
        public Object GetByClientId(long id)
        {
            return Ok(mng.GetClientVMs(id));
        }

        /// <summary>
        /// Получить клиентом инфо о заказе
        /// </summary>
        [HttpGet]
        [Route("api/orders/{id}")]
        public Object Get(long id)
        {
            return Ok(mng.GetVMAsync(id));
        }

        /// <summary>
        /// Получить заказы консультанта
        /// </summary>
        [HttpGet]
        [Route("api/orders/consultant/{id}")]
        public Object GetByConsultantId(long id)
        {
            return Ok(mng.GetConsVMs(id));
        }

        /// <summary>
        /// Получить заказы для клиента
        /// </summary>
        [HttpGet]
        [Route("api/orders/{offset}/{limit}")]
        public Object Get(int offset, int limit)
        {
            return Ok(mng.GetVMs(offset, limit));
        }

        /// <summary>
        /// Обновить время
        /// </summary>
        [HttpPut]
        [Route("api/order/{id}/{timestamp}")]
        public async Task<IHttpActionResult> UpdateTime(long id, long timestamp)
        {
            CRUDResult<Order> result = await mng.UpdateTimeAsync(id, timestamp);
            return SendResult<Order>(result);
        }
    }
}