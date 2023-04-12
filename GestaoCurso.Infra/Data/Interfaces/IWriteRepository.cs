namespace GestaoCurso.Infra.Data.Interfaces
{
    public interface IWriteRepository<T> where T : class
    {
        void Add(T entity);
        Task<T> AddAsync(T entity);
        void Delete(T entity);
        void Update(T entity);
        Task ExcluirAsync(int id);
    }
}
