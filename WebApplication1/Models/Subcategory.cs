namespace WebApplication1.Models
{
    public class Subcategory
    {
        public int Id { get; set; }
        public int CategoryId { get; set; }
        public string Title { get; set; }
        public bool Deleted { get; set; }
    }
}