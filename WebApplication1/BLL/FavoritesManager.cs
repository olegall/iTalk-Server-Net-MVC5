using System;
using System.Collections.Generic;
using System.Linq;
using WebApplication1.Models;
using WebApplication1.Utils;
using WebApplication1.DAL;
using System.Threading.Tasks;

namespace WebApplication1.BLL
{
    public class FavoritesManager
    {
        #region Fields
        private readonly GenericRepository<Favorite> rep = Reps.Favorites;
        // !!! избавиться от папки packages
        private readonly ConsultantManager consMng = new ConsultantManager();
        private readonly ServiceManager serviceMng = new ServiceManager();
        #endregion

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
            catch (Exception e)
            {
                throw new Exception(ServiceUtil.GetExMsg(e, "Не удалось добавить консультанта в избранное"));
            }
        }



        public IEnumerable<FavoriteConsultantVM> GetVMs(long clientId)
        {
            IList<FavoriteConsultantVM> vms = new List<FavoriteConsultantVM>();
            foreach (long consId in GetFavoriteConsIds(clientId))
            {
                PrivateConsultant private_ = Reps.Privates.Get().Where(x => x.Id == consId)
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
                JuridicConsultant juridic = Reps.Juridics.Get()
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

        // !!!
        /*
         using (EmployeeContext context = new EmployeeContext())
         {
         }
        */
        /*
        private bool SaveCustomer(Customer customer)
        {
            try
            {
                using (MyContext context = new MyContext())
                {
                    context.Customers.Add(customer);
                    context.SaveChanges();
                }
                return true;
            }
            catch (DbUpdateException ex)
            {
                return false;
            }
        } 
        */
        // !!! ulong
        public async Task DeleteAsync(long clientId, long consultantId)
        {
            Favorite favorite = rep.Get().SingleOrDefault(x => x.ClientId == clientId && 
                                                          x.ConsultantId == consultantId);
            try
            {
                await rep.DeleteAsync(favorite);
            }
            catch (Exception e)
            {
                throw new Exception(ServiceUtil.GetExMsg(e, "Не удалось удалить консультанта из избранного"));
            }
        }
        #endregion
    }
}