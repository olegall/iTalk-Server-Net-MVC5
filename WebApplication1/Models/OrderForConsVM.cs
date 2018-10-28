using System;

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