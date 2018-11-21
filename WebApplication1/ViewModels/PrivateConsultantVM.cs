using System.Collections.Generic;

namespace WebApplication1.ViewModels
{
    public class PrivateConsultantVM : ConsultantVM
    {
        public string Name { get; set; }
        public string Surname { get; set; }
        public string Patronymic { get; set; }
        public string Photo { get; set; }
        public IEnumerable<string> GalleryImages { get; set; }
    }
}