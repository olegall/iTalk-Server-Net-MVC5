namespace WebApplication1.Models
{
    public class GetConsultantsVM
    {
        public int offset { get; set; }
        public int limit { get; set; }
        public long subcategoryId { get; set; }
        public bool free { get; set; }
        public bool onlyFavorite { get; set; }
        public string filter { get; set; }
    }
}