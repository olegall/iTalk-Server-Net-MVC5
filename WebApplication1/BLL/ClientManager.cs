using System;
using System.Linq;
using System.Threading.Tasks;
using System.Collections.Generic;
using WebApplication1.Models;
using WebApplication1.Interfaces;
using WebApplication1.Utils;

namespace WebApplication1.BLL
{
    public class ClientManager : IClientManager
    {
        private readonly IGenericRepository<Client> rep;
        private static readonly DataContext _db = new DataContext();

        public ClientManager(IGenericRepository<Client> rep)
        {
            this.rep = rep;
        }

        public async Task<CRUD.Result> CreateAsync(string name, string phone)
        {
            try
            {
                await rep.CreateAsync(new Client(name, phone));
            }
            catch
            {
                return CRUD.Result.ServerOrConnectionFailed;
            }
            return CRUD.Result.OK;
        }
               // !
        public object GetAsync(long id, bool adPush)
        {
            object result = TryGetClient(id);
            if (result.GetType() == typeof(Client))
                return (Client)result;

            return (CRUD.Result)result;
        }

        public async Task<CRUD.Result> UpdateAsync(long id, string name, bool adPush) // adPush - позже
        {
            object result = TryGetClient(id);
            if (result.GetType() == typeof(Client))
            {
                Client client = (Client)result;
                client.Name = name;
                try
                {
                    await rep.UpdateAsync(client);
                }
                catch
                {
                    return CRUD.Result.ServerOrConnectionFailed;
                }
                return CRUD.Result.OK;
            }
            return (CRUD.Result)result;
        }

        public async Task<CRUD.Result> DeleteAsync(long id)
        {
            object result = TryGetClient(id);
            if (result.GetType() == typeof(Client))
            {
                Client client = (Client)result;
                try
                {
                    await rep.DeleteAsync(client);
                }
                catch
                {
                    return CRUD.Result.ServerOrConnectionFailed;
                }
                return CRUD.Result.OK;
            }
            return (CRUD.Result)result;
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

        private object TryGetClient(long id)
        {
            Client client;
            try
            {
                client = rep.GetAsync(id);
                if (client == null)
                    return CRUD.Result.EntityNotFound;

                return client;
            }
            catch
            {
                return CRUD.Result.ServerOrConnectionFailed;
            }
        }
    }
}