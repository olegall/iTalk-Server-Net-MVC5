using System.Collections.Generic;

namespace WebApplication1.ViewModels
{
    public class FavoriteConsultantVM
    {
        public long Id { get; set; }
        public string Name { get; set; }
        public decimal Rating { get; set; }
        public int FeedbacksCount { get; set; }
        public IEnumerable<ServiceVM> Services { get; set; }
    }
}