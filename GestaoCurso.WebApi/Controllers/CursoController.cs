using GestaoCurso.Domain.Entities;
using GestaoCurso.Domain.Services.OpenAi;
using GestaoCurso.Infra;
using GestaoCurso.WebApi.ViewModels;
using GestaoCurso.WebApi.ViewModels.Cursos;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace GestaoCurso.WebApi.Controllers
{
    [ApiController]
    public class CursoController : ControllerBase
    {
        private readonly GestaoCursoDataContext _context;
        private readonly IOpenAi _openAI;

        public CursoController(GestaoCursoDataContext context, IOpenAi openAI)
        {
            _context = context;
            _openAI = openAI;
        }

        [HttpGet("api/cursos")]
        public async Task<IActionResult> GeyAsync()
        {
            try
            {
                var cursos = await _context.Cursos.AsNoTracking().Include(x => x.Categoria).ToListAsync();
                return Ok(new ResultViewModel<List<Curso>>(cursos));
            }
            catch
            {
                return StatusCode(500, new ResultViewModel<List<Curso>>("0CX01 - Falha interna no servidor"));
            }
        }

        [HttpGet("api/cursos/{id:Guid}")]
        public async Task<IActionResult> GetAsyncById(Guid id)
        {
            try
            {
                var curso = await _context.Cursos.AsNoTracking().Where(x => x.Id == id).Include(x => x.Categoria).FirstOrDefaultAsync();

                if (curso is null)
                    return NotFound(new ResultViewModel<Curso>("Curso não encontrado"));

                return Ok(new ResultViewModel<Curso>(curso));
            }
            catch
            {
                return StatusCode(500, new ResultViewModel<Curso>("0Cx02 - Falha interna no servidor"));
            }
        }

        [HttpGet("api/cursos/by-categoria")]
        public async Task<IActionResult> GetAsyncByCategoria(string nome)
        {
            try
            {
                var cursos = await _context.Cursos.AsNoTracking()
                    .Where(x => x.Categoria.Nome.ToUpper() == nome.ToUpper())
                    .Include(x => x.Categoria).ToListAsync();

                if (cursos is null)
                    return NotFound(new ResultViewModel<List<Curso>>("Curso não encontrado"));

                return Ok(new ResultViewModel<List<Curso>>(cursos));
            }
            catch
            {
                return StatusCode(500, new ResultViewModel<List<Curso>>("0Cx08 - Falha interna no servidor"));
            }
        }

        [HttpGet("api/cursos/{nome}")]
        public async Task<IActionResult> GetAsyncByName(string nome)
        {
            try
            {
                var curso = await _context.Cursos.AsNoTracking().Where(x => x.Nome == nome).Include(x => x.Categoria).FirstOrDefaultAsync();

                if (curso is null)
                    return NotFound(new ResultViewModel<Curso>("Curso não encontrado"));

                return Ok(new ResultViewModel<Curso>(curso));
            }
            catch
            {
                return StatusCode(500, new ResultViewModel<Curso>("0Cx03 - Falha interna no servidor"));
            }
        }

        [HttpPost("api/cursos")]
        public async Task<IActionResult> PostAsync(CreateCursoViewModel model)
        {
            try
            {
                var descricao = await _openAI.GeradorDeDescricaoAsync(model.Nome);

                var curso = new Curso(model.Nome, descricao, model.DataInicio, model.DataFim, model.QuantidadeDeAluno, model.CategoriaId);
                await _context.Cursos.AddAsync(curso);
                await _context.SaveChangesAsync();

                return Created($"cursos/{curso.Id}", new ResultViewModel<Curso>(curso));
            }
            catch
            {
                return StatusCode(500, new ResultViewModel<Curso>("0CX04 - Falha interna no servidor"));
            }
        }

        [HttpPut("api/cursos/{id:Guid}")]
        public async Task<IActionResult> PutAsync(Guid id, UpdateCursoViewModel model)
        {
            try
            {
                var curso = await _context.Cursos.Where(x => x.Id == id).FirstOrDefaultAsync();

                if (curso is null)
                    return NotFound(new ResultViewModel<Curso>("Curso não encontrado"));

                curso.Alterar(model.Imagem, model.Nome, model.Descricao, model.DataInicio, model.DataFim, model.QuantidadeDeAluno, model.CategoriaId);

                _context.Cursos.Update(curso);
                await _context.SaveChangesAsync();

                return Ok(new ResultViewModel<Curso>(curso));
            }
            catch
            {
                return StatusCode(500, new ResultViewModel<Curso>("0CX05 - Falha interna no servidor"));
            }
        }

        [HttpDelete("api/cursos/{id:Guid}")]
        public async Task<IActionResult> DeleteAsync(Guid id)
        {
            try
            {
                var curso = await _context.Cursos.AsNoTracking().Where(x => x.Id == id).FirstOrDefaultAsync();

                if (curso is null)
                    return NotFound(new ResultViewModel<Curso>("Curso não encontrado"));

                _context.Cursos.Remove(curso);
                await _context.SaveChangesAsync();

                return Ok(new ResultViewModel<Curso>(curso));
            }
            catch 
            {
                return StatusCode(500, new ResultViewModel<Curso>("0CX07 - Falha interna no servidor"));
            }
        }
    }
}
