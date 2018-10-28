using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace WebApplication1.Models
{
    // 26 40
    public class PrivateConsultant : Consultant
    {
        [StringLength(20)]
        public string Name { get; set; }

        [StringLength(20)]
        public string Surname { get; set; }

        [StringLength(20)]
        public string Patronymic { get; set; }

        public string ProfileImageName { get; set; }
        public List<string> GalleryImageNames { get; set; }
        public string PhotoName { get; set; }
        public string PassportScanName { get; set; }

        public PrivateConsultant(string name, 
                                 string surname, 
                                 string patronymic, 
                                 string phone, 
                                 string email, 
                                 DateTime freeDate)
        {
            Name =  name;
            Surname = surname;
            Patronymic = patronymic;
            Phone = phone;
            Email = email;
        }
    }
}