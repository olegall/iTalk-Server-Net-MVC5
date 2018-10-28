using System;
using System.ComponentModel.DataAnnotations;

namespace WebApplication1.Models
{
    public class JuridicConsultant : Consultant
    {
        // !!! везде ограничения на длины
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

        public JuridicConsultant(string ltdtitle, string ogrn, string inn, string phone, string siteurl, DateTime freeDate)
        {
            LTDTitle = ltdtitle;
            OGRN = ogrn;
            INN = inn;
            Phone = phone;
            SiteUrl = siteurl;
            FreeDate = freeDate;
        }
    }
}