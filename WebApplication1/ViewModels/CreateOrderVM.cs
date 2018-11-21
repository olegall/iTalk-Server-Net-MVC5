using System;

namespace WebApplication1.ViewModels
{
    public class CreateOrderVM
    {
        public long ServiceId { get; set; }
        public long StatusId { get; set; }
        public long ConsultationTypeId { get; set; }
        public DateTime СlientConvenientConsultationTime { get; set; }
    }
}