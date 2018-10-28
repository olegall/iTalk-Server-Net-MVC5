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

        public async Task<Client> GetAsync(long id, bool adPush)
        {
            return await rep.GetAsync(id);
        }

        private Client GetInstance(NameValueCollection formData)
        {
            return new Client(formData["name"], formData["phone"]);
        }

        public async void UpdateAsync(long id, string name, bool adPush) // adPush - позже
        {
            Client client = await rep.GetAsync(id);
            client.Name = name;
            try
            {
                rep.UpdateAsync(client);
            }
            catch (Exception e)
            {
                throw new Exception(ServiceUtil.GetExMsg(e, "Не удалось зарегистрировать клиента"));
            }
        }
        // !!! тут не получалось через репозиторий
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