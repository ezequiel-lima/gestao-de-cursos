using GestaoCurso.Domain.Entities;
using GestaoCurso.Domain.ViewModels.Categorias;

namespace GestaoCurso.Application.Services.Interfaces
{
    public interface ICategoriaService
    {
        Task<List<Categoria>> GetAtivos();
        Task<Categoria> GetById(Guid id);
        Task<Categoria> GetByNome(string nome);
        Task<Categoria> CreateCategoria(CreateCategoriaViewModel model);
        Task<Categoria> UpdateCategoria(Guid id, UpdateCategoriaViewModel model);
        Task<Categoria> PatchCategoria(Guid id);
    }
}
