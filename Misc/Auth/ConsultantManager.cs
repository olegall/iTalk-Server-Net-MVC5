using WebApplication1.Models;
using WebApplication1.DAL;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.Owin;
using Microsoft.Owin;

namespace WebApplication1.Misc.Auth
{
    public class ConsultantManager : UserManager<Consultant, long>
    {
        public ConsultantManager(IUserStore<Consultant, long> store = null) : base(store ?? new ConsultantRepo())
        {
        }

        public static ConsultantManager Create(IdentityFactoryOptions<ConsultantManager> options, IOwinContext context)
        {
            var dbContext = context.Get<DataContext>();
            var manager = new ConsultantManager(new ConsultantRepo(dbContext));

            manager.UserValidator = new UserValidator<Consultant, long>(manager)
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
                    new DataProtectorTokenProvider<Consultant, long>(dataProtectionProvider.Create("Cryotop.User"));
            }

            return manager;
        }
    }
}