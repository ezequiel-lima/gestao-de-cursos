using GestaoCurso.Infra.Data.Interfaces;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;

namespace GestaoCurso.Infra.Data
{
    public class ApplicationRepository<T> : IReadRepository<T>, IWriteRepository<T> where T : class
    {
        private readonly GestaoCursoDataContext _repositoryContext;
        private readonly DbSet<T> _dbSet;

        public ApplicationRepository(GestaoCursoDataContext repositoryContext)
        {
            _repositoryContext = repositoryContext ?? throw new ArgumentNullException(nameof(repositoryContext));
            _dbSet = _repositoryContext.Set<T>();
        }

        public IQueryable<T> FindAll()
        {
            return _dbSet.AsNoTracking();
        }

        public IQueryable<T> FindByCondition(Expression<Func<T, bool>> expression)
        {
            return _dbSet.Where(expression);
        }

        public bool Any(Expression<Func<T, bool>> expression)
        {
            return _dbSet.Any(expression);
        }

        public void Add(T entity)
        {
            _dbSet.Add(entity);
        }

        public void Delete(T entity)
        {
            _dbSet.Remove(entity);
        }

        public virtual async Task ExcluirAsync(int id)
        {
            var obj = await _dbSet.FindAsync(id);
            _dbSet.Remove(obj);
            await Task.CompletedTask;
        }

        public void Update(T entity)
        {
            _dbSet.Update(entity);
        }

        public async Task<T> AddAsync(T entity)
        {
            var result = await _dbSet.AddAsync(entity, new CancellationToken());

            return result.Entity;
        }
    }
}
