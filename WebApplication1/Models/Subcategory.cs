using WebApplication1.BLL;
namespace WebApplication1.Models
{
    public class Subcategory //: Base
    {
        public int Id { get; set; }
        public int CategoryId { get; set; }
        public string Title { get; set; }
        public bool Deleted { get; set; }
    }
}