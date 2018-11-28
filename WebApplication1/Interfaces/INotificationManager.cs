using WebApplication1.Models;
using System.Collections.Generic;

namespace WebApplication1.Interfaces
{
    public interface INotificationManager
    {
        IEnumerable<NotificationVM> GetVMs(long consId);
    }
}