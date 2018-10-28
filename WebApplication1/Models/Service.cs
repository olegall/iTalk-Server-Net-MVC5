using System.ComponentModel.DataAnnotations;

namespace WebApplication1.Models
{
    public class Service
    {
        public long Id { get; set; }
        public long ConsultantId { get; set; }
        public long CategoryId { get; set; }
        public long SubcategoryId { get; set; }
        public long ModerationStatusId { get; set; }
        public string Phone { get; set; }
        public decimal Cost { get; set; }
        public long ServiceCostId { get; set; }     // на будущее, пока одна цена в течение дня
        [StringLength(20)]
        public string Title { get; set; }
        public string Description { get; set; }
        public decimal Duration { get; set; }
        public int UsageCount { get; set; }
        public bool Deleted { get; set; }
        public bool Available { get; set; }
        public int AvailablePeriod { get; set; }
    }
}