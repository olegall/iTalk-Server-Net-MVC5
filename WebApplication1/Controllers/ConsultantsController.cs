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
using System.Threading.Tasks;

namespace WebApplication1.Controllers
{
    public class ConsultantsController : ApiController
    {
        private readonly ConsultantManager mng = new ConsultantManager();
        private readonly SearchManager searchMng = new SearchManager();

        /// <summary>
        /// Получить консультантов
        /// </summary>
        [HttpGet]
        [Route("api/consultants/{offset}/{limit}/{subcategoryId}/{free}/{onlyFavorite}/{filter}")]
        // физлица: api/consultants/0/6/1/false/true/null
        // юрлица: api/consultants/0/2/2/false/false/null
        public Object Get(int offset, // !!! модель
                          int limit, 
                          long subcategoryId, 
                          bool free, 
                          bool onlyFavorite, 
                          string filter)
        {
            IList<ConsultantVM> VMs = new List<ConsultantVM>();
            foreach (PrivateConsultantVM privateVM in mng.GetPrivateVMs(offset, limit, subcategoryId, free, onlyFavorite, filter))
            {
                VMs.Add(privateVM);
            }
            foreach (JuridicConsultantVM juridicVM in mng.GetJuridicsVMs(offset, limit, subcategoryId, free, onlyFavorite, filter))
            {
                VMs.Add(juridicVM);
            }
            return Ok(VMs);
        }

        /// <summary>
        /// Получить консультанта для карточки
        /// </summary>
        // избавиться от прочерка, Object
        public async Task<Object> Get(int id)
        {
            if (mng.IsJuridic(id))
            {
                JuridicConsultant juridic = await mng.GetJuridicAsync(id);
                return Ok(mng.GetJuridicVM(juridic));
            }
            else
            {
                PrivateConsultant private_ = await mng.GetPrivateAsync(id);
                return Ok(mng.GetPrivateVM(private_));
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
            foreach (PrivateConsultantVM privateVM in mng.GetPrivatesVMs(id))
            {
                VMs.Add(privateVM);
            }
            foreach (JuridicConsultantVM juridicVM in mng.GetJuridicsVMs(id))
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
            return Ok(mng.GetAvailableTimes(time));
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
            var createdId = mng.CreatePrivateAsync(form["name"], 
                                                   form["surname"], 
                                                   form["patronymic"], 
                                                   form["phone"], 
                                                   form["email"]);
            mng.CreatePrivateImages(httpRequest.Files, createdId);
            return Ok(createdId);
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
            long lastCreatedId = mng.CreateJuridic(form["ltdtitle"], // ! async
                                                   form["ogrn"], 
                                                   form["inn"], 
                                                   form["phone"], 
                                                   form["siteurl"]);
            mng.CreateJuridicImagesAsync(httpRequest.Files, lastCreatedId);
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
            mng.UpdatePrivateFieldsAsync(id, ServiceUtil.Request.Form);
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
            mng.UpdateJuridicFieldsAsync(id, ServiceUtil.Request.Form);
            return Ok(true);
        }

        /// <summary>
        /// Добавить изображение консультанта - при его редактировании
        /// </summary>
        [HttpPost]
        [Route("api/consultants/{id}/image")]
        public Object CreateImage(long id)
        {
            mng.CreateImageWhenUpdateAsync(ServiceUtil.Request, id);
            return Ok(true);
        }

        /// <summary>
        /// Удалить изображение физ/юрлица - при его редактировании
        /// </summary>
        [HttpDelete]
        [Route("api/consultants/image/{id}")]
        public Object DeleteImage(long id)
        {
            mng.DeleteImageAsync(id);
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
            mng.UpdateRatingAsync(id, rating);
            return Ok(true);
        }

        /// <summary>
        /// Получить консультантов
        /// </summary>
        [HttpGet]
        [Route("api/consultants/test")]
        public void Test()
        {
            //var rep = new DAL.Repositories();
            GenericRepository<PrivateConsultant> privateRep = DAL.Reps.Privates;
            var privates = privateRep.Get();
            var searched1 = searchMng.SearchPrivateConsultants(privates, "Александр Лавров");
            var searched2 = searchMng.SearchPrivateConsultants(privates, "Александр Петрович");
            var searched3 = searchMng.SearchPrivateConsultants(privates, "Александр Петрович Лавров");
            var searched4 = searchMng.SearchPrivateConsultants(privates, "Алексан");
            var searched5 = searchMng.SearchPrivateConsultants(privates, "Александр Пе");
            var searched6 = searchMng.SearchPrivateConsultants(privates, "Александр Юристы");
            var searched7 = searchMng.SearchPrivateConsultants(privates, "Александр Врачи");

            GenericRepository<JuridicConsultant> juridicRep = DAL.Reps.Juridics;
            var juridics = juridicRep.Get();
            var searched8 = searchMng.SearchJuridicConsultants(juridics, "Окна Профи");   // не работает
            var searched9 = searchMng.SearchJuridicConsultants(juridics, "Окна Профи Установщики окон");   // сделать комбинации
        }
    }
}