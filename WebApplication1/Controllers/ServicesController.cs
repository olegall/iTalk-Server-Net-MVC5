using System;
using System.Net;
using System.Web.Http;
using System.Threading.Tasks;
using WebApplication1.BLL;
using WebApplication1.Utils;
using WebApplication1.DAL;
using WebApplication1.Models;
using WebApplication1.Misc;

namespace WebApplication1.Controllers
{
    public class ServicesController : BaseApiController<Misc.Auth.ConsultantManager>
    {
        private readonly ServiceManager mng = new ServiceManager(Reps.Services,
                                                                 Reps.ServiceImages, 
                                                                 Reps.Categories, 
                                                                 Reps.Subcategories);

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
        public async Task<IHttpActionResult> Post()
        {
            CRUDResult<Service> result = await mng.CreateAsync(ServiceUtil.Request.Form);
            return SendResult<Service>(result);
        }

        /// <summary>
        /// Добавить сразу много услуг
        /// </summary>
        [HttpPost]
        [Route("api/services/many")]
        public Object CreateMany() // !
        {
            mng.CreateManyAsync(ServiceUtil.Request.Form);
            return Ok(true);
        }

        /// <summary>
        /// Создать изображение услуги
        /// </summary>
        [HttpPost]
        [Route("api/services/CreateImage")]
        public Object CreateImage() // !
        {
            mng.CreateImageAsync(ServiceUtil.Request);
            return Ok(true);
        }

        /// <summary>
        /// Редактировать услугу
        /// </summary>
        [HttpPut]
        public async Task<IHttpActionResult> Update()
        {
            CRUDResult<Service> result = await mng.UpdateAsync(ServiceUtil.Request.Form);
            return SendResult<Service>(result);
        }

        /// <summary>
        /// Скрыть услугу
        /// </summary>
        [HttpDelete]
        public async Task<IHttpActionResult> Delete(long id)
        {
            CRUDResult<Service> result = await mng.HideAsync(id);
            return SendResult<Service>(result);
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