using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Data.Entity;
using System.Linq;
using WebApplication1.Models;
using WebApplication1.Utils;
using System.Threading.Tasks;
using WebApplication1.ViewModels;

namespace WebApplication1.BLL
{
    public class OrderManager
    {   // !!! инициализация через конструктор - IoC. Завязка на интерфейсы в констуркторе
        #region Fields
        private readonly IGenericRepository<Order> rep;
        private readonly IGenericRepository<ConsultationType> consultationTypesRep;
        private readonly IGenericRepository<OrderStatus> orderStatusesRep;
        private readonly IGenericRepository<PaymentStatus> paymentStatusesRep;

        private readonly ConsultantManager consMng = new ConsultantManager();
        private readonly ServiceManager serviceMng = new ServiceManager();
        #endregion

        #region ctor
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

        #region Public methods
        public async Task CreateAsync(NameValueCollection formData)
        {
            try
            {
                await rep.CreateAsync(new Order(ServiceUtil.GetLong(formData["consultationtypeid"]), 
                                                ServiceUtil.GetLong(formData["serviceid"]), 
                                                formData["comment"],
                                                Convert.ToDateTime(formData["date"]),
                                                (int)OrderStatuses.Начат_клиентом));
            }
            catch (Exception e)
            {
                throw new Exception(ServiceUtil.GetExMsg(e, "Не удалось создать заказ"));
            }
        }

        public async Task ConfirmAsync(long id)
        {
            Order order = rep.GetAsync(id);
            order.StatusCode = (int)OrderStatuses.Принят_консультантом;
            try
            {
                await rep.UpdateAsync(order);
            }
            catch (Exception e)
            {
                throw new Exception(ServiceUtil.GetExMsg(e, "Не удалось подтвердить заказ клиентом"));
            }
        }

        public async Task CancelByClientAsync(long id)
        {
            Order order = rep.GetAsync(id);
            order.StatusCode = (int)OrderStatuses.Отменён_клиентом;
            try
            {
                await rep.UpdateAsync(order);
            }
            catch (Exception e)
            {
                throw new Exception(ServiceUtil.GetExMsg(e, "Не удалось отменить заказ клиентом"));
            }
        }

        public async Task CancelByConsAsync(long id)
        {
            Order order = rep.GetAsync(id);
            order.StatusCode = (int)OrderStatuses.Отменён_консультантом;
            try
            {
                await rep.UpdateAsync(order);
            }
            catch (Exception e)
            {
                throw new Exception(ServiceUtil.GetExMsg(e, "Не удалось отменить заказ консультантом"));
            }
        }

        // !!! все id ulong
        public async Task<OrderForClientVM> GetVMAsync(long id)
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

        public IEnumerable<OrderForClientVM> GetClientVMs(long clientId)
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

        public IEnumerable<ConsultationType> GetConsultationTypes()
        {
            return consultationTypesRep.Get();
        }

        public async Task UpdateTimeAsync(long id, long timestamp)
        {
            Order order = rep.GetAsync(id);
            order.DateTime = ServiceUtil.UnixTimestampToDateTime(timestamp);
            try
            {
                await rep.UpdateAsync(order);
            }
            catch (Exception e)
            {
                throw new Exception(ServiceUtil.GetExMsg(e, "Не удалось изменить время заказа"));
            }
        }
        #endregion
    }
}