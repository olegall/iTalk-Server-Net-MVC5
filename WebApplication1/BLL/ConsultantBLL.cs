using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Data.Entity;
using System.Linq;
using System.Web;
using WebApplication1.Models;
using WebApplication1.Utils;
using WebApplication1.DAL;
using WebApplication1.Misc;
using System.Threading.Tasks;

namespace WebApplication1.BLL
{
    public class ConsultantBLL
    {
        #region Fields
        private readonly static Repositories rep = new Repositories();
        private readonly GenericRepository<PrivateConsultant> privateRep = rep.Privates;
        private readonly GenericRepository<JuridicConsultant> juridicRep = rep.Juridics;
        private readonly GenericRepository<ConsultantImage> imageRep = rep.ConsultantImages;
        private readonly GenericRepository<GalleryImage> galleryImageRep = rep.GalleryImages;
        private readonly GenericRepository<Category> categoryRep = rep.Categories;
        private readonly GenericRepository<Subcategory> subcatRep = rep.Subcategories;
        private readonly GenericRepository<Service> serviceRep = rep.Services;
        private readonly GenericRepository<Favorite> favoriteRep = rep.Favorites;
        private readonly GenericRepository<Order> orderRep = rep.Orders;
        private readonly GenericRepository<Feedback> feedbackRep = rep.Feedbacks;
        #endregion

