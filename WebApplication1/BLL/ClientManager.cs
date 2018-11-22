using System;
using System.Collections.Specialized;
using System.Web;
using WebApplication1.Models;
using WebApplication1.Utils;
using System.Threading.Tasks;
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

        #region Public methods
        public async Task CreateAsync(NameValueCollection formData)
        {
            try
            {
                await rep.CreateAsync(GetInstance(formData));
            }
            catch (Exception e)
            {
                throw new Exception(ServiceUtil.GetExMsg(e, "Не удалось зарегистрировать клиента"));
            }
        }

        public Client GetAsync(long id, bool adPush)
        {   // !!! разделить ситуации - исключения по обрыву связи и не найденному польз-лю
            try
            {

                return rep.GetAsync(id);
            }
            catch // !!! отфильтровать исключение
            {// !!! код ошибки
                throw new HttpException("Нет клиента с таким Id или проблемы с доступом к серверу");
            }
            // !!! с finally после Update не работает Get
            //finally
            //{
            //    Reps.Clients.Dispose();
            //}
        }

        private Client GetInstance(NameValueCollection formData)
        {
            return new Client(formData["name"], formData["phone"]);
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
                throw new Exception(ServiceUtil.GetExMsg(e, "Не удалось зарегистрировать клиента"));
            }
        }

        public async Task DeleteAsync(long id)
        {
            Client client = rep.GetAsync(id);
            try
            {
                await rep.DeleteAsync(client);
            } /// !!! 2 catch - под
            catch (Exception e)
            {
                throw new Exception(ServiceUtil.GetExMsg(e, "Не удалось удалить клиента"));
            }
        }
        #endregion
    }
}