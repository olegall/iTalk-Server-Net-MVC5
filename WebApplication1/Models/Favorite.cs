using WebApplication1.BLL;
namespace WebApplication1.Models
{
    public class Favorite //: Base
    {
        public long Id { get; set; }
        public long ClientId { get; set; }
        public long ConsultantId { get; set; }

        public Favorite(long clientId, long consultantId)
        {
            ClientId = clientId;
            ConsultantId = consultantId;
        }
    }
}