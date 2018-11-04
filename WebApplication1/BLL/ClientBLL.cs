using System;
using System.Collections.Specialized;
using System.Web;
using WebApplication1.Models;
using WebApplication1.Utils;
using WebApplication1.DAL;
using System.Threading.Tasks;


namespace WebApplication1.BLL
{
    public class ClientBLL
    {
        private readonly static Repositories reps = new Repositories();
        private readonly DataContext _db = new DataContext();
        private readonly GenericRepository<Client> rep = reps.Clients;

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
        {
            try
            {
                return rep.GetAsync(id);
            }
            catch (Exception e)
            {
                throw new HttpException("Нет клиента с таким Id или проблемы с доступом к серверу. "+"InnerEcxeption: "+e.InnerException.Message);
            }
            finally
            {
                rep.Dispose();
            }
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
    }
}