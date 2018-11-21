using System.Collections.Generic;

namespace WebApplication1.ViewModels
{
    public class CategoryVM
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string Image { get; set; }
        public IEnumerable<SubcategoryVM> Subcategories { get; set; }
    }
}