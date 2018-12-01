using System;
using System.Linq;
using System.Threading.Tasks;
using System.Collections.Generic;
using WebApplication1.Models;
using WebApplication1.Interfaces;
using WebApplication1.Utils;

namespace WebApplication1.BLL
{
    public class ClientManager : BaseManager, IClientManager
    {
        private readonly IGenericRepository<Client> rep;
        private static readonly DataContext _db = new DataContext();

        public ClientManager(IGenericRepository<Client> rep)
        {
            this.rep = rep;
        }

        public async Task<CRUDResult<Client>> CreateAsync(string name, string phone)
        {
            CRUDResult<Client> CRUDResult = new CRUDResult<Client>();
            try
            {
                await rep.CreateAsync(new Client(name, phone));
            }
            catch
            {
                CRUDResult.Mistake = (int)CRUDResult<Client>.Mistakes.ServerOrConnectionFailed;
            }
            return CRUDResult;
        }

        public CRUDResult<Client> GetAsync(long id, bool adPush)
        {
            return TryGetEntity<Client>(rep, id);
        }

        public async Task<CRUDResult<Client>> UpdateAsync(long id, string name, bool adPush) // adPush - позже
        {
            CRUDResult<Client> result = TryGetEntity<Client>(rep, id);
            if (result.Entity == null)
            {
                result.Mistake = (int)CRUDResult<Client>.Mistakes.EntityNotFound;
                return result;
            }

            result.Entity.Name = name;
            try
            {
                await rep.UpdateAsync(result.Entity);
            }
            catch
            {
                result.Mistake = (int)CRUDResult<Client>.Mistakes.ServerOrConnectionFailed;
            }
            return result;
        }

        public async Task<CRUDResult<Client>> DeleteAsync(long id)
        {
            CRUDResult<Client> result = TryGetEntity<Client>(rep, id);
            if (result.Entity == null)
            {
                result.Mistake = (int)CRUDResult<Client>.Mistakes.EntityNotFound;
                return result;
            }

            try
            {
                await rep.DeleteAsync(result.Entity);
            }
            catch
            {
                result.Mistake = (int)CRUDResult<Client>.Mistakes.ServerOrConnectionFailed;
            }
            return result;
        }

        public IEnumerable<Client> GetAllTest() // !
        {
            //_db.Clients.Add(new Client("n1", "ph1"));
            //_db.Clients.Add(new Client("n2", "ph2"));
            //_db.SaveChanges();

            var a1 = _db.Clients.ToArray();
            try
            {
                /*Reps.Clients.Get()*/
                 //_db.Clients.Remove(_db.Clients.Find(1));
            }
            catch (Exception e) {
            }
            _db.SaveChanges();
            return rep.Get();
        }
    }
}