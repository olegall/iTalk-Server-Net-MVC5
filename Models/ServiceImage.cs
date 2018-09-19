using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WebApplication1.Models
{
    public class ServiceImage : File
    {
        public long ServiceId { get; set; }
    }
}