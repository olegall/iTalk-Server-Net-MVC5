using WebApplication1.Models;
using WebApplication1.DAL;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.Owin;
using Microsoft.Owin;

namespace WebApplication1.Misc.Auth
{
    public class ClientManager : UserManager<Client, long>
    {
        public ClientManager(IUserStore<Client, long> store = null) : base(store ?? new ClientRepo())
        {
        }

        public static ClientManager Create(IdentityFactoryOptions<ClientManager> options, IOwinContext context)
        {
            var dbContext = context.Get<DataContext>();
            var manager = new ClientManager(new ClientRepo(dbContext));

            manager.UserValidator = new UserValidator<Client, long>(manager)
            {
                AllowOnlyAlphanumericUserNames = false,
                RequireUniqueEmail = true
            };

            manager.PasswordValidator = new PasswordValidator
            {
                RequiredLength = 6,
                RequireNonLetterOrDigit = false,
                RequireDigit = false,
                RequireLowercase = false,
                RequireUppercase = false,
            };

            manager.UserLockoutEnabledByDefault = false;

            //manager.EmailService = new EmailService();
            var dataProtectionProvider = options.DataProtectionProvider;
            if (dataProtectionProvider != null)
            {
                manager.UserTokenProvider =
                    new DataProtectorTokenProvider<Client, long>(dataProtectionProvider.Create("Cryotop.User"));
            }

            return manager;
        }
    }
}