using WebApplication1.Models;
using WebApplication1.BLL;

namespace WebApplication1.DAL
{
    public static class Reps
    {
        private static readonly DataContext _db = new DataContext();

        public static IGenericRepository<Client>            Clients           { get { return new GenericRepository<Client>(_db); } }
        public static IGenericRepository<PrivateConsultant> Privates          { get { return new GenericRepository<PrivateConsultant>(_db); } }
        public static IGenericRepository<JuridicConsultant> Juridics          { get { return new GenericRepository<JuridicConsultant>(_db); } }

        public static IGenericRepository<Order>             Orders            { get { return new GenericRepository<Order>(_db); } }

        public static IGenericRepository<Category>          Categories        { get { return new GenericRepository<Category>(_db); } }
        public static IGenericRepository<Subcategory>       Subcategories     { get { return new GenericRepository<Subcategory>(_db); } }

        public static IGenericRepository<Service>           Services          { get { return new GenericRepository<Service>(_db); } }
        public static IGenericRepository<Favorite>          Favorites         { get { return new GenericRepository<Favorite>(_db); } }

        public static IGenericRepository<ChatActivity>      ChatActivities    { get { return new GenericRepository<ChatActivity>(_db); } }
        public static IGenericRepository<ChatMsg>           ChatMsgs          { get { return new GenericRepository<ChatMsg>(_db); } }

        public static IGenericRepository<ConsultantImage>   ConsultantImages  { get { return new GenericRepository<ConsultantImage>(_db); } }
        public static IGenericRepository<GalleryImage>      GalleryImages     { get { return new GenericRepository<GalleryImage>(_db); } }
        public static IGenericRepository<CategoryImage>     CategoryImages    { get { return new GenericRepository<CategoryImage>(_db); } }
        public static IGenericRepository<ServiceImage>      ServiceImages     { get { return new GenericRepository<ServiceImage>(_db); } }

        public static IGenericRepository<Feedback>          Feedbacks         { get { return new GenericRepository<Feedback>(_db); } }
        public static IGenericRepository<ConsultationType>  ConsultationTypes { get { return new GenericRepository<ConsultationType>(_db); } }

        public static IGenericRepository<OrderStatus>       OrderStatuses     { get { return new GenericRepository<OrderStatus>(_db); } }
        public static IGenericRepository<PaymentStatus>     PaymentStatuses   { get { return new GenericRepository<PaymentStatus>(_db); } }
    }
}