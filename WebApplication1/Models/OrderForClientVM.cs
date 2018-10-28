using System;

namespace WebApplication1.Models
{
    public class OrderForClientVM
    {
        public long Id { get; set; }
        public long Number { get; set; }
        public DateTime Date { get; set; }
        public string Consultant { get; set; }
        public string Service { get; set; }
    }
}