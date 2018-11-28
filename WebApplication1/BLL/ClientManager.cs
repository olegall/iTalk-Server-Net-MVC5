using System;
using System.Web;
using System.Linq;
using System.Threading.Tasks;
using System.Collections.Generic;
using WebApplication1.Models;
using WebApplication1.Interfaces;

namespace WebApplication1.BLL
{
    public class ClientManager : IClientManager
    {
        private readonly IGenericRepository<Client> rep;
        public ClientManager(IGenericRepository<Client> rep)
        {
            this.rep = rep;
        }
        private static readonly DataContext _db = new DataContext();
        #region Public methods
        public async Task CreateAsync(string name, string phone)
        {
            try
            {
                await rep.CreateAsync(GetInstance(name, phone));
            }
            catch (Exception e)
            {
                throw new HttpException(500, "Не удалось зарегистрировать клиента");
            }
        }
        // ! проверки на null
        public Client GetAsync(long id, bool adPush)
        {   // !!! разделить ситуации - исключения по обрыву связи и не найденному польз-лю
            try
            {
                return rep.GetAsync(id);
            }
            catch // !!! отфильтровать исключение
            {// !!! код ошибки
                throw new HttpException(500, "Нет клиента с таким Id или проблемы с доступом к серверу");
            }
            // !!! с finally после Update не работает Get
            //finally
            //{
            //    Reps.Clients.Dispose();
            //}
        }

        private Client GetInstance(string name, string phone)
        {
            return new Client(name, phone);
        }

        public async Task UpdateAsync(long id, string name, bool adPush) // adPush - позже
        {
            Client client = rep.GetAsync(id);
            client.Name = name;
            try
            {
                await rep.UpdateAsync(client);
            }
            catch (Exception e)
            {
                throw new HttpException(500, "Не удалось зарегистрировать клиента");
            }
        }

        public async Task DeleteAsync(long id)
        {
            Client client = rep.GetAsync(id);
            try
            {
                await rep.DeleteAsync(client);
            } 
            catch (Exception e)
            {
                throw new HttpException(500, "Не удалось удалить клиента");
            }
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
        #endregion
    }
}