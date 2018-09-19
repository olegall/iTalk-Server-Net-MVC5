//using Cryotop.App_LocalResources;
using System.ComponentModel.DataAnnotations;

namespace WebApplication1.Models
{
    public class LoginModel
    {
        [Display(Name = "Login"/*, ResourceType = typeof(ModelRes)*/)]
        [Required]
        [MaxLength(50)]
        public string Login { get; set; }

        [Display(Name = "Password"/*, ResourceType = typeof(ModelRes)*/)]
        [DataType(DataType.Password)]
        [MinLength(6)]
        [Required]
        public string Password { get; set; }

        [Display(Name = "RememberMe"/*, ResourceType = typeof(ModelRes)*/)]
        public bool RememberMe { get; set; }
    }
}