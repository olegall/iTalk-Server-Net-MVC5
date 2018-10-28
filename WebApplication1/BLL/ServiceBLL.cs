using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Linq;
using System.Web;
using WebApplication1.Models;
using WebApplication1.Utils;
using Newtonsoft.Json;
using WebApplication1.Models.ServiceJSON;
using System.Threading.Tasks;

namespace WebApplication1.BLL
{
    public class ServiceBLL
    {
        // !!! везде regions
        private readonly DataContext _db = new DataContext();
        private readonly ConsultantBLL consMng = new ConsultantBLL();
        private readonly GenericRepository<Service> rep;
        private readonly GenericRepository<ServiceImage> imageRep;
        // !!! инициализировать прямо или в конструкторе?
        public ServiceBLL()
        {
            rep = new GenericRepository<Service>(_db);
            imageRep = new GenericRepository<ServiceImage>(_db);
        }
        // !!! async
        public IEnumerable<Service> Get(long consId)
        {
            return rep.Get().Where(x => x.ConsultantId == consId).ToArray();
        }

        public Service GetById(long id)
        {
            return rep.Get().SingleOrDefault(x => x.Id == id);
        }

        public async Task<IEnumerable<Service>> GetAll()
        {
            return await rep.GetAsync();
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

        public async Task<IEnumerable<ServiceVM>> GetVMsAsync()
        {
            IList<ServiceVM> vms = new List<ServiceVM>();
            foreach (Service service in await GetAll())
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

        public void Create(NameValueCollection formData)
        {
            try
            {
                rep.CreateAsync(GetInstance(ServiceUtil.GetLong(formData["consid"]),
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
                throw new Exception(ServiceUtil.GetExMsg(e, "Не получилось добавить услугу"));
            }
        }

        public void CreateMany(NameValueCollection formData)
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
                rep.CreateAsync(service);
            }
        }

        public void Update(NameValueCollection formData)
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
                rep.UpdateAsync(service);
            }
            catch (Exception e)
            {
                throw new Exception(ServiceUtil.GetExMsg(e, "Не получилось редактировать услугу"));
            }
        }

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

        public void Hide(long id)
        {
            Service service = rep.Get().SingleOrDefault(x => x.Id == id);
            service.Deleted = true;
            try
            {
                rep.UpdateAsync(service);
            }
            catch (Exception e)
            {
                throw new Exception(ServiceUtil.GetExMsg(e, "Не получилось скрыть услугу"));
            }
        }

        public void CreateImage(HttpRequest request)
        {
            try
            {
                ServiceImage img = GetImage(request.Files["image"]);
                img.ServiceId =  ServiceUtil.GetLong(request.Form["id"]);
                imageRep.CreateAsync(img);
            }
            catch (Exception e)
            {
                throw new Exception(ServiceUtil.GetExMsg(e, "Не удалось создать изображение для услуги"));
            }
        }

        private ServiceImage GetImage(HttpPostedFile file) {
            return new ServiceImage(ServiceUtil.GetBytesFromStream(file.InputStream), 
                                    file.FileName, 
                                    file.ContentLength, 
                                    DateTime.Now);
        }

        public string GetCategory(Service service)
        {
            return _db.Categories.Where(x => x.Id == service.CategoryId)
                                 .Select(x => x.Title)
                                 .SingleOrDefault();
        }
        // !!! SingleOrDefault или FirstOrDefault
        // !!! дублирование кода
        public string GetSubcategory(Service service)
        {
            return _db.Subcategories.Where(x => x.Id == service.SubcategoryId)
                                    .Select(x => x.Title)
                                    .SingleOrDefault();
        }
    }
}