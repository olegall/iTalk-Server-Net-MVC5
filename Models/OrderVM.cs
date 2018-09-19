using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace WebApplication1.Models
{
    public class OrderVM
    {
        public string Status { get; set; }
        public int StatusCode { get; set; }
        public long ServiceId { get; set; }
        public string Image { get; set; }
    }
}