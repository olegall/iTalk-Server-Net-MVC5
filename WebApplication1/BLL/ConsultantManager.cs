using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Data.Entity;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using WebApplication1.Models;
using WebApplication1.Utils;
using WebApplication1.Misc;
using WebApplication1.ViewModels;
using WebApplication1.DAL;
using WebApplication1.Interfaces;

namespace WebApplication1.BLL
{
    public class ConsultantManager : IConsultantManager
    {
        #region Fields
        private readonly IGenericRepository<PrivateConsultant> privatesRep;
        private readonly IGenericRepository<JuridicConsultant> juridicsRep;
        private readonly IGenericRepository<Favorite> favoritesRep;
        private readonly IGenericRepository<GalleryImage> galleryImagesRep;
        private readonly IGenericRepository<Feedback> feedbacksRep;
        private readonly IGenericRepository<ConsultantImage> consultantImagesRep;

        private readonly SearchManager searchMng = new SearchManager(Reps.Privates, 
                                                                     Reps.Juridics, 
                                                                     Reps.Categories, 
                                                                     Reps.Subcategories, 
                                                                     Reps.Services);
        //private readonly ISearchManager searchMng; // !
        private readonly int MAX_PRIVATE_DOCS_NUM = 5;
        private readonly int MAX_JURIDIC_DOCS_NUM = 5;
        #endregion

        #region ctor
        public ConsultantManager()
        {
        }
        public ConsultantManager(IGenericRepository<PrivateConsultant> privatesRep, 
                                 IGenericRepository<JuridicConsultant> juridicsRep,
                                 IGenericRepository<Favorite> favoritesRep,
                                 IGenericRepository<GalleryImage> galleryImagesRep,
                                 IGenericRepository<Feedback> feedbacksRep, 
                                 IGenericRepository<ConsultantImage> consultantImagesRep)
        {
            this.privatesRep = privatesRep;
            this.juridicsRep = juridicsRep;
            this.favoritesRep = favoritesRep;
            this.galleryImagesRep = galleryImagesRep;
            this.feedbacksRep = feedbacksRep;
            this.consultantImagesRep = consultantImagesRep;
        }
        #endregion

        #region Private methods
        // !!! GetAsync
        private bool IsInFavorites(long id)
        {
            return favoritesRep.Get().Any(x => x.ConsultantId == id);
        }
        #endregion

        #region Public methods
        // !!! GetAsync
        public PrivateConsultant Get(string phone)
        {
            return privatesRep.Get().SingleOrDefault(x => x.Phone == phone);
        }

        public string GetName(long orderId)
        {
            PrivateConsultant private_ = ServiceUtil.Context.PrivateConsultants // ! убрать дублирование - ввести реп-й Consultant
                                                             .Join(ServiceUtil.Context.Orders,
                                                                   priv => priv.Id,
                                                                   order => order.ConsultantId,
                                                                   (priv, order) => new { Private = priv, Order = order }) // ! убрать
                                                             .Select(x => x.Private)
                                                             .SingleOrDefault();
            if (private_ != null)
            {
                return private_.Name;
            }
            else
            {
                JuridicConsultant juridic = ServiceUtil.Context.JuridicConsultants  
                                                               .Join(ServiceUtil.Context.Orders,
                                                                     jur => jur.Id,
                                                                     order => order.ConsultantId,
                                                                     (jur, order) => new { Juridic = jur, Order = order })
                                                               .Select(x => x.Juridic)
                                                               .SingleOrDefault();
                return juridic.LTDTitle;
            }
        }
        // ! убрать async, await?
        public async Task<PrivateConsultant> GetPrivateAsync(long id)
        {
            try
            {
                return privatesRep.GetAsync(id);
            }
            catch // ! отфильтровать исключение
            {// ! код ошибки
                throw new HttpException("Нет физлица с таким Id или проблемы с доступом к серверу");
            }
            //finally
            //{
            //    privateRep.Dispose();
            //}
        }

        public async Task<JuridicConsultant> GetJuridicAsync(long id)
        {
            try
            {
                return juridicsRep.GetAsync(id);
            }
            catch // ! отфильтровать исключение
            {// ! код ошибки
                throw new HttpException("Нет юрлица с таким Id или проблемы с доступом к серверу");
            }
            //finally
            //{
            //    juridicRep.Dispose();
            //}
        }

        public IEnumerable<PrivateConsultant> GetPrivatesBySubcategory(long subcatId)
        {
            return ServiceUtil.Context.PrivateConsultants
                                      .Join(ServiceUtil.Context.Services.Where(x => x.SubcategoryId == subcatId),
                                            priv => priv.Id,
                                            serv => serv.ConsultantId,
                                            (priv, serv) => new { Private = priv, Serv = serv })
                                      .Select(x => x.Private);
        }

        public IEnumerable<PrivateConsultant> GetPrivates(int offset, 
                                                          int limit, 
                                                          long subcategoryId, 
                                                          bool free, 
                                                          bool onlyFavorite, 
                                                          string filter)
        {
            IEnumerable<PrivateConsultant> privates = GetPrivatesBySubcategory(subcategoryId)
                                                      .Skip(offset)
                                                      .Take(limit)
                                                      .ToArray();
            if (free == true)
            {
                privates = privates.Where(x => x.Free);
            }
            if (onlyFavorite == true)
            {
                privates = privates.Where(x => IsInFavorites(x.Id));
            }
            if (filter != "null")
            {
                privates = searchMng.SearchPrivateConsultants(privates, filter);
            }
            return privates;
        }

        public IEnumerable<JuridicConsultant> GetJuridics(int offset,
                                                          int limit,
                                                          long subcategoryId,
                                                          bool free,
                                                          bool onlyFavorite,
                                                          string filter)
        {
            IEnumerable<JuridicConsultant> juridics = GetJuridicsBySubcategory(subcategoryId)
                                                      .Skip(offset)
                                                      .Take(limit)
                                                      .ToArray();
            if (free == true)
            {
                juridics = juridics.Where(x => x.Free);
            }
            if (onlyFavorite == true)
            {
                juridics = juridics.Where(x => IsInFavorites(x.Id));
            }
            if (filter != "null")
            {
                juridics = searchMng.SearchJuridicConsultants(juridics, filter);
            }
            return juridics;
        }

        public PrivateConsultant GetPrivateByPhone(string phone)
        {
            try
            {          // !!! GetAsync
                return privatesRep.Get().SingleOrDefault(x => x.Phone == phone);
            }
            catch // !!! отфильтровать исключение
            {// !!! код ошибки
                throw new HttpException("Нет физлица с таким телефоном или проблемы с доступом к серверу");
            }
            //finally
            //{
            //    privateRep.Dispose();
            //}
        }
        // !!! GetAsync
        public JuridicConsultant GetJuridicByPhone(string phone)
        {
            try
            {
                return juridicsRep.Get().SingleOrDefault(x => x.Phone == phone);
            }
            catch // !!! отфильтровать исключение
            {// !!! код ошибки
                throw new HttpException("Нет юрлица с таким телефоном или проблемы с доступом к серверу");
            }
            //finally
            //{
            //    juridicRep.Dispose();  // !!! везде для Get
            //}
        }

        public PrivateConsultantVM GetPrivateVM(PrivateConsultant private_)
        {
            return new PrivateConsultantVM
            {
                Id = private_.Id,
                Name = private_.Name,
                Surname = private_.Surname,
                Patronymic = private_.Patronymic,
                Photo = BaseUrl.Get($"consimage/{private_.Id}"),
                GalleryImages = GetGalleryImagesNames(private_.Id),
                Rating = private_.Rating,
                FeedbacksCount = GetFeedbacksCount(private_.Id),
                Services =  new ServiceManager().GetVM(private_),
                Free = private_.Free,
                FreeDate = ServiceUtil.DateTimeToUnixTimestamp(private_.FreeDate),
                Favorite = IsFavorite(private_.Id),
                LegalEntity = IsJuridic(private_.Id)
            };
        }

        public JuridicConsultantVM GetJuridicVM(JuridicConsultant juridic)
        {
            return new JuridicConsultantVM
            {
                LTDTitle = juridic.LTDTitle,
                OGRN = juridic.OGRN,
                INN = juridic.INN,
                SiteUrl = juridic.SiteUrl,
                Logo = BaseUrl.Get($"consimage/{juridic.Id}"),
                OGRNCertificate = juridic.OGRNCertificate,
                BankAccountDetails = juridic.BankAccountDetails,
                AccountNumber = juridic.AccountNumber,
                Rating = juridic.Rating,
                FeedbacksCount = GetFeedbacksCount(juridic.Id),
                Services = new ServiceManager().GetVM(juridic),
                Free = juridic.Free,
                FreeDate = ServiceUtil.DateTimeToUnixTimestamp(juridic.FreeDate),
                Favorite = IsFavorite(juridic.Id),
                LegalEntity = IsJuridic(juridic.Id)
            };
        }

        public IEnumerable<PrivateConsultantVM> GetPrivatesVMs(long subcatId)
        {
            IList<PrivateConsultantVM> vms = new List<PrivateConsultantVM>();
            foreach (PrivateConsultant private_ in GetPrivatesBySubcategory(subcatId))
            {
                vms.Add(GetPrivateVM(private_));
            }
            return vms;
        }

        public IEnumerable<PrivateConsultantVM> GetPrivateVMs(int offset, 
                                                              int limit, 
                                                              long subcategoryId, 
                                                              bool free, 
                                                              bool onlyFavorite, 
                                                              string filter)
        {
            IList<PrivateConsultantVM> vms = new List<PrivateConsultantVM>();
            foreach (PrivateConsultant private_ in GetPrivates(offset, limit, subcategoryId, free, onlyFavorite, filter))
            {
                vms.Add(GetPrivateVM(private_));
            }
            return vms;
        }

        public IEnumerable<JuridicConsultantVM> GetJuridicsVMs(long subcatId)
        {
            IList<JuridicConsultantVM> vms = new List<JuridicConsultantVM>();
            foreach (JuridicConsultant juridic in GetJuridicsBySubcategory(subcatId))
            {
                vms.Add(GetJuridicVM(juridic));
            }
            return vms;
        }

        public IEnumerable<JuridicConsultantVM> GetJuridicsVMs(int offset, 
                                                               int limit, 
                                                               long subcategoryId, 
                                                               bool free, 
                                                               bool onlyFavorite, 
                                                               string filter)
        {
            IList<JuridicConsultantVM> vms = new List<JuridicConsultantVM>();
            foreach (JuridicConsultant juridic in GetJuridics(offset, limit, subcategoryId, free, onlyFavorite, filter))
            {
                vms.Add(GetJuridicVM(juridic));
            }
            return vms;
        }

        public IEnumerable<JuridicConsultant> GetJuridicsBySubcategory(long subcatId)
        {
            return ServiceUtil.Context.JuridicConsultants
                              .Join(ServiceUtil.Context.Services.Where(x => x.SubcategoryId == subcatId),
                                   jur => jur.Id,
                                   serv => serv.ConsultantId,
                                   (jur, serv) => new { Juridic = jur, Serv = serv })
                              .Select(x => x.Juridic);
        }

        public IList<long> GetAvailableTimes(long time)
        {
            return new List<long> { 123456789,
                                    111111111,
                                    123457777};
        }

        public bool IsFavorite(long id)
        {
            return favoritesRep.Get().Any(x => x.ConsultantId == id);
        }

        public bool IsJuridic(long id)
        {
            if (juridicsRep.Get().Any(x => x.Id == id))
                return true;

            if (privatesRep.Get().Any(x => x.Id == id))
                return false;

            throw new HttpException(500, "Нет консультанта с таким Id");
        }

        public IEnumerable<string> GetGalleryImagesNames(long id)
        {
            IEnumerable<long> imageIds = galleryImagesRep.Get().Where(x => x.ConsultantId == id)
                                                           .Select(x => x.Id);
            IList<string> images = new List<string>();
            foreach (long imageId in imageIds)
            {
                images.Add(BaseUrl.Get($"galleryimage/{imageId}"));
            }
            return images;
        }

        public int GetFeedbacksCount(long consultantId)
        {
            return feedbacksRep.Get().Where(x => x.ConsultantId == consultantId)
                                       .ToList()
                                       .Count;
        }
        // !!! проще предпоследнюю строку
        // !!! избавиться от подчёркивания
        public long CreatePrivateAsync(string name, 
                                       string surname, 
                                       string patronymic, 
                                       string phone, 
                                       string email)
        {
            try
            {
                privatesRep.CreateAsync(new PrivateConsultant(name,
                                                             surname,
                                                             patronymic,
                                                             phone,
                                                             email,
                                                             DateTime.Now));
            }
            catch
            {
                throw new HttpException(500, "Не удалось добавить физ.лицо при регистрации");
            }
            long createdId = privatesRep.Get().OrderByDescending(x => x.Id)
                                             .Select(x => x.Id)
                                             .First();
            return createdId;
        }

        public async Task CreatePrivateImagesAsync(HttpFileCollection files, long id)
        {
            HttpPostedFile photo = files["photo"];
            if (photo != null)
            {   // !!! async
                await CreateImageAsync(photo, (long)FileTypes.Photo, id);
            }

            HttpPostedFile passportscan = files["passportscan"];
            if (passportscan != null)
            {
                await CreateImageAsync(passportscan, (long)FileTypes.PassportScan, id);
            }

            for (int i = 1; i <= MAX_PRIVATE_DOCS_NUM; i++)
            {
                HttpPostedFile doc = files["doc" + i];
                if (doc != null)
                {
                    await CreateImageAsync(doc, (long)FileTypes.PrivateDoc, id);
                }
            }
        }

        public long CreateJuridic(string ltdtitle,
                                  string ogrn,
                                  string inn,
                                  string phone,
                                  string siteurl)
        {
            try
            {
                juridicsRep.CreateAsync(new JuridicConsultant(ltdtitle, ogrn, inn, phone, siteurl, DateTime.Now));
            }
            catch
            {
                throw new HttpException(500, "Не удалось добавить юр. консультанта при регистрации");
            }

            long createdId = juridicsRep.Get().OrderByDescending(x => x.Id)
                                              .Select(x => x.Id)
                                              .First();
            return createdId;
        }

        public async Task CreateJuridicImagesAsync(HttpFileCollection files, long id)
        {
            if (files["logo"] != null)
            {
                await CreateImageAsync(files["logo"], (long)FileTypes.Logo, id);
            }
            for (int i = 1; i <= MAX_JURIDIC_DOCS_NUM; i++)
            {
                HttpPostedFile file = files["doc" + i];
                if (file != null)
                {
                    await CreateImageAsync(file, (long)FileTypes.JuridicDoc, id);
                }
            }
        }

        public async Task CreateImageAsync(HttpPostedFile file, long fileTypeid, long consId)
        {
            try
            {
                await consultantImagesRep.CreateAsync(new ConsultantImage(consId, 
                                                                            ServiceUtil.GetBytesFromStream(file.InputStream), 
                                                                            file.FileName, 
                                                                            file.ContentLength, 
                                                                            DateTime.Now, 
                                                                            fileTypeid));
            }
            catch
            {
                throw new HttpException(500, "Не удалось добавить изображение");
            }
        }
        // !!! везде проверки на null
        public async Task CreateImageWhenUpdateAsync(HttpRequest request, long consId)
        {
            foreach (FileTypes fileType in (FileTypes[])Enum.GetValues(typeof(FileTypes)))
            {
                HttpPostedFile file = request.Files[fileType.ToString()];
                if (file != null)
                {
                    try
                    {
                        await consultantImagesRep.CreateAsync(new ConsultantImage(consId,
                                                                                    ServiceUtil.GetBytesFromStream(file.InputStream),
                                                                                    file.FileName,
                                                                                    file.ContentLength,
                                                                                    DateTime.Now,
                                                                                    (long)fileType));
                    }
                    catch
                    {                                                                // ! (я)
                        throw new HttpException(500, "Не удалось добавить изображение(я)");
                    }
                }
            }
        }

        public async Task DeleteImageAsync(long id)
        {
            ConsultantImage image = consultantImagesRep.GetAsync(id);
            try
            {
                await consultantImagesRep.DeleteAsync(image);
            }
            catch
            {
                throw new HttpException(500, "Не удалось удалить изображение юр. консультанта");
            }
        }

        public async Task UpdatePrivateFieldsAsync(long id, NameValueCollection formData)
        {
            PrivateConsultant private_ = new PrivateConsultant(formData["name"], 
                                                               formData["surname"], 
                                                               formData["patronymic"],
                                                               formData["phone"], 
                                                               formData["email"], 
                                                               Convert.ToDateTime(formData["freedate"]));
            private_.Id = id;
            try
            {
                await privatesRep.UpdateAsync(private_);
            }
            catch
            {
                throw new HttpException(500, "Не удалось обновить поле(я) физ. лица");
            }
        }

        public async Task UpdateJuridicFieldsAsync(long id, NameValueCollection formData)
        {
            JuridicConsultant juridic = new JuridicConsultant(formData["ltdtitle"], 
                                                              formData["ogrn"], 
                                                              formData["inn"], 
                                                              formData["phone"], 
                                                              formData["siteurl"], 
                                                              Convert.ToDateTime(formData["freedate"]));
            juridic.Id = id;
            try
            {
                await juridicsRep.UpdateAsync(juridic);
            }
            catch (Exception e)
            {
                throw new HttpException(500, "Не удалось обновить поле(я) юр. лица");
            }
        }

        public async Task UpdateRatingAsync(long id, decimal rating)
        {
            PrivateConsultant private_ = privatesRep.GetAsync(id);
            if (private_ != null)
            {
                private_.Rating = rating;
                try
                {
                    await privatesRep.UpdateAsync(private_);
                }
                catch
                {
                    throw new HttpException(500, "Не удалось обновить рейтинг физлица");
                }
                return;
            }
            JuridicConsultant juridic = juridicsRep.GetAsync(id);
            juridic.Rating = rating;
            try
            { 
                await juridicsRep.UpdateAsync(juridic);
            }
            catch
            {
                throw new HttpException(500, "Не удалось обновить рейтинг юрлица");
            }
        }
        #endregion
    }
}