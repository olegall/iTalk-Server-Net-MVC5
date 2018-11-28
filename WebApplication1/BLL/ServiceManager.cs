using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Linq;
using System.Web;
using System.Threading.Tasks;
using Newtonsoft.Json;
using WebApplication1.Models;
using WebApplication1.Utils;
using WebApplication1.Models.ServiceJSON;
using WebApplication1.ViewModels;

namespace WebApplication1.BLL
{
    public class ServiceManager
    {
        #region Fields
        private readonly ConsultantManager consMng = new ConsultantManager();
        private readonly IGenericRepository<Service> rep;
        private readonly IGenericRepository<ServiceImage> serviceImagesRep;
        private readonly IGenericRepository<Category> categoriesRep;
        private readonly IGenericRepository<Subcategory> subcategoriesRep;
        #endregion

        #region ctor
        public ServiceManager()
        {
        }

        public ServiceManager(IGenericRepository<Service> rep, 
                              IGenericRepository<ServiceImage> serviceImagesRep,
                              IGenericRepository<Category> categoriesRep,
                              IGenericRepository<Subcategory> subcategoriesRep)
        {
            this.rep = rep;
            this.serviceImagesRep = serviceImagesRep;
            this.categoriesRep = categoriesRep;
            this.subcategoriesRep = subcategoriesRep;
        }
        #endregion

        #region Private methods
        private Service GetInstance(long consultantId,
                                    string category,
                                    string subcategory,
                                    string title,
                                    string description,
                                    decimal cost,
                                    int duration,
                                    bool available,
                                    int availablePeriod)
        {
            long categoryId, subcategoryId;
            bool consChoseCat = Int64.TryParse(category, out categoryId);
            bool consChoseSubcat = Int64.TryParse(subcategory, out subcategoryId);

            Service service = new Service();
            service.ConsultantId = consultantId;
            if (consChoseCat && consChoseSubcat)
            {
                service.CategoryId = categoryId;
                service.SubcategoryId = subcategoryId;
            }
            if (consChoseCat && !consChoseSubcat)
            {
                service.CategoryId = categoryId;
                service.SubcategoryId = 0;
            }
            if (!consChoseCat && !consChoseSubcat)
            {
                service.CategoryId = 0;
                service.SubcategoryId = 0;
            }
            service.CategoryId = categoryId;
            service.SubcategoryId = subcategoryId;
            service.Title = title;
            service.Description = description;
            service.Cost = cost;
            service.Duration = duration;
            service.Available = available;
            service.AvailablePeriod = availablePeriod;
            service.ModerationStatusId = 1;
            return service;
        }

        private ServiceImage GetImage(HttpPostedFile file)
        {
            return new ServiceImage(ServiceUtil.GetBytesFromStream(file.InputStream),
                                    file.FileName,
                                    file.ContentLength,
                                    DateTime.Now);
        }
        #endregion

        #region Public methods
        // !!! async
        public IEnumerable<Service> Get(long consId)
        {
            return rep.Get().Where(x => x.ConsultantId == consId).ToArray();
        }

        public Service GetById(long id)
        {
            return rep.GetAsync(id);
        }

        public IEnumerable<Service> GetAll()
        {
            return rep.Get();
        }

        public IEnumerable<ServiceVM> GetVM(Consultant cons)
        {
            IList<ServiceVM> vm = new List<ServiceVM>();
            IEnumerable<Service> services = Get(cons.Id);
            foreach (Service service in services)
            {
                vm.Add(new ServiceVM
                       {
                            Title = service.Title,
                            Description = service.Description,
                            Category = GetCategory(service),
                            Subcategory = GetSubcategory(service),
                            Cost = service.Cost,
                            Duration = service.Duration
                       });
            }
            return vm;
        }

        public IEnumerable<ServiceVM> GetVMs()
        {
            IList<ServiceVM> vms = new List<ServiceVM>();
            foreach (Service service in GetAll())
            {
                vms.Add(new ServiceVM
                {
                    Title = service.Title,
                    Description = service.Description,
                    Category = GetCategory(service),
                    Subcategory = GetSubcategory(service),
                    Cost = service.Cost,
                    Duration = service.Duration
                });
            }
            return vms;
        }

        public IEnumerable<ServiceVM> GetVMs(long consId)
        {
            IList<ServiceVM> vm = new List<ServiceVM>();
            IEnumerable<Service> services = Get(consId);
            foreach (Service service in services)
            {
                vm.Add(new ServiceVM
                {
                    Title = service.Title,
                    Description = service.Description,
                    Category = GetCategory(service),
                    Subcategory = GetSubcategory(service),
                    Cost = service.Cost,
                    Duration = service.Duration
                });
            }
            return vm;
        }

        public async Task CreateAsync(NameValueCollection formData)
        {
            try
            {
                await rep.CreateAsync(GetInstance(ServiceUtil.GetLong(formData["consid"]),
                                                  formData["category"],
                                                  formData["subcategory"],
                                                  formData["title"],
                                                  formData["description"],
                                                  Convert.ToDecimal(formData["cost"]),
                                                  Convert.ToInt16(formData["duration"]),
                                                  Convert.ToBoolean(formData["available"]),
                                                  Convert.ToInt16(formData["availablePeriod"])));
            }
            catch (Exception e)
            {
                throw new HttpException(500, "Не получилось добавить услугу");
            }
        }

        public async Task CreateManyAsync(NameValueCollection formData)
        {
            ServiceJSON serviceJSON = JsonConvert.DeserializeObject<ServiceJSON>(formData["services"]);
            foreach (ServiceParams param in serviceJSON.Params)
            {
                Service service = GetInstance(serviceJSON.ConsId, 
                                              param.Category, 
                                              param.Subcategory,
                                              param.Title,
                                              param.Description,
                                              param.Cost,
                                              param.Duration,
                                              param.Available,
                                              param.AvailablePeriod);
                await rep.CreateAsync(service);
            }
        }

        public async Task UpdateAsync(NameValueCollection formData)
        {
            Service service = GetInstance(ServiceUtil.GetLong(formData["consid"]), 
                                          formData["category"],
                                          formData["subcategory"],
                                          formData["title"],
                                          formData["description"],
                                          Convert.ToDecimal(formData["cost"]),
                                          Convert.ToInt16(formData["duration"]),
                                          Convert.ToBoolean(formData["available"]),
                                          Convert.ToInt16(formData["availablePeriod"]));
            service.Id = ServiceUtil.GetLong(formData["id"]);
            try
            {
                await rep.UpdateAsync(service);
            }
            catch (Exception e)
            {
                throw new HttpException(500, "Не получилось редактировать услугу");
            }
        }

        public async void HideAsync(long id)
        {
            Service service = rep.GetAsync(id);
            service.Deleted = true;
            try
            {
                await rep.UpdateAsync(service);
            }
            catch
            {
                throw new HttpException(500, "Не получилось скрыть услугу");
            }
        }

        public async Task CreateImageAsync(HttpRequest request)
        {
            try
            {
                ServiceImage img = GetImage(request.Files["image"]);
                img.ServiceId =  ServiceUtil.GetLong(request.Form["id"]);
                await serviceImagesRep.CreateAsync(img);
            }
            catch (Exception e)
            {
                throw new HttpException(500, "Не удалось создать изображение для услуги");
            }
        }

        // !!! передавать ServiceId
        public string GetCategory(Service service)
        {
            return categoriesRep.Get().Where(x => x.Id == service.CategoryId)
                                        .Select(x => x.Title)
                                        .SingleOrDefault();
        }
        // !!! SingleOrDefault или FirstOrDefault
        // !!! дублирование кода
        // !!! передавать ServiceId
        public string GetSubcategory(Service service)
        {
            return subcategoriesRep.Get().Where(x => x.Id == service.SubcategoryId)
                                     .Select(x => x.Title)
                                     .SingleOrDefault();
        }
        #endregion
    }
}