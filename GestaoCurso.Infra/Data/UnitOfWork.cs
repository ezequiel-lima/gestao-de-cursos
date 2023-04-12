using GestaoCurso.Infra.Data.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace GestaoCurso.Infra.Data
{
    public class UnitOfWork : IRepositoryFactory, IUnitOfWork, IDisposable
    {
        private readonly IServiceProvider _serviceProvider;
        private Dictionary<Type, object> _serviceProviderCache;

        public UnitOfWork(GestaoCursoDataContext dbContext, IServiceProvider serviceProvider)
        {
            Context = dbContext ?? throw new ArgumentNullException(nameof(dbContext));
            _serviceProvider = serviceProvider ?? throw new ArgumentNullException(nameof(serviceProvider));
        }

        public DbContext Context { get; }

        public int Commit()
        {
            return Context.SaveChanges();
        }

        public async Task<int> CommitAsync()
        {
            return await Context.SaveChangesAsync();
        }

        public void Dispose()
        {
            Context.Dispose();
        }

        public IReadRepository<TEntity> GetReadRepository<TEntity>() where TEntity : class
        {
            if (_serviceProviderCache == null)
                _serviceProviderCache = new Dictionary<Type, object>();

            var type = typeof(IReadRepository<TEntity>);
            if (!_serviceProviderCache.ContainsKey(type))
                _serviceProviderCache[type] = _serviceProvider.GetService(type);

            return _serviceProviderCache[type] as IReadRepository<TEntity>;
        }

        public IWriteRepository<TEntity> GetWriteRepository<TEntity>() where TEntity : class
        {
            if (_serviceProviderCache == null)
                _serviceProviderCache = new Dictionary<Type, object>();

            var type = typeof(IWriteRepository<TEntity>);
            if (!_serviceProviderCache.ContainsKey(type))
                _serviceProviderCache[type] = _serviceProvider.GetService(type);

            return _serviceProviderCache[type] as IWriteRepository<TEntity>;
        }
    }
}
