using System;
using System.ComponentModel.DataAnnotations.Schema;
using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.AspNet.Identity;
using WebApplication1.Misc.Auth;

namespace WebApplication1.Models
{
    public class Client : /*Base,*/ IUser<long>
    {
        public long Id { get; set; }
        public string Name { get; set; }
        public string Phone { get; set; }
        public Guid SocketId { get; set; }

        public Client(string name, string phone)
        {
            Name = name;
            Phone = phone;
        }

        public Client()
        {
        }

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