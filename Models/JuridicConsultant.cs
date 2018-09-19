using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;

namespace WebApplication1.Models
{
    public class JuridicConsultant : Consultant
    {
        [StringLength(50)]
        public string LTDTitle { get; set; }

        [StringLength(100)]
        public string OGRN { get; set; }

        [StringLength(50)]
        public string INN { get; set; }

        [StringLength(50)]
        public string SiteUrl { get; set; }

        public string LogoName { get; set; }
        public string OGRNCertificate { get; set; }
        public string BankAccountDetails { get; set; }
    }
}