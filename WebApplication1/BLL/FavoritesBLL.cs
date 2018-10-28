using System;
using System.Collections.Generic;
using System.Linq;
using WebApplication1.Models;
using WebApplication1.Utils;
using WebApplication1.DAL;

namespace WebApplication1.BLL
{
    public class FavoritesBLL
    {   // !!! избавиться от полотна
        private readonly DataContext _db = new DataContext();
        private readonly static Repositories reps = new Repositories();
        private readonly GenericRepository<Favorite> rep = reps.Favorites;
        private readonly GenericRepository<PrivateConsultant> privateRep = reps.Privates;
        private readonly GenericRepository<JuridicConsultant> juridicRep = reps.Juridics;

        private readonly ConsultantBLL consBLL = new ConsultantBLL();
        private readonly ServiceBLL serviceBLL = new ServiceBLL();
        // !!! через конструктор
        public void Create(long clientId, long consultantId)
        {
            Favorite favorite = new Favorite();
            favorite.ClientId = clientId;
            favorite.ConsultantId = consultantId;
            try
            {
                rep.CreateAsync(favorite);
            }
            catch (Exception e)
            {
                throw new Exception(ServiceUtil.GetExMsg(e, "Не удалось добавить консультанта в избранное"));
            }
        }

        private IEnumerable<long> GetFavoriteConsIds(long clientId)
        {
            return rep.Get().Where(x => x.ClientId == clientId)
                            .Select(x => x.ConsultantId);
        }

        public IEnumerable<FavoriteConsultantVM> GetVMs(long clientId)
        {
            IList<FavoriteConsultantVM> vms = new List<FavoriteConsultantVM>();
            foreach (long consId in GetFavoriteConsIds(clientId))
            {
                PrivateConsultant private_ = privateRep.Get().Where(x => x.Id == consId)
                                                             .SingleOrDefault();
                if (private_ != null)
                {
                    vms.Add(new FavoriteConsultantVM
                    {
                        Id = private_.Id,
                        Name = private_.Name,
                        Rating = private_.Rating,
                        FeedbacksCount = consBLL.GetFeedbacksCount(private_.Id),
                        Services = serviceBLL.GetVM(private_)
                    });
                }
                JuridicConsultant juridic = juridicRep.Get()
                                                      .Where(x => x.Id == consId)
                                                      .SingleOrDefault();
                if (juridic != null)
                {
                    vms.Add(new FavoriteConsultantVM
                    {
                        Id = juridic.Id,
                        Name = juridic.LTDTitle,
                        Rating = juridic.Rating,
                        FeedbacksCount = consBLL.GetFeedbacksCount(juridic.Id),
                        Services = serviceBLL.GetVM(juridic)
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


        public void Delete(long clientId, long consultantId)
        {
            Favorite favorite = _db.Favorites.SingleOrDefault(x => x.ClientId == clientId && x.ConsultantId == consultantId);
            try
            {
                _db.Favorites.Remove(favorite);
                _db.SaveChanges();
            }
            catch (Exception e)
            {
                throw new Exception(ServiceUtil.GetExMsg(e, "Не удалось удалить консультанта из избранного"));
            }
        }
    }
}