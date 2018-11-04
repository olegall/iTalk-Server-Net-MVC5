using WebApplication1.BLL;
namespace WebApplication1.Models
{
    public class PaymentStatus //: Base
    {
        public int Id { get; set; }
        public string Text { get; set; }
    }

    /*
    Id	Status
    1	Оплачено
    2	Не оплачено
    */
}