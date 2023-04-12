using GestaoCurso.Application.Services.Interfaces;
using GestaoCurso.Domain.Entities;
using GestaoCurso.Domain.ViewModels.Categorias;
using GestaoCurso.Infra.Data.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace GestaoCurso.Application.Services.Categorias
{
    public class CategoriaService : ICategoriaService
    {
        private readonly IReadRepository<Categoria> _readRepository;
        private readonly IWriteRepository<Categoria> _writeRepository;
        private readonly IUnitOfWork _unitOfWork;

        public CategoriaService(IReadRepository<Categoria> readRepository, IWriteRepository<Categoria> writeRepository, IUnitOfWork unitOfWork)
        {
            _readRepository = readRepository;
            _writeRepository = writeRepository;
            _unitOfWork = unitOfWork;
        }

        public async Task<List<Categoria>> GetAtivos()
        {
             return await _readRepository.FindByCondition(x => x.Ativo == true).ToListAsync();
        }

        public async Task<Categoria> GetById(Guid id)
        {
            var categoria = await _readRepository.FindByCondition(x => x.Id == id).FirstOrDefaultAsync();

            if (categoria is null)
                throw new Exception("Categoria não encontrada");

            return categoria;
        }

        public async Task<Categoria> GetByNome(string nome)
        {
            var categoria = await _readRepository.FindByCondition(x => x.Nome == nome).FirstOrDefaultAsync();

            if (categoria is null)
                throw new Exception("Categoria não encontrada");

            return categoria;
        }

        public async Task<Categoria> CreateCategoria(CreateCategoriaViewModel model)
        {
            var categoria = new Categoria(model.Nome);
            await _writeRepository.AddAsync(categoria);
            await _unitOfWork.CommitAsync();
            return categoria;
        }

        public async Task<Categoria> UpdateCategoria(Guid id, UpdateCategoriaViewModel model)
        {
            var categoria = await _readRepository.FindByCondition(x => x.Id == id).FirstOrDefaultAsync();

            if (categoria is null)
                throw new Exception("Categoria não encontrada");

            categoria.Alterar(model.Nome);

            _writeRepository.Update(categoria);
            await _unitOfWork.CommitAsync();

            return categoria;
        }

        public async Task<Categoria> PatchCategoria(Guid id)
        {
            var categoria = await _readRepository.FindByCondition(x => x.Id == id).FirstOrDefaultAsync();

            if (categoria is null)
                throw new Exception("Categoria não encontrada");

            categoria.Alterar();

            _writeRepository.Update(categoria);
            await _unitOfWork.CommitAsync();

            return categoria;
        }
    }
}
