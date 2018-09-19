using System.Collections.Generic;
using System.Linq;
using WebApplication1.Models;
using WebApplication1.DAL;

namespace WebApplication1.BLL
{
    public class NotificationBLL
    {
        private readonly static Repositories reps = new Repositories();
        private readonly DataContext _db = new DataContext();
        private readonly GenericRepository<Order> rep = reps.Orders;
        private const string DATE_TIME_FORMAT = "dd.MM.yyyy HH:mm";

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

        private IEnumerable<Order> GetStartedOrdersByConsId(long consId)
        {
            return rep.Get().Where(x => x.ConsultantId == consId && 
                                        x.StatusCode == (long)OrderStatuses.Начат_клиентом);
        }
    }
}