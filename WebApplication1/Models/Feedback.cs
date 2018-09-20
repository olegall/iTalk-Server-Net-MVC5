using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WebApplication1.Models
{
    public class Feedback
    {
        public long Id { get; set; }
        public long ConsultantId { get; set; }
        public long ClientId { get; set; }
        public string Text { get; set; }
    }
}