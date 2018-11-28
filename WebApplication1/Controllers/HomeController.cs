using System.Web.Mvc;
using System;
using WebApplication1.BLL;
using WebApplication1.Utils;
using WebApplication1.Misc;
using WebApplication1.Misc.Auth;
using System.Web.Http;
using WebApplication1.Interfaces;
using WebApplication1.DAL;

namespace WebApplication1.Controllers
{
    public class HomeController : Controller
    {
        // !
        //private readonly IClientManager mng;
        //public HomeController(IClientManager mng)
        //{
        //    this.mng = mng;
        //}

        // GET: Home
        public ActionResult Index()
        {
            //var a1 = mng;
            //var a2 = mng.GetAsync(1, true);
            return View();
        }
    }
}