namespace WebApplication1.Models
{
    public class Category
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public bool Deleted { get; set; }

        public Category(string title, string description)
        {
            Title = title;
            Description = description;
        }
    }
}