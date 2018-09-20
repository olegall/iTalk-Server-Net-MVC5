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

namespace WebApplication1.BLL
{
    public class ConsultantBLL
    {
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

        #region Constants
        private const int MAX_PRIVATE_DOCS_NUM = 5;
        private const int MAX_JURIDIC_DOCS_NUM = 5;
        private const byte BUSY = 0x00000001;
        private const byte FREE = 0x00000010;
        #endregion

        SearchBLL searchBLL = new SearchBLL();

        public PrivateConsultant Get(string phone)
        {
            return privateRep.Get().SingleOrDefault(x => x.Phone == phone);
        }

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

        public PrivateConsultant GetPrivate(long id)
        {
            return privateRep.Get().SingleOrDefault(x => x.Id == id);
        }

        public JuridicConsultant GetJuridic(long id)
        {
            return juridicRep.Get().SingleOrDefault(x => x.Id == id);
        }

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
            IEnumerable<long> ids = serviceRep.Get().Where(x => x.SubcategoryId == subcategoryId)
                                                    .Select(x => x.ConsultantId)
                                                    .Distinct()
                                                    .ToArray();
            IEnumerable<PrivateConsultant> privates = GetPrivatesBySubcategory(subcategoryId);
            privates = privates.Skip(offset)
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
            IEnumerable<long> ids = serviceRep.Get().Where(x => x.SubcategoryId == subcategoryId)
                                                    .Select(x => x.ConsultantId)
                                                    .Distinct()
                                                    .ToArray();
            IEnumerable<JuridicConsultant> juridics = GetJuridicsBySubcategory(subcategoryId);
            juridics = juridics.Skip(offset).Take(limit).ToArray();
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

        public PrivateConsultant GetPrivateByPhone(string phone)
        {
            return privateRep.Get().SingleOrDefault(x => x.Phone == phone);
        }

        public JuridicConsultant GetJuridicByPhone(string phone)
        {
            return juridicRep.Get().SingleOrDefault(x => x.Phone == phone);
        }

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
                Favorite = isFavorite(private_.Id),
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
                Favorite = isFavorite(juridic.Id),
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

        public IEnumerable<PrivateConsultantVM> GetPrivatesVMs(int offset, 
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

        public bool isFavorite(long id)
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

        public long CreatePrivate(string name, 
                                  string surname, 
                                  string patronymic, 
                                  string phone, 
                                  string email)
        {
            PrivateConsultant private_ = new PrivateConsultant();
            private_.Name = name;
            private_.Surname = surname;
            private_.Patronymic = patronymic;
            private_.Phone = phone;
            private_.Email = email;
            private_.FreeDate = DateTime.Now;
            try
            {
                privateRep.Create(private_);
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
            JuridicConsultant juridic = new JuridicConsultant();
            juridic.LTDTitle = ltdtitle;
            juridic.OGRN = ogrn;
            juridic.INN = inn;
            juridic.Phone = phone;
            juridic.SiteUrl = siteurl;
            juridic.FreeDate = DateTime.Now;
            try
            {
                juridicRep.Create(juridic);
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
            ConsultantImage image = new ConsultantImage();
            image.ConsultantId = consId;
            image.Bytes = ServiceUtil.GetBytesFromStream(file.InputStream);
            image.FileName = file.FileName;
            image.Size = file.ContentLength;
            image.Date = DateTime.Now;
            image.FileTypeId = fileTypeid;
            try
            {
                imageRep.Create(image);
            }
            catch (Exception e)
            {
                throw new Exception(ServiceUtil.GetExMsg(e, Settings.createjurImagesEx));
            }
        }

        public void CreateImageWhenUpdate(HttpRequest request, long consId)
        {
            foreach (FileTypes fileType in (FileTypes[])Enum.GetValues(typeof(FileTypes)))
            {
                HttpPostedFile file = request.Files[fileType.ToString()];
                if (file != null)
                {
                    ConsultantImage image = new ConsultantImage();
                    image.ConsultantId = consId;
                    image.Bytes = ServiceUtil.GetBytesFromStream(file.InputStream);
                    image.FileName = file.FileName;
                    image.Size = file.ContentLength;
                    image.Date = DateTime.Now;
                    image.FileTypeId = (long)fileType;
                    try
                    {
                        imageRep.Create(image);
                    }
                    catch (Exception e)
                    {
                        throw new Exception(ServiceUtil.GetExMsg(e, Settings.createjurImagesEx));
                    }
                }
            }
        }

        public void DeleteImage(long id)
        {
            ConsultantImage image = imageRep.Get().SingleOrDefault(x => x.Id == id);
            try
            {
                imageRep.Delete(image);
            }
            catch (Exception e)
            {
                throw new Exception(ServiceUtil.GetExMsg(e, Settings.createjurImagesEx));
            }
        }

        public void UpdatePrivateFields(long id, NameValueCollection formData)
        {
            PrivateConsultant private_ = new PrivateConsultant();
            private_.Id = id;
            private_.Name = formData["name"];
            private_.Surname = formData["surname"];
            private_.Patronymic = formData["patronymic"];
            private_.Phone = formData["phone"];
            private_.Email = formData["email"];
            private_.FreeDate = Convert.ToDateTime(formData["freedate"]);
            try
            {
                privateRep.Update(private_);
            }
            catch (Exception e)
            {
                throw new Exception(ServiceUtil.GetExMsg(e, Settings.createJurEx));
            }
        }

        public void UpdateJuridicFields(long id, NameValueCollection formData)
        {
            JuridicConsultant juridic = new JuridicConsultant();
            juridic.Id = id;
            juridic.LTDTitle = formData["ltdtitle"];
            juridic.OGRN = formData["ogrn"];
            juridic.INN = formData["inn"];
            juridic.Phone = formData["phone"];
            juridic.SiteUrl = formData["siteurl"];
            juridic.FreeDate = Convert.ToDateTime(formData["freedate"]);
            try
            {
                juridicRep.Update(juridic);
            }
            catch (Exception e)
            {
                throw new Exception(ServiceUtil.GetExMsg(e, Settings.createJurEx));
            }
        }

        public void UpdateRating(int id, decimal rating)
        {
            PrivateConsultant private_ = privateRep.Get().SingleOrDefault(x => x.Id == id);
            if (private_ != null)
            {
                private_.Rating = rating;
                try
                {
                    privateRep.Update(private_);
                }
                catch (Exception e)
                {
                    throw new Exception(ServiceUtil.GetExMsg(e, "Не удалось обновить рейтинг физлица"));
                }
                return;
            }
            JuridicConsultant juridic = juridicRep.Get().SingleOrDefault(x => x.Id == id);
            juridic.Rating = rating;
            try
            { 
                juridicRep.Update(juridic);
            }
            catch (Exception e)
            {
                throw new Exception(ServiceUtil.GetExMsg(e, "Не удалось обновить рейтинг юрлица"));
            }
        }
    }
}