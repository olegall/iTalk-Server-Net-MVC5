using System;
using System.Collections.Generic;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using WebApplication1.BLL;
using WebApplication1.Models;
using System.Web;
using System.Collections.Specialized;
using WebApplication1.Utils;

namespace WebApplication1.Controllers
{
    public class ConsultantsController : ApiController
    {
        private readonly ConsultantBLL BLL = new ConsultantBLL();
        private readonly SearchBLL searchBLL = new SearchBLL();

        /// <summary>
        /// Получить консультантов
        /// </summary>
        [HttpGet]
        [Route("api/consultants/{offset}/{limit}/{subcategoryId}/{free}/{onlyFavorite}/{filter}")]
        // физлица: api/consultants/0/6/1/false/true/null
        // юрлица: api/consultants/0/2/2/false/false/null
        public Object Get(int offset, 
                          int limit, 
                          long subcategoryId, 
                          bool free, 
                          bool onlyFavorite, 
                          string filter)
        {
            IList<ConsultantVM> VMs = new List<ConsultantVM>();
            foreach (PrivateConsultantVM privateVM in BLL.GetPrivatesVMs(offset, limit, subcategoryId, free, onlyFavorite, filter))
            {
                VMs.Add(privateVM);
            }
            foreach (JuridicConsultantVM juridicVM in BLL.GetJuridicsVMs(offset, limit, subcategoryId, free, onlyFavorite, filter))
            {
                VMs.Add(juridicVM);
            }
            return Ok(VMs);
        }

        /// <summary>
        /// Получить консультанта для карточки
        /// </summary>
        public Object Get(int id)
        {
            if (BLL.IsJuridic(id))
            {
                JuridicConsultant juridic = BLL.GetJuridic(id);
                return Ok(BLL.GetJuridicVM(juridic));
            }
            else
            {
                PrivateConsultant private_ = BLL.GetPrivate(id);
                return Ok(BLL.GetPrivateVM(private_));
            }
        }

        /// <summary>
        /// Получить консультантов подкатегории
        /// </summary>
        [HttpGet]
        [Route("api/consultants/subcategory/{id}")]
        public Object GetBySubcategory(int id)
        {
            IList<ConsultantVM> VMs = new List<ConsultantVM>();
            foreach (PrivateConsultantVM privateVM in BLL.GetPrivatesVMs(id))
            {
                VMs.Add(privateVM);
            }
            foreach (JuridicConsultantVM juridicVM in BLL.GetJuridicsVMs(id))
            {
                VMs.Add(juridicVM);
            }
            return Ok(VMs);
        }

        /// <summary>
        /// Получить свободные времена дня
        /// </summary>
        [HttpPut]
        [Route("api/consultants/availableTimes/{time}")]
        public Object AvailableTimes(long time)
        {
            return Ok(BLL.GetAvailableTimes(time));
        }

        /// <summary>
        /// Зарегистрировать физлицо
        /// </summary>
        [HttpPost]
        [Route("api/consultants/RegisterPrivate")]
        public Object RegisterPrivate()
        {
            if (!Request.Content.IsMimeMultipartContent())
            {
                throw new HttpResponseException(HttpStatusCode.UnsupportedMediaType);
            }
            HttpRequest httpRequest = HttpContext.Current.Request;
            NameValueCollection form = httpRequest.Form;
            long lastCreatedId = BLL.CreatePrivate(form["name"], 
                                                   form["surname"], 
                                                   form["patronymic"], 
                                                   form["phone"], 
                                                   form["email"]);
            BLL.CreatePrivateImages(httpRequest.Files, lastCreatedId);
            return Ok(lastCreatedId);
        }

        /// <summary>
        /// Зарегистрировать юрлицо
        /// </summary>
        [HttpPost]
        [Route("api/consultants/RegisterJuridic")]
        public Object RegisterJuridic()
        {
            if (!Request.Content.IsMimeMultipartContent())
            {
                throw new HttpResponseException(HttpStatusCode.UnsupportedMediaType);
            }
            HttpRequest httpRequest = HttpContext.Current.Request;
            NameValueCollection form = httpRequest.Form;
            long lastCreatedId = BLL.CreateJuridic(form["ltdtitle"], 
                                                   form["ogrn"], 
                                                   form["inn"], 
                                                   form["phone"], 
                                                   form["siteurl"]);
            BLL.CreateJuridicImages(httpRequest.Files, lastCreatedId);
            return Ok(lastCreatedId);
        }

        /// <summary>
        /// Редактировать поля физлица
        /// </summary>
        [HttpPut]
        [Route("api/consultants/private/{id}")]
        public Object UpdatePrivateFields(long id)
        {
            if (!Request.Content.IsMimeMultipartContent())
            {
                throw new HttpResponseException(HttpStatusCode.UnsupportedMediaType);
            }
            NameValueCollection form = ServiceUtil.Request.Form;
            BLL.UpdatePrivateFields(id, ServiceUtil.Request.Form);
            return Ok(true);
        }

        /// <summary>
        /// Редактировать поля юрлица
        /// </summary>
        [HttpPut]
        [Route("api/consultants/juridic/{id}")]
        public Object UpdateJuridicFields(long id)
        {
            if (!Request.Content.IsMimeMultipartContent())
            {
                throw new HttpResponseException(HttpStatusCode.UnsupportedMediaType);
            }
            BLL.UpdateJuridicFields(id, ServiceUtil.Request.Form);
            return Ok(true);
        }

        /// <summary>
        /// Добавить изображение консультанта - при его редактировании
        /// </summary>
        [HttpPost]
        [Route("api/consultants/{id}/image")]
        public Object CreateImage(long id)
        {
            BLL.CreateImageWhenUpdate(ServiceUtil.Request, id);
            return Ok(true);
        }

        /// <summary>
        /// Удалить изображение физ/юрлица - при его редактировании
        /// </summary>
        [HttpDelete]
        [Route("api/consultants/image/{id}")]
        public Object DeleteImage(long id)
        {
            BLL.DeleteImage(id);
            return Ok(true);
        }

        /// <summary>
        /// Получить инфо о загруженности
        /// </summary>
        [HttpGet]
        [Route("api/consultants/{id}/loading")]
        public Object GetLoading(int id)
        {
            return Ok(true);
        }

        /// <summary>
        /// Редактировать рейтинг
        /// </summary>
        [HttpPut]
        [Route("api/consultants/{id}/rating/{rating}")]
        public Object UpdateRating(int id, decimal rating)
        {
            BLL.UpdateRating(id, rating);
            return Ok(true);
        }

        /// <summary>
        /// Получить консультантов
        /// </summary>
        [HttpGet]
        [Route("api/consultants/test")]
        public void Test()
        {
            var rep = new DAL.Repositories();
            GenericRepository<PrivateConsultant> privateRep = rep.Privates;
            var privates = privateRep.Get();
            var searched1 = searchBLL.SearchPrivateConsultants(privates, "Александр Лавров");
            var searched2 = searchBLL.SearchPrivateConsultants(privates, "Александр Петрович");
            var searched3 = searchBLL.SearchPrivateConsultants(privates, "Александр Петрович Лавров");
            var searched4 = searchBLL.SearchPrivateConsultants(privates, "Алексан");
            var searched5 = searchBLL.SearchPrivateConsultants(privates, "Александр Пе");
            var searched6 = searchBLL.SearchPrivateConsultants(privates, "Александр Юристы");
            var searched7 = searchBLL.SearchPrivateConsultants(privates, "Александр Врачи");

            GenericRepository<JuridicConsultant> juridicRep = rep.Juridics;
            var juridics = juridicRep.Get();
            var searched8 = searchBLL.SearchJuridicConsultants(juridics, "Окна Профи");   // не работает
            var searched9 = searchBLL.SearchJuridicConsultants(juridics, "Окна Профи Установщики окон");   // сделать комбинации
        }
    }
}