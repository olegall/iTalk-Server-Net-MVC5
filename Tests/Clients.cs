using Microsoft.VisualStudio.TestTools.UnitTesting;
using WebApplication1.DAL;
using WebApplication1.BLL;
using System.Linq;
using WebApplication1.Models;
using WebApplication1;
using WebApplication1.Controllers;

namespace Tests
{
    [TestClass]
    public class Clients
    {
        private readonly DataContext _db = new DataContext();
        private readonly ClientManager mng = new ClientManager(Reps.Clients);

        public int Count { get { return new DB().GetClients().Count(); } }

        [TestMethod]
        public void Create() // !
        {
            var aa = new ClientsController().Get(1, true);
            //mng.CreateAsync("title1", "description1");
            var a1 = Reps.Clients.Get();
            var a2 = mng.GetAllTest();
            var a3 = _db.Database.SqlQuery<Client>("select * from Clients");
            var a4 = Reps.Clients.Get();
            //int clientsCountAfterCreation = Count;
            //Assert.AreEqual(mng.GetAllTest(), 3);
        }
    }
}