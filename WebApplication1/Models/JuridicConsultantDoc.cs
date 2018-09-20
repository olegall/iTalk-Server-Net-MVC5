using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WebApplication1.Models
{
    public class JuridicConsultantDoc
    {
        public int Id { get; set; }
        public int ConsultantId { get; set; }
        public string Name { get; set; }
    }
}