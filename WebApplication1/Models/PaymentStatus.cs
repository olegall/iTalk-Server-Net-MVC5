using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WebApplication1.Models
{
    public class PaymentStatus
    {
        public int Id { get; set; }
        public string Text { get; set; }
    }

    /*
    Id	Status
    1	Оплачено
    2	Не оплачено
    */
}