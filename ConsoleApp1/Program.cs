using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WebApplication1.Controllers;
using WebApplication1.DAL;
using WebApplication1.BLL;
using System.Linq;
using WebApplication1.Models;
using WebApplication1;

namespace ConsoleApp1
{
    class Program
    {
        static void Main(string[] args)
        {
            new ClientsController().Get(1, true);
        }
    }
}
