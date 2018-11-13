using System;
using System.Collections.Specialized;
using System.Web;
using WebApplication1.Models;
using WebApplication1.Utils;
using WebApplication1.DAL;
using System.Threading.Tasks;

namespace WebApplication1.BLL
{
    public class ClientManager
    {
        private readonly GenericRepository<Client> rep = Reps.Clients;
        #region Public methods
        public async Task CreateAsync(NameValueCollection formData)
        {
            try
            {
                await Reps.Clients.CreateAsync(GetInstance(formData));
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

                return Reps.Clients.GetAsync(id);
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
            Client client = Reps.Clients.GetAsync(id);
            client.Name = name;
            try
            {
                await Reps.Clients.UpdateAsync(client);
            }
            catch (Exception e)
            {
                throw new Exception(ServiceUtil.GetExMsg(e, "Не удалось зарегистрировать клиента"));
            }
        }

        public async Task DeleteAsync(long id)
        {
            Client client = Reps.Clients.GetAsync(id);
            try
            {
                await Reps.Clients.DeleteAsync(client);
            } /// !!! 2 catch - под
            catch (Exception e)
            {
                throw new Exception(ServiceUtil.GetExMsg(e, "Не удалось удалить клиента"));
            }
        }
        #endregion
    }
}