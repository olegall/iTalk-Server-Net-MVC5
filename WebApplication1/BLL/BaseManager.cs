using WebApplication1.Utils;

namespace WebApplication1.BLL
{
    public class BaseManager
    {
        protected CRUDResult<TEntity> TryGetEntity<TEntity>(IGenericRepository<TEntity> rep, long id) where TEntity : class
        {
            CRUDResult<TEntity> CRUDResult = new CRUDResult<TEntity>();
            try
            {
                CRUDResult.Entity = rep.GetAsync(id);
                if (CRUDResult.Entity == null)
                {
                    CRUDResult.Mistake = (int)CRUDResult<TEntity>.Mistakes.EntityNotFound;
                    return CRUDResult;
                }
            }
            catch
            {
                CRUDResult.Mistake = (int)CRUDResult<TEntity>.Mistakes.ServerOrConnectionFailed;
            }
            return CRUDResult;
        }
    }
}