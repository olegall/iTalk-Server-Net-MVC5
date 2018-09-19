namespace WebApplication1.Models.ServiceJSON
{
    public class ServiceParams
    {
        public string Category { get; set; } // Id или название
        public string Subcategory { get; set; } // Id или название
        public string Title { get; set; }
        public string Description { get; set; }
        public decimal Cost { get; set; }
        public int Duration { get; set; }
        public bool Available { get; set; }
        public int AvailablePeriod { get; set; }
    }
}