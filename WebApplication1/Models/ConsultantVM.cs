using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace WebApplication1.Models
{
    public class ConsultantVM
    {
        public long Id { get; set; }
        public decimal Rating { get; set; }
        public int FeedbacksCount { get; set; }
        public IEnumerable<ServiceVM> Services { get; set; }
        public string AccountNumber { get; set; }
        public bool Free { get; set; }
        public long FreeDate { get; set; }
        public bool LegalEntity { get; set; }
        public bool Favorite { get; set; }
    }
}