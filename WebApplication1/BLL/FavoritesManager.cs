using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Threading.Tasks;
using WebApplication1.Models;
using WebApplication1.ViewModels;
using WebApplication1.Interfaces;

namespace WebApplication1.BLL
{
    public class FavoritesManager : IFavoritesManager
    {
        #region Fields
        private readonly IGenericRepository<Favorite> rep;
        private readonly IGenericRepository<PrivateConsultant> privatesRep;
        private readonly IGenericRepository<JuridicConsultant> juridicsRep;
        private readonly ConsultantManager consMng = new ConsultantManager();
        private readonly ServiceManager serviceMng = new ServiceManager();
        #endregion

        #region Ctor
        public FavoritesManager(IGenericRepository<Favorite> rep, 
                                IGenericRepository<PrivateConsultant> privatesRep,
                                IGenericRepository<JuridicConsultant> juridicsRep)
        {
            this.rep = rep;
            this.privatesRep = privatesRep;
            this.juridicsRep = juridicsRep;
        }
        #endregion
        // !!! избавиться от папки packages

        // ! join
        private IEnumerable<long> GetFavoriteConsIds(long clientId)
        {
            return rep.Get().Where(x => x.ClientId == clientId)
                            .Select(x => x.ConsultantId);
        }

        #region Public methods
        public async Task CreateAsync(long clientId, long consultantId)
        {
            try
            {
                await rep.CreateAsync(new Favorite(clientId, consultantId));
            }
            catch
            {
                throw new HttpException(500, "Не удалось добавить консультанта в избранное");
            }
        }

        public IEnumerable<FavoriteConsultantVM> GetVMs(long clientId)
        {
            IList<FavoriteConsultantVM> vms = new List<FavoriteConsultantVM>();
            foreach (long consId in GetFavoriteConsIds(clientId))
            {
                PrivateConsultant private_ = privatesRep.Get().Where(x => x.Id == consId)
                                                                .SingleOrDefault();
                if (private_ != null)
                {
                    vms.Add(new FavoriteConsultantVM
                    {
                        Id = private_.Id,
                        Name = private_.Name,
                        Rating = private_.Rating,
                        FeedbacksCount = consMng.GetFeedbacksCount(private_.Id),
                        Services = serviceMng.GetVM(private_)
                    });
                }
                JuridicConsultant juridic = juridicsRep.Get()
                                                       .Where(x => x.Id == consId)
                                                       .SingleOrDefault();
                if (juridic != null)
                {
                    vms.Add(new FavoriteConsultantVM
                    {
                        Id = juridic.Id,
                        Name = juridic.LTDTitle,
                        Rating = juridic.Rating,
                        FeedbacksCount = consMng.GetFeedbacksCount(juridic.Id),
                        Services = serviceMng.GetVM(juridic)
                    });
                }
            }
            return vms;
        }

        public async Task DeleteAsync(long clientId, long consultantId)
        {
            Favorite favorite = rep.Get().SingleOrDefault(x => x.ClientId == clientId && 
                                                          x.ConsultantId == consultantId);
            try
            {
                await rep.DeleteAsync(favorite);
            }
            catch
            {
                throw new HttpException(500, "Не удалось удалить консультанта из избранного");
            }
        }
        #endregion
    }
}