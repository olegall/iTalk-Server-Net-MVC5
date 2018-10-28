using System.Collections.Generic;

namespace WebApplication1.Models
{
    public class ImagesVM
    {
        public string Profile { get; set; }
        public IEnumerable<string> Gallery { get; set; }
    }
}