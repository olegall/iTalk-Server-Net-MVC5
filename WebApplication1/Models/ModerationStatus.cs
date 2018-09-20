using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WebApplication1.Models
{
    public class ModerationStatus
    {
        public int Id { get; set; }
        public string Text { get; set; }
    }

    /*
    Id	Status
    1	На рассмотрении
    2	Отклонено
    3	Разрешено
    */
}