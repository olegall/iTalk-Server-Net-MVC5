using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;

namespace WebApplication1.Models
{
    public class JuridicConsultantVM : ConsultantVM
    {
        public string LTDTitle { get; set; }
        public string OGRN { get; set; }
        public string INN { get; set; }
        public string SiteUrl { get; set; }
        public string Logo { get; set; }
        public string OGRNCertificate { get; set; }
        public string BankAccountDetails { get; set; }
    }
}