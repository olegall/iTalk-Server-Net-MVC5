using System;
using System.Collections.Generic;
using System.Linq;
using System.Data.Entity;
using System.Threading.Tasks;

namespace WebApplication1.BLL
{
    //public class Base
    //{
    //    public int Id { get; set; }
    //}

    public class GenericRepository<TEntity> : IGenericRepository<TEntity> where TEntity /*: Base*/ : class
    {
        protected DbContext _context;
        protected DbSet<TEntity> _dbSet;

        public GenericRepository(DbContext context)
        {
            _context = context;
            _dbSet = context.Set<TEntity>();
        }
        // !!! убрать
        public IEnumerable<TEntity> Get()
        {
            return _dbSet.AsNoTracking().ToArray();
        }
        // !!! AsNoTracking?
        // !!! не работает
        //public async Task<IEnumerable<TEntity>> GetAsync()
        //{
        //    return await _dbSet.AsNoTracking().ToArrayAsync();
        //}

        public async Task<TEntity> GetAsync_(long id)
        {
            return await _dbSet.FindAsync(id);
        }

        // !!! убрать async, .Result
        public TEntity GetAsync(long id)
        {
            return _dbSet.FindAsync(id).Result;
            //return _dbSet.Find(id);
        }

        // GetPaging

        public void Create(TEntity item) // ! для теста. убрать
        {
            _dbSet.Add(item);
            _context.SaveChanges();
        }

        public async Task CreateAsync(TEntity item)
        {
            _dbSet.Add(item);
            await _context.SaveChangesAsync();
        }

        public async void CreateManyAsync(IEnumerable<TEntity> items)
        {
            foreach (TEntity item in items)
            {
                _dbSet.Add(item);
            }
            await _context.SaveChangesAsync();
        }

        public async Task UpdateAsync(TEntity item)
        {
            _context.Entry(item).State = EntityState.Modified;
            await _context.SaveChangesAsync();
        }

        public async Task DeleteAsync(TEntity item)
        {
            _dbSet.Remove(item);
            await _context.SaveChangesAsync();
        }

        #region Disposing
        protected bool IsExtContext { get; private set; }

        private bool disposed = false;

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        public virtual void Dispose(bool disposing)
        {
            if (!this.disposed)
            {
                if (disposing && !IsExtContext)
                {
                    _context.Dispose();
                }
            }
            this.disposed = true;
        }
        #endregion
    }
}