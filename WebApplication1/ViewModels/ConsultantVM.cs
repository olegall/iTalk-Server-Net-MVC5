using System.Collections.Generic;

namespace WebApplication1.ViewModels
{
    public class ConsultantVM
    {
        public long Id { get; set; }
        public decimal Rating { get; set; }
        public int FeedbacksCount { get; set; }
        public IEnumerable<ServiceVM> Services { get; set; }
        public string AccountNumber { get; set; }
        public bool Free { get; set; }
        public long FreeDate { get; set; }
        public bool LegalEntity { get; set; }
        public bool Favorite { get; set; }
    }
}