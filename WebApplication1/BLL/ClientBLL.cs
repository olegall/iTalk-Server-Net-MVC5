using System;
using System.Collections.Specialized;
using System.Linq;
using WebApplication1.Models;
using WebApplication1.Utils;
using WebApplication1.DAL;
using System.Threading.Tasks;
using System.Collections.Generic;

namespace WebApplication1.BLL
{
    public class ClientBLL
    {
        private readonly static Repositories reps = new Repositories();
        private readonly DataContext _db = new DataContext();
        private readonly GenericRepository<Client> rep = reps.Clients;

        public void Create(NameValueCollection formData)
        {
            try
            {
                rep.CreateAsync(GetInstance(formData));
            }
            catch (Exception e)
            {
                throw new Exception(ServiceUtil.GetExMsg(e, "Не удалось зарегистрировать клиента"));
            }
        }

        // !!! надо GetAsync, async перед Task, await после return?
        public Task<Client> Get(long id, bool adPush)
        {
            return rep.GetAsync(id);
        }
        // !!! через конструктор
        private Client GetInstance(NameValueCollection formData)
        {
            Client client = new Client();
            client.Name = formData["name"];
            client.Phone = formData["phone"];
            return client;
        }

        public async void UpdateAsync(long id, string name, bool adPush)
        {
            Client client = await rep.GetAsync(id);
            client.Name = name;
            //client.AdPush = adPush;
            try
            {
                rep.UpdateAsync(client);
            }
            catch (Exception e)
            {
                throw new Exception(ServiceUtil.GetExMsg(e, "Не удалось зарегистрировать клиента"));
            }
        }

        public void Delete(long id)
        {
            Client client = _db.Clients.SingleOrDefault(x => x.Id == id);
            try
            {
                _db.Clients.Remove(client);
                _db.SaveChanges();
            }
            catch (Exception e)
            {
                throw new Exception(ServiceUtil.GetExMsg(e, "Не удалось удалить клиента"));
            }
        }
    }
}