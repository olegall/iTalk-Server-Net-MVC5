using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.AspNet.Identity;
using System.Security.Claims;
using System.Threading.Tasks;
using WebApplication1.BLL;
namespace WebApplication1.Models
{
    public class Consultant : /*Base,*/ IUser<long>
    {
        [Key]
        public long Id { get; set; }
        public string Phone { get; set; }
        public long ModerationStatusId { get; set; }

        [StringLength(50)]
        public string Email { get; set; }

        [StringLength(100)]
        public string YandexWalletNum { get; set; }

        [Column(TypeName = "Money")]
        public decimal ITalkCommittee { get; set; }

        [Column(TypeName = "Money")]
        public decimal ITalkDebts { get; set; }

        [Column(TypeName = "Money")]
        public decimal ITalkEarnedMoney { get; set; }

        public decimal Rating { get; set; }

        [StringLength(50)]
        public string AccountNumber { get; set; }

        public string ServicesDescription { get; set; }

        public bool Free { get; set; }
        public DateTime FreeDate { get; set; }
        public Guid SocketId { get; set; }

        [NotMapped]
        public string UserName
        {
            get { return Phone; }
            set { Phone = value; }
        }

        public async Task<ClaimsIdentity> GenerateIdentityAsync(UserManager<Consultant, long> manager, string authenticationType)
        {
            var identity = await manager.CreateIdentityAsync(this, authenticationType);
            //identity.AddClaim(new Claim(ClaimType.EMAIL, Email));
            //identity.AddClaim(new Claim(ClaimType.PHONE, Phone));

            return identity;
        }
    }
}