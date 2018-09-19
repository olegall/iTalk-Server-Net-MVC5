using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WebApplication1.Models
{
    public class ConsultantStatus
    {
        public int Id { get; set; }
        public string Status { get; set; }
    }

    /*
    Id	Status
    1	Доступен
    2	Не доступен
    3	Будет доступен к
    4	Занят
    */
}