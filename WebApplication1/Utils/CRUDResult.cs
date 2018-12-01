namespace WebApplication1.Utils
{
    public class CRUDResult<TEntity> where TEntity : class
    {
        public CRUDResult()
        {
            Mistake = (int)Mistakes.None;
        }

        public enum Mistakes
        {
            None,
            EntityNotFound,
            ServerOrConnectionFailed,
            UnrecognizedProblem
        };

        public TEntity Entity { get; set; }
        public int Mistake { get; set; }
    }
}