using System;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.AspNet.Identity;
using System.Security.Claims;
using System.Threading.Tasks;
using WebApplication1.Misc.Auth;

namespace WebApplication1.Models
{
    public class Client : IUser<long>
    {
        public long Id { get; set; }
        public string Name { get; set; }
        public string Phone { get; set; }
        public Guid SocketId { get; set; }

        [NotMapped]
        public string UserName
        {
            get { return Phone; }
            set { Phone = value; }
        }

        public async Task<ClaimsIdentity> GenerateIdentityAsync(UserManager<Client, long> manager, string authenticationType)
        {
            var identity = await manager.CreateIdentityAsync(this, authenticationType);
            identity.AddClaim(new Claim(ClaimType.TOKEN, Phone));
            return identity;
        }
    }
}