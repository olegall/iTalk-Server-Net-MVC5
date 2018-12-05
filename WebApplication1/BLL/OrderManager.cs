using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Data.Entity;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using WebApplication1.Models;
using WebApplication1.Utils;
using WebApplication1.ViewModels;
using WebApplication1.Interfaces;

namespace WebApplication1.BLL
{
    public class OrderManager : BaseManager, IOrderManager
    {   // !!! инициализация через конструктор - IoC. Завязка на интерфейсы в констуркторе
        #region Fields
        private readonly IGenericRepository<Order> rep;
        private readonly IGenericRepository<ConsultationType> consultationTypesRep;
        private readonly IGenericRepository<OrderStatus> orderStatusesRep;
        private readonly IGenericRepository<PaymentStatus> paymentStatusesRep;

        private readonly ConsultantManager consMng = new ConsultantManager();
        private readonly ServiceManager serviceMng = new ServiceManager();
        #endregion

        #region Ctor
        public OrderManager(IGenericRepository<Order> rep, 
                            IGenericRepository<ConsultationType> consultationTypesRep, 
                            IGenericRepository<OrderStatus> orderStatusesRep, 
                            IGenericRepository<PaymentStatus> paymentStatusesRep)
        {
            this.rep = rep;
            this.consultationTypesRep = consultationTypesRep;
            this.orderStatusesRep = orderStatusesRep;
            this.paymentStatusesRep = paymentStatusesRep;
        }
        #endregion

        #region Public methods
        public async Task<CRUDResult<Order>> CreateAsync(NameValueCollection formData)
        {
            CRUDResult<Order> CRUDResult = new CRUDResult<Order>();
            try
            {
                await rep.CreateAsync(new Order(ServiceUtil.GetLong(formData["consultationtypeid"]), 
                                                ServiceUtil.GetLong(formData["serviceid"]), 
                                                formData["comment"],
                                                Convert.ToDateTime(formData["date"]),
                                                (int)OrderStatuses.Начат_клиентом));
            }
            catch
            {
                CRUDResult.Mistake = (int)CRUDResult<Order>.Mistakes.ServerOrConnectionFailed;
            }
            return CRUDResult;
        }

        public async Task<CRUDResult<Order>> ConfirmAsync(long id)
        {
            CRUDResult<Order> result = TryGetEntity<Order>(rep, id);
            if (result.Entity == null)
            {
                result.Mistake = (int)CRUDResult<Order>.Mistakes.EntityNotFound;
                return result;
            }

            result.Entity.StatusCode = (int)OrderStatuses.Принят_консультантом;
            try
            {
                await rep.UpdateAsync(result.Entity);
            }
            catch
            {
                result.Mistake = (int)CRUDResult<Client>.Mistakes.ServerOrConnectionFailed;
            }
            return result;
        }

        public async Task<CRUDResult<Order>> CancelByClientAsync(long id)
        {
            CRUDResult<Order> result = TryGetEntity<Order>(rep, id);
            if (result.Entity == null)
            {
                result.Mistake = (int)CRUDResult<Client>.Mistakes.EntityNotFound;
                return result;
            }

            result.Entity.StatusCode = (int)OrderStatuses.Отменён_клиентом;
            try
            {
                await rep.UpdateAsync(result.Entity);
            }
            catch
            {
                result.Mistake = (int)CRUDResult<Client>.Mistakes.ServerOrConnectionFailed;
            }
            return result;
        }

        public async Task<CRUDResult<Order>> CancelByConsAsync(long id)
        {
            CRUDResult<Order> result = TryGetEntity<Order>(rep, id);
            if (result.Entity == null)
            {
                result.Mistake = (int)CRUDResult<Client>.Mistakes.EntityNotFound;
                return result;
            }

            result.Entity.StatusCode = (int)OrderStatuses.Отменён_консультантом;
            try
            {
                await rep.UpdateAsync(result.Entity);
            }
            catch
            {
                result.Mistake = (int)CRUDResult<Client>.Mistakes.ServerOrConnectionFailed;
            }
            return result;
        }


        // !!! все id ulong или проверка на отриц id
        public async Task<OrderForClientVM> GetVMAsync(long id) // ! обработать
        {
            Order order = rep.GetAsync(id);
            return new OrderForClientVM
            {
                Id = order.Id,
                Number = order.Number,
                Date = order.DateTime,
                Consultant = consMng.GetName(order.Id),
                Service = serviceMng.GetById(order.ServiceId).Title
            };
        }

        public IEnumerable<OrderForClientVM> GetClientVMs(long clientId) // ! обработать
        {
            IList<OrderForClientVM> vms = new List<OrderForClientVM>();
            foreach (Order order in GetByClientId(clientId))
            {
                vms.Add(new OrderForClientVM
                {
                    Id = order.Id,
                    Number = order.Number,
                    Date = order.DateTime,
                    Consultant = consMng.GetName(order.Id),
                    Service = serviceMng.GetById(order.ServiceId).Title
                });
            }
            return vms;
        }

        public IEnumerable<OrderVM> GetVMs(int offset, int limit)
        {
            IList<OrderVM> vms = new List<OrderVM>();
            foreach (Order order in GetRangeWithOffset(offset, limit))
            {
                vms.Add(new OrderVM
                {
                    Status = GetStatus(order.StatusCode),
                    StatusCode = order.StatusCode,
                    ServiceId = order.ServiceId,
                    Image = String.Empty
                });
            }
            return vms;
        }

        public IEnumerable<OrderForConsVM> GetConsVMs(long consId)
        {
            IList<OrderForConsVM> vms = new List<OrderForConsVM>();
            foreach (Order order in GetByConsId(consId))
            {
                vms.Add(new OrderForConsVM
                {
                    Id = order.Id,
                    Status = GetStatus(order.StatusCode),
                    PaymentStatus = GetPaymentStatusAsync(order.PaymentStatusId),
                    Service = serviceMng.GetById(order.ServiceId).Title,
                    Date = order.DateTime,
                });
            }
            return vms;
        }

        public IEnumerable<ConsultationType> GetConsultationTypes() // ! обработать
        {
            return consultationTypesRep.Get();
        }

        public async Task<CRUDResult<Order>> UpdateTimeAsync(long id, long timestamp)
        {
            CRUDResult<Order> result = TryGetEntity<Order>(rep, id);
            if (result.Entity == null)
            {
                result.Mistake = (int)CRUDResult<Order>.Mistakes.EntityNotFound;
                return result;
            }

            result.Entity.DateTime = ServiceUtil.UnixTimestampToDateTime(timestamp);
            try
            {
                await rep.UpdateAsync(result.Entity);
            }
            catch
            {
                result.Mistake = (int)CRUDResult<Client>.Mistakes.ServerOrConnectionFailed;
            }
            return result;
        }
        #endregion

        #region Private methods
        // !!! GetAsync
        private IEnumerable<Order> GetByClientId(long id)
        {
            return rep.Get().Where(x => x.ClientId == id);
        }
        // !!! GetAsync
        private IEnumerable<Order> GetByConsId(long id)
        {
            return rep.Get().Where(x => x.ConsultantId == id);
        }
        // !!! GetPagingAsync
        private IEnumerable<Order> GetRangeWithOffset(int offset, int limit)
        {
            return rep.Get().Skip(offset).Take(limit);
        }
        // !!! GetAsync
        private string GetStatus(long code)
        {
            return orderStatusesRep.Get().SingleOrDefault(x => x.Code == code).Text;
        }
        // !!! await, async Task
        private string GetPaymentStatusAsync(long id)
        {
            return paymentStatusesRep.GetAsync(id).Text;
        }
        #endregion
    }
}