using System.Collections.Generic;
using System.Linq;
using WebApplication1.Models;
using WebApplication1.Interfaces;

namespace WebApplication1.BLL
{
    public class NotificationManager : INotificationManager
    {
        private readonly string DATE_TIME_FORMAT = "dd.MM.yyyy HH:mm";
        private readonly IGenericRepository<Order> ordersRep;

        public NotificationManager(IGenericRepository<Order> ordersRep)
        {
            this.ordersRep = ordersRep;
        }

        private IEnumerable<Order> GetStartedOrdersByConsId(long consId)
        {
            return ordersRep.Get().Where(x => x.ConsultantId == consId &&
                                              x.StatusCode == (long)OrderStatuses.Начат_клиентом);
        }

        public IEnumerable<NotificationVM> GetVMs(long consId)
        {
            IList<NotificationVM> vms = new List<NotificationVM>();
            foreach (Order order in GetStartedOrdersByConsId(consId))
            {
                vms.Add(new NotificationVM
                {
                    Description = order.RequestDescription,
                    DateTime = order.DateTime.ToString(DATE_TIME_FORMAT)
                });
            }
            return vms;
        }

        // !!! везде где можно IQueryable
    }
}