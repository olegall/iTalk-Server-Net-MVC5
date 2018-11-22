using System.Collections.Generic;
using System;
using System.Collections.Specialized;
using System.Threading.Tasks;
using WebApplication1.ViewModels;

namespace WebApplication1.Interfaces
{
    public interface IFavoritesManager
    {
        Task CreateAsync(long clientId, long consultantId);
        IEnumerable<FavoriteConsultantVM> GetVMs(long clientId);
        Task DeleteAsync(long clientId, long consultantId);
    }
}