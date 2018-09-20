using WebApplication1.Models;
using WebApplication1.BLL;
using Microsoft.AspNet.Identity;
using System.Threading.Tasks;
using System.Data.Entity;

namespace WebApplication1.DAL
{
    public class ConsultantRepo : GenericRepository<Consultant>,
                            IUserStore<Consultant, long>
    //IUserRoleStore<ConsultantManager, long>,
    //IUserPasswordStore<ConsultantManager, long>,
    //IUserEmailStore<ConsultantManager, long>,
    //IUserSecurityStampStore<ConsultantManager, long>,
    //IUserLockoutStore<ConsultantManager, long>,
    //IUserTwoFactorStore<ConsultantManager, long>
    {
        public ConsultantRepo() : base(new DataContext()) { }

        public ConsultantRepo(DataContext context) : base(context) { }

        public Task CreateAsync(Consultant user)
        {
            _dbSet.Add(user);
            return _context.SaveChangesAsync();
        }

        public Task UpdateAsync(Consultant user)
        {
            _context.Entry(user).State = EntityState.Modified;
            return _context.SaveChangesAsync();
        }

        public Task DeleteAsync(Consultant user)
        {
            _dbSet.Remove(user);
            return _context.SaveChangesAsync();
        }

        public async Task<Consultant> FindAsync(string userName, string email, string phone)
        {
            //return await _dbSet.Where(x => x.Login == userName
            //    && x.Email == email
            //    && x.Phone == phone).SingleOrDefaultAsync();
            return null;
        }

        public Task<Consultant> FindByIdAsync(long userId)
        {
            return _dbSet.FindAsync(userId);
        }

        public Task<Consultant> FindByNameAsync(string userName)
        {
            //return _dbSet.SingleOrDefaultAsync(x => x.Login == userName);
            return null;
        }
    }
}