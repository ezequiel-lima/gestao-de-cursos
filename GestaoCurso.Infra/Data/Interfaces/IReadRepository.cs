using System.Linq.Expressions;

namespace GestaoCurso.Infra.Data.Interfaces
{
    public interface IReadRepository<T> where T : class
    {
        IQueryable<T> FindAll();
        IQueryable<T> FindByCondition(Expression<Func<T, bool>> expression);
        bool Any(Expression<Func<T, bool>> expression);
    }
}
