using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WebApplication1.Models
{
    public class Load
    {
        public int Id { get; set; }
        public int ConsultantId { get; set; }
        public int Level { get; set; }
        public DateTime Date { get; set; }
    }
}