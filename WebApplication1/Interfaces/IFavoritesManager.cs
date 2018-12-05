using System.Collections.Generic;
using System.Threading.Tasks;
using WebApplication1.ViewModels;
using WebApplication1.Utils;
using WebApplication1.Models;

namespace WebApplication1.Interfaces
{
    public interface IFavoritesManager
    {
        Task<CRUDResult<Favorite>> CreateAsync(long clientId, long consultantId);
        IEnumerable<FavoriteConsultantVM> GetVMs(long clientId);
        Task<CRUDResult<Favorite>> DeleteAsync(long clientId, long consultantId);
    }
}