using System.Data.Entity;
using WebApplication1.Models;

namespace WebApplication1
{
    public class DataContext : DbContext
    {
        public DbSet<Client> Clients { get; set; }
        public DbSet<PrivateConsultant> PrivateConsultants { get; set; }
        public DbSet<JuridicConsultant> JuridicConsultants { get; set; }
        public DbSet<Service> Services { get; set; }
        public DbSet<Order> Orders { get; set; }
        public DbSet<Category> Categories { get; set; }
        public DbSet<Subcategory> Subcategories { get; set; }
        public DbSet<Feedback> Feedbacks { get; set; }
        public DbSet<Favorite> Favorites { get; set; }

        public DbSet<ChatActivity> ChatActivity { get; set; }
        public DbSet<ChatMsg> ChatMessages { get; set; }

        public DbSet<GalleryImage> GalleryImages { get; set; }
        public DbSet<ConsultantImage> ConsultantImages { get; set; }
        public DbSet<ServiceImage> ServiceImages { get; set; }
        public DbSet<CategoryImage> CategoryImages { get; set; }

        public DbSet<OrderStatus> OrderStatuses { get; set; }
        public DbSet<PaymentStatus> PaymentStatuses { get; set; }

        public DbSet<ConsultationType> ConsultationTypes { get; set; }

        //public DbSet<File> FILES { get; set; }

        public DataContext() : base("DataContext")
        { }

        public static DataContext Create()
        {
            return new DataContext();
        }
    }
}