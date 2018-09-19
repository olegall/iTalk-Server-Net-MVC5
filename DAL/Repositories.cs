using WebApplication1.Models;
using WebApplication1.BLL;

namespace WebApplication1.DAL
{
    public class Repositories
    {
        private readonly DataContext _db = new DataContext();

        public GenericRepository<Client>            Clients           { get { return new GenericRepository<Client>(_db); } }
        public GenericRepository<PrivateConsultant> Privates          { get { return new GenericRepository<PrivateConsultant>(_db); } }
        public GenericRepository<JuridicConsultant> Juridics          { get { return new GenericRepository<JuridicConsultant>(_db); } }

        public GenericRepository<Order>             Orders            { get { return new GenericRepository<Order>(_db); } }

        public GenericRepository<Category>          Categories        { get { return new GenericRepository<Category>(_db); } }
        public GenericRepository<Subcategory>       Subcategories     { get { return new GenericRepository<Subcategory>(_db); } }

        public GenericRepository<Service>           Services          { get { return new GenericRepository<Service>(_db); } }
        public GenericRepository<Favorite>          Favorites         { get { return new GenericRepository<Favorite>(_db); } }

        public GenericRepository<ChatActivity>      ChatActivities    { get { return new GenericRepository<ChatActivity>(_db); } }
        public GenericRepository<ChatMsg>           ChatMsgs          { get { return new GenericRepository<ChatMsg>(_db); } }

        public GenericRepository<ConsultantImage>   ConsultantImages  { get { return new GenericRepository<ConsultantImage>(_db); } }
        public GenericRepository<GalleryImage>      GalleryImages     { get { return new GenericRepository<GalleryImage>(_db); } }
        public GenericRepository<CategoryImage>     CategoryImages    { get { return new GenericRepository<CategoryImage>(_db); } }
        public GenericRepository<ServiceImage>      ServiceImages     { get { return new GenericRepository<ServiceImage>(_db); } }

        public GenericRepository<Feedback>          Feedbacks         { get { return new GenericRepository<Feedback>(_db); } }
        public GenericRepository<ConsultationType>  ConsultationTypes { get { return new GenericRepository<ConsultationType>(_db); } }

        public GenericRepository<OrderStatus>       OrderStatuses     { get { return new GenericRepository<OrderStatus>(_db); } }
        public GenericRepository<PaymentStatus>     PaymentStatuses   { get { return new GenericRepository<PaymentStatus>(_db); } }
    }
}