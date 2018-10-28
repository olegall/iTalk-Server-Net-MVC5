using System;
using System.ComponentModel.DataAnnotations.Schema;

namespace WebApplication1.Models
{
    public class Order
    {
        public long Id { get; set; }
        public long Number { get; set; }
        public long ClientId { get; set; }
        public long ConsultantId { get; set; }
        public long ServiceId { get; set; }
        public int StatusCode { get; set; }
        public long PaymentStatusId { get; set; }
        public long ConsultationTypeId { get; set; }
        public DateTime DateTime { get; set; }
        public string Comment { get; set; }
        public int ClientsGradeToConsultant { get; set; }
        public bool ConfirmedByClient { get; set; }
        public bool ConfirmedByConsultant { get; set; }
        [Column(TypeName = "Money")]
        public decimal Sum { get; set; }
        public string YandexWalletNum { get; set; }
        public string BankAccountDetails { get; set; }
        [Column(TypeName = "Money")]
        public decimal ITalkCommittee { get; set; }
        public string RequestDescription { get; set; }

        public Order(long consultationTypeId, long serviceId, string comment, DateTime dateTime, int statusCode)
        {
            ConsultationTypeId = consultationTypeId;
            ServiceId = serviceId;
            Comment = comment;
            DateTime = dateTime;
            StatusCode = statusCode;
        }
    }
}