using GestaoCurso.Application.Services.Interfaces;
using GestaoCurso.Domain.Entities;
using GestaoCurso.Domain.ViewModels.Cursos;
using GestaoCurso.Infra.Data.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace GestaoCurso.Application.Services.Cursos
{
    public class CursoService : ICursoService
    {
        private readonly IReadRepository<Curso> _readRepository;
        private readonly IWriteRepository<Curso> _writeRepository;
        private readonly IOpenAi _openAI;
        private readonly IUnitOfWork _unitOfWork;

        public CursoService(IReadRepository<Curso> readRepository, IWriteRepository<Curso> writeRepository, IUnitOfWork unitOfWork, IOpenAi openAI)
        {
            _readRepository = readRepository;
            _writeRepository = writeRepository;
            _unitOfWork = unitOfWork;
            _openAI = openAI;
        }

        public async Task<List<Curso>> GetAll()
        {
            return await _readRepository.FindAll().Include(x => x.Categoria).ToListAsync();
        }

        public async Task<Curso> GetById(Guid id)
        {
            var curso = await _readRepository.FindByCondition(x => x.Id == id).Include(x => x.Categoria).FirstOrDefaultAsync();

            if (curso is null)
                throw new Exception("Curso não encontrado");

            return curso;
        }

        public async Task<List<Curso>> GetCursoByCategoria(string nome)
        {
            var curso = await _readRepository.FindByCondition(x => x.Categoria.Nome.ToUpper() == nome.ToUpper()).Include(x => x.Categoria).ToListAsync();

            if (curso is null)
                throw new Exception("Curso não encontrado");

            return curso;            
        }

        public async Task<Curso> GetByNome(string nome)
        {
            var curso = await _readRepository.FindByCondition(x => x.Nome == nome).Include(x => x.Categoria).FirstOrDefaultAsync();

            if (curso is null)
                throw new Exception("Curso não encontrado");

            return curso;
        }

        public async Task<Curso> CreateCurso(CreateCursoViewModel model)
        {
            var descricao = await _openAI.GeradorDeDescricaoAsync(model.Nome);

            var curso = new Curso(model.Nome, descricao, model.DataInicio, model.DataFim, model.QuantidadeDeAluno, model.CategoriaId);
            await _writeRepository.AddAsync(curso);
            await _unitOfWork.CommitAsync();

            return curso;
        }

        public async Task<Curso> UpdateCurso(Guid id, UpdateCursoViewModel model)
        {
            var curso = await _readRepository.FindByCondition(x => x.Id == id).FirstOrDefaultAsync();

            if (curso is null)
                throw new Exception("Curso não encontrado");

            curso.Alterar(model.Imagem, model.Nome, model.Descricao, model.DataInicio, model.DataFim, model.QuantidadeDeAluno, model.CategoriaId);

            _writeRepository.Update(curso);
            await _unitOfWork.CommitAsync();

            return curso;           
        }

        public async Task<Curso> DeleteCurso(Guid id)
        {
            var curso = await _readRepository.FindByCondition(x => x.Id == id).FirstOrDefaultAsync();

            if (curso is null)
                throw new Exception("Curso não encontrado");

            _writeRepository.Delete(curso);
            await _unitOfWork.CommitAsync();

            return curso;
        }
    }
}
