using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WebApplication1.Models
{
    public class PrivateConsultantListVM
    {
        public string Id { get; set; }
        public string Name { get; set; }
        public string Surname { get; set; }
        public string Patronymic { get; set; }
        public string ProfileImageName { get; set; }
        public IEnumerable<string> GalleryImagesNames { get; set; }
        public decimal Rating { get; set; }
        public int FeedbacksCount { get; set; }
        public IEnumerable<ServiceVM> Services { get; set; }
        public bool Free { get; set; }
		public bool Favorite { get; set; }
        public DateTime FreeDate { get; set; }
        public bool LegalEntity { get; set; }
    }
}