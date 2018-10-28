using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Data.Entity;
using System.Linq;
using WebApplication1.Models;
using WebApplication1.Utils;
using WebApplication1.DAL;
using System.Threading.Tasks;

namespace WebApplication1.BLL
{
    public class OrderBLL
    {
        private readonly static Repositories reps = new Repositories();
        private readonly GenericRepository<Order> rep = reps.Orders;
        private readonly GenericRepository<ConsultationType> consTypesRep = reps.ConsultationTypes;
        private readonly GenericRepository<PrivateConsultant> privatesRep = reps.Privates;
        private readonly GenericRepository<JuridicConsultant> juridicsRep = reps.Juridics;
        private readonly GenericRepository<OrderStatus> statusRep = reps.OrderStatuses;
        private readonly GenericRepository<PaymentStatus> paymentStatusRep = reps.PaymentStatuses;

        private readonly ConsultantBLL consBLL = new ConsultantBLL();
        private readonly ServiceBLL serviceBLL = new ServiceBLL();
        // !!! конструктор
        public void Create(NameValueCollection formData)
        {
            Order order = new Order();
            order.ConsultationTypeId = ServiceUtil.GetLong(formData["consultationtypeid"]);
            order.ServiceId = ServiceUtil.GetLong(formData["serviceid"]);
            order.Comment = formData["comment"];
            order.DateTime = Convert.ToDateTime(formData["date"]);
            order.StatusCode = (int)OrderStatuses.Начат_клиентом;
            try
            {
                rep.CreateAsync(order);
            }
            catch (Exception e)
            {
                throw new Exception(ServiceUtil.GetExMsg(e, "Не удалось создать заказ"));
            }
        }

        public async void ConfirmAsync(long id)
        {
            Order order = await rep.GetAsync(id);
            order.StatusCode = (int)OrderStatuses.Принят_консультантом;
            try
            {
                rep.UpdateAsync(order);
            }
            catch (Exception e)
            {
                throw new Exception(ServiceUtil.GetExMsg(e, "Не удалось подтвердить заказ клиентом"));
            }
        }

        public async void CancelByClientAsync(long id)
        {
            Order order = await rep.GetAsync(id);
            order.StatusCode = (int)OrderStatuses.Отменён_клиентом;
            try
            {
                rep.UpdateAsync(order);
            }
            catch (Exception e)
            {
                throw new Exception(ServiceUtil.GetExMsg(e, "Не удалось отменить заказ клиентом"));
            }
        }

        public async void CancelByConsAsync(long id)
        {
            Order order = await rep.GetAsync(id);
            order.StatusCode = (int)OrderStatuses.Отменён_консультантом;
            try
            {
                rep.UpdateAsync(order);
            }
            catch (Exception e)
            {
                throw new Exception(ServiceUtil.GetExMsg(e, "Не удалось отменить заказ консультантом"));
            }
        }
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

        public async Task<OrderForClientVM> GetVMAsync(long id)
        {
            Order order = await rep.GetAsync(id);
            return new OrderForClientVM
            {
                Id = order.Id,
                Number = order.Number,
                Date = order.DateTime,
                Consultant = consBLL.GetName(order.Id),
                Service = serviceBLL.GetById(order.ServiceId).Title
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
                    Consultant = consBLL.GetName(order.Id),
                    Service = serviceBLL.GetById(order.ServiceId).Title
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
                    PaymentStatus = GetPaymentStatus(order.PaymentStatusId),
                    Service = serviceBLL.GetById(order.ServiceId).Title,
                    Date = order.DateTime,
                });
            }
            return vms;
        }

        public async Task<IEnumerable<ConsultationType>> GetConsultationTypesAsync()
        {
            return await consTypesRep.GetAsync();
        }

        public async void UpdateTimeAsync(long id, long timestamp)
        {
            Order order = await rep.GetAsync(id);
            order.DateTime = ServiceUtil.UnixTimestampToDateTime(timestamp);
            try
            {
                rep.UpdateAsync(order);
            }
            catch (Exception e)
            {
                throw new Exception(ServiceUtil.GetExMsg(e, "Не удалось изменить время заказа"));
            }
        }
        // !!! GetAsync
        private string GetStatus(long code)
        {
            return statusRep.Get().SingleOrDefault(x => x.Code == code).Text;
        }
        // !!! GetAsync
        private string GetPaymentStatus(long id)
        {
            return paymentStatusRep.Get().SingleOrDefault(x => x.Id == id).Text;
        }
    }
}