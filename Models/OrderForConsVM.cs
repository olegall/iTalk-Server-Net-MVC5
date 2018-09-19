using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace WebApplication1.Models
{
    public class OrderForConsVM
    {
        public long Id { get; set; }
        public string Service { get; set; }
        public string Status { get; set; }
        public string PaymentStatus { get; set; }
        public DateTime Date { get; set; }
    }
}