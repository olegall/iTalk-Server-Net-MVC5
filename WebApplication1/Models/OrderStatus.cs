namespace WebApplication1.Models
{
    public enum OrderStatuses { Начат_клиентом = 1,
                                Принят_консультантом,
                                Отменён_клиентом,
                                Отменён_консультантом };

    public class OrderStatus
    {
        public int Id { get; set; }
        public int Code { get; set; }
        public string Text { get; set; }
    }
}