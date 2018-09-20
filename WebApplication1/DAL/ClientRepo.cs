using WebApplication1.Models;
using WebApplication1.BLL;
using Microsoft.AspNet.Identity;
using System.Threading.Tasks;
using System.Data.Entity;

namespace WebApplication1.DAL
{
    public class ClientRepo : GenericRepository<Client>,
                            IUserStore<Client, long>
    //IUserRoleStore<ConsultantManager, long>,
    //IUserPasswordStore<ConsultantManager, long>,
    //IUserEmailStore<ConsultantManager, long>,
    //IUserSecurityStampStore<ConsultantManager, long>,
    //IUserLockoutStore<ConsultantManager, long>,
    //IUserTwoFactorStore<ConsultantManager, long>
    {
        public ClientRepo() : base(new DataContext()) { }

        public ClientRepo(DataContext context) : base(context) { }

        public Task CreateAsync(Client user)
        {
            _dbSet.Add(user);
            return _context.SaveChangesAsync();
        }

        public Task UpdateAsync(Client user)
        {
            _context.Entry(user).State = EntityState.Modified;
            return _context.SaveChangesAsync();
        }

        public Task DeleteAsync(Client user)
        {
            _dbSet.Remove(user);
            return _context.SaveChangesAsync();
        }

        public Task<Client> FindByIdAsync(long userId)
        {
            return _dbSet.FindAsync(userId);
        }

        public Task<Client> FindByNameAsync(string userName)
        {
            //return _dbSet.SingleOrDefaultAsync(x => x.Login == userName);
            return _dbSet.SingleOrDefaultAsync(x => x.Name /*Phone*/ == userName);
        }

        public Task<Client> FindByPhoneAsync(string phone)
        {
            return _dbSet.SingleOrDefaultAsync(x => x.Phone == phone);
        }
    }
}