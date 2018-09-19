using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WebApplication1.Models
{
    public class ImagesVM
    {
        public string Profile { get; set; }
        public IEnumerable<string> Gallery { get; set; }
    }
}