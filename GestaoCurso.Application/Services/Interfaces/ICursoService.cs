using GestaoCurso.Domain.Entities;
using GestaoCurso.Domain.ViewModels.Cursos;

namespace GestaoCurso.Application.Services.Interfaces
{
    public interface ICursoService
    {
        Task<List<Curso>> GetAll();
        Task<Curso> GetById(Guid id);
        Task<List<Curso>> GetCursoByCategoria(string nome);
        Task<Curso> GetByNome(string nome);
        Task<Curso> CreateCurso(CreateCursoViewModel model);
        Task<Curso> UpdateCurso(Guid id, UpdateCursoViewModel model);
        Task<Curso> DeleteCurso(Guid id);
    }
}