        #region Constants
        private const int MAX_PRIVATE_DOCS_NUM = 5;
        private const int MAX_JURIDIC_DOCS_NUM = 5;
        private const byte BUSY = 0x00000001;
        private const byte FREE = 0x00000010;
        #endregion
        // !!! везде regions
        private readonly SearchBLL searchBLL = new SearchBLL();
        // !!! GetAsync
        public PrivateConsultant Get(string phone)
        {
            return privateRep.Get().SingleOrDefault(x => x.Phone == phone);
        }
        // !!! join
        public string GetName(long orderId)
        {
            long consId = orderRep.Get().Where(x => x.Id == orderId)
                                        .Select(x => x.ConsultantId)
                                        .SingleOrDefault();
            PrivateConsultant private_ = privateRep.Get().Where(x => x.Id == consId)
                                                         .SingleOrDefault();
            if (private_ != null)
            {
                return private_.Name;
            }
            else
            {
                return juridicRep.Get().Where(x => x.Id == consId)
                                       .SingleOrDefault().LTDTitle;
            }
        }
        // !!! убрать async, await?
        public async Task<PrivateConsultant> GetPrivateAsync(long id)
        {
            return await privateRep.GetAsync(id);
        }
        // !!! убрать async, await?
        public async Task<JuridicConsultant> GetJuridicAsync(long id)
        {
            return await juridicRep.GetAsync(id);
        }
        // !!! join
        public IEnumerable<PrivateConsultant> GetPrivatesBySubcategory(long subcatId)
        {
            IEnumerable<long> ids = serviceRep.Get().Where(x => x.SubcategoryId == subcatId)
                                                    .Select(x => x.ConsultantId)
                                                    .Distinct()
                                                    .ToArray();
            IList<PrivateConsultant> privates = new List<PrivateConsultant>();
            foreach (long id in ids)
            {
                if (privateRep.Get().Any(x => x.Id == id))
                {
                    privates.Add(privateRep.Get().SingleOrDefault(x => x.Id == id));
                }
            }
            return privates;
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
                privates = searchBLL.SearchPrivateConsultants(privates, filter);
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
                juridics = searchBLL.SearchJuridicConsultants(juridics, filter);
            }
            return juridics;
        }
        // !!! GetAsync
        public PrivateConsultant GetPrivateByPhone(string phone)
        {
            return privateRep.Get().SingleOrDefault(x => x.Phone == phone);
        }
        // !!! GetAsync
        public JuridicConsultant GetJuridicByPhone(string phone)
        {
            return juridicRep.Get().SingleOrDefault(x => x.Phone == phone);
        }
        // !!! GetAsync
        private bool IsInFavorites(long id)
        {
            return favoriteRep.Get().Any(x => x.ConsultantId == id);
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
                Services =  new ServiceBLL().GetVM(private_),
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
                Services = new ServiceBLL().GetVM(juridic),
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
        // !!! join
        public IEnumerable<JuridicConsultant> GetJuridicsBySubcategory(long subcatId)
        {
            IEnumerable<long> ids = serviceRep.Get().Where(x => x.SubcategoryId == subcatId)
                                                    .Select(x => x.ConsultantId)
                                                    .Distinct()
                                                    .ToArray();
            IList<JuridicConsultant> juridics = new List<JuridicConsultant>();
            foreach (long id in ids)
            {
                if (juridicRep.Get().Any(x => x.Id == id))
                {
                    juridics.Add(juridicRep.Get().SingleOrDefault(x => x.Id == id));
                }
            }
            return juridics;
        }

        public IList<long> GetAvailableTimes(long time)
        {
            return new List<long> { 123456789,
                                    111111111,
                                    123457777};
        }

        public bool IsFavorite(long id)
        {
            return favoriteRep.Get().Any(x => x.ConsultantId == id);
        }

        public bool IsJuridic(long id)
        {
            if (juridicRep.Get().Any(x => x.Id == id))
                return true;

            if (privateRep.Get().Any(x => x.Id == id))
                return false;

            throw new Exception(Settings.noConsWithId);
        }

        public IEnumerable<string> GetGalleryImagesNames(long id)
        {
            IEnumerable<long> imageIds = galleryImageRep.Get().Where(x => x.ConsultantId == id)
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
            return feedbackRep.Get().Where(x => x.ConsultantId == consultantId)
                                    .ToList()
                                    .Count;
        }
        // !!! проще предпоследнюю строку
        public long CreatePrivate(string name, 
                                  string surname, 
                                  string patronymic, 
                                  string phone, 
                                  string email)
        {
            try
            {
                privateRep.CreateAsync(new PrivateConsultant(name,
                                                             surname,
                                                             patronymic,
                                                             phone,
                                                             email,
                                                             DateTime.Now));
            }
            catch (Exception e)
            {
                throw new Exception(ServiceUtil.GetExMsg(e, Settings.createJurEx));
            }
            long id = privateRep.Get().OrderByDescending(x => x.Id).Select(x => x.Id).First();
            return id;
        }

        public void CreatePrivateImages(HttpFileCollection files, long id)
        {
            HttpPostedFile photo = files["photo"];
            if (photo != null)
            {
                CreateImage(photo, (long)FileTypes.Photo, id);
            }

            HttpPostedFile passportscan = files["passportscan"];
            if (passportscan != null)
            {
                CreateImage(passportscan, (long)FileTypes.PassportScan, id);
            }

            for (int i = 1; i <= MAX_PRIVATE_DOCS_NUM; i++)
            {
                HttpPostedFile doc = files["doc" + i];
                if (doc != null)
                {
                    CreateImage(doc, (long)FileTypes.PrivateDoc, id);
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
                juridicRep.CreateAsync(new JuridicConsultant(ltdtitle, ogrn, inn, phone, siteurl, DateTime.Now));
            }
            catch (Exception e)
            {
                throw new Exception(ServiceUtil.GetExMsg(e, Settings.createJurEx));
            }

            long id = juridicRep.Get().OrderByDescending(x => x.Id).Select(x => x.Id).First();
            return id;
        }

        public void CreateJuridicImages(HttpFileCollection files, long id)
        {
            if (files["logo"] != null)
            {
                CreateImage(files["logo"], (long)FileTypes.Logo, id);
            }
            for (int i = 1; i <= MAX_PRIVATE_DOCS_NUM; i++)
            {
                HttpPostedFile file = files["doc" + i];
                if (file != null)
                {
                    CreateImage(file, (long)FileTypes.JuridicDoc, id);
                }
            }
        }

        public void CreateImage(HttpPostedFile file, long fileTypeid, long consId)
        {
            try
            {
                imageRep.CreateAsync(new ConsultantImage(consId, 
                                                         ServiceUtil.GetBytesFromStream(file.InputStream), 
                                                         file.FileName, 
                                                         file.ContentLength, 
                                                         DateTime.Now, 
                                                         fileTypeid));
            }
            catch (Exception e)
            {
                throw new Exception(ServiceUtil.GetExMsg(e, Settings.createjurImagesEx));
            }
        }
        // !!! везде проверки на null
        public void CreateImageWhenUpdate(HttpRequest request, long consId)
        {
            foreach (FileTypes fileType in (FileTypes[])Enum.GetValues(typeof(FileTypes)))
            {
                HttpPostedFile file = request.Files[fileType.ToString()];
                if (file != null)
                {
                    try
                    {
                        imageRep.CreateAsync(new ConsultantImage(consId,
                                                         ServiceUtil.GetBytesFromStream(file.InputStream),
                                                         file.FileName,
                                                         file.ContentLength,
                                                         DateTime.Now,
                                                         (long)fileType));
                    }
                    catch (Exception e)
                    {
                        throw new Exception(ServiceUtil.GetExMsg(e, Settings.createjurImagesEx));
                    }
                }
            }
        }
        // !!! оставить просто Delete
        public async void DeleteImageAsync(long id)
        {
            ConsultantImage image = await imageRep.GetAsync(id);
            try
            {
                imageRep.DeleteAsync(image);
            }
            catch (Exception e)
            {
                throw new Exception(ServiceUtil.GetExMsg(e, Settings.createjurImagesEx));
            }
        }

        public void UpdatePrivateFields(long id, NameValueCollection formData)
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
                privateRep.UpdateAsync(private_);
            }
            catch (Exception e)
            {
                throw new Exception(ServiceUtil.GetExMsg(e, Settings.createJurEx));
            }
        }

        public void UpdateJuridicFields(long id, NameValueCollection formData)
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
                juridicRep.UpdateAsync(juridic);
            }
            catch (Exception e)
            {
                throw new Exception(ServiceUtil.GetExMsg(e, Settings.createJurEx));
            }
        }

        public async void UpdateRatingAsync(long id, decimal rating)
        {
            PrivateConsultant private_ = await privateRep.GetAsync(id);
            if (private_ != null)
            {
                private_.Rating = rating;
                try
                {
                    privateRep.UpdateAsync(private_);
                }
                catch (Exception e)
                {
                    throw new Exception(ServiceUtil.GetExMsg(e, "Не удалось обновить рейтинг физлица"));
                }
                return;
            }
            JuridicConsultant juridic = await juridicRep.GetAsync(id);
            juridic.Rating = rating;
            try
            { 
                juridicRep.UpdateAsync(juridic);
            }
            catch (Exception e)
            {
                throw new Exception(ServiceUtil.GetExMsg(e, "Не удалось обновить рейтинг юрлица"));
            }
        }
    }
}