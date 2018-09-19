using System;
using System.Web.Http;
using WebApplication1.BLL;

namespace WebApplication1.Controllers
{
    public class AgreementController : ApiController
    {
        private readonly AgreementBLL BLL = new AgreementBLL();

        /// <summary>
        /// Получить ссылку на пользовательское соглашение
        /// </summary>
        [HttpGet]
        public Object Get()
        {
            return Ok(BLL.Link);
        }
    }
}