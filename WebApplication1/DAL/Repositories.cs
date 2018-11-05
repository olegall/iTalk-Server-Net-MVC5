using WebApplication1.Models;
using WebApplication1.BLL;

namespace WebApplication1.DAL
{
    public static class Reps
    {
        private static readonly DataContext _db = new DataContext();

        public static GenericRepository<Client>            Clients           { get { return new GenericRepository<Client>(_db); } }
        public static GenericRepository<PrivateConsultant> Privates          { get { return new GenericRepository<PrivateConsultant>(_db); } }
        public static GenericRepository<JuridicConsultant> Juridics          { get { return new GenericRepository<JuridicConsultant>(_db); } }

        public static GenericRepository<Order>             Orders            { get { return new GenericRepository<Order>(_db); } }

        public static GenericRepository<Category>          Categories        { get { return new GenericRepository<Category>(_db); } }
        public static GenericRepository<Subcategory>       Subcategories     { get { return new GenericRepository<Subcategory>(_db); } }

        public static GenericRepository<Service>           Services          { get { return new GenericRepository<Service>(_db); } }
        public static GenericRepository<Favorite>          Favorites         { get { return new GenericRepository<Favorite>(_db); } }

        public static GenericRepository<ChatActivity>      ChatActivities    { get { return new GenericRepository<ChatActivity>(_db); } }
        public static GenericRepository<ChatMsg>           ChatMsgs          { get { return new GenericRepository<ChatMsg>(_db); } }

        public static GenericRepository<ConsultantImage>   ConsultantImages  { get { return new GenericRepository<ConsultantImage>(_db); } }
        public static GenericRepository<GalleryImage>      GalleryImages     { get { return new GenericRepository<GalleryImage>(_db); } }
        public static GenericRepository<CategoryImage>     CategoryImages    { get { return new GenericRepository<CategoryImage>(_db); } }
        public static GenericRepository<ServiceImage>      ServiceImages     { get { return new GenericRepository<ServiceImage>(_db); } }

        public static GenericRepository<Feedback>          Feedbacks         { get { return new GenericRepository<Feedback>(_db); } }
        public static GenericRepository<ConsultationType>  ConsultationTypes { get { return new GenericRepository<ConsultationType>(_db); } }

        public static GenericRepository<OrderStatus>       OrderStatuses     { get { return new GenericRepository<OrderStatus>(_db); } }
        public static GenericRepository<PaymentStatus>     PaymentStatuses   { get { return new GenericRepository<PaymentStatus>(_db); } }
    }
}