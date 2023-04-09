using GestaoCurso.Domain.Entities;
using GestaoCurso.Infra;
using GestaoCurso.WebApi.ViewModels;
using GestaoCurso.WebApi.ViewModels.Categorias;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace GestaoCurso.WebApi.Controllers
{
    [ApiController]
    public class CategoriaController : ControllerBase
    {
        private readonly GestaoCursoDataContext _context;

        public CategoriaController(GestaoCursoDataContext context)
        {
            _context = context;
        }

        [HttpGet("api/categorias")]
        public async Task<IActionResult> GeyAsync()
        {
            try
            {
                var categorias = await _context.Categorias.AsNoTracking().Where(x => x.Ativo == true).ToListAsync();
                return Ok(new ResultViewModel<List<Categoria>>(categorias));
            }
            catch 
            {
                return StatusCode(500, new ResultViewModel<List<Categoria>>("01X01 - Falha interna no servidor"));
            }
        }

        [HttpGet("api/categorias/{id:Guid}")]
        public async Task<IActionResult> GetAsyncById(Guid id)
        {
            try
            {
                var categoria = await _context.Categorias.AsNoTracking().Where(x => x.Id == id).FirstOrDefaultAsync();

                if (categoria is null)
                    return NotFound(new ResultViewModel<Categoria>("Categoria não encontrada"));

                return Ok(new ResultViewModel<Categoria>(categoria));
            }
            catch
            {
                return StatusCode(500, new ResultViewModel<Categoria>("01x02 - Falha interna no servidor"));
            }
        }

        [HttpGet("api/categorias/{nome}")]
        public async Task<IActionResult> GetAsyncByName(string nome)
        {
            try
            {
                var categoria = await _context.Categorias.AsNoTracking().Where(x => x.Nome == nome).FirstOrDefaultAsync();

                if (categoria is null)
                    return NotFound(new ResultViewModel<Categoria>("Categoria não encontrada"));

                return Ok(new ResultViewModel<Categoria>(categoria));
            }
            catch 
            {
                return StatusCode(500, new ResultViewModel<Categoria>("01x03 - Falha interna no servidor"));
            }
        }

        [HttpPost("api/categorias")]
        public async Task<IActionResult> PostAsync(CreateCategoriaViewModel model)
        {
            try
            {
                if (!model.IsValid)
                    return BadRequest(model.Notifications);                           

                var categoria = new Categoria(model.Nome);
                await _context.Categorias.AddAsync(categoria);
                await _context.SaveChangesAsync();

                return Created($"categorias/{categoria.Id}", new ResultViewModel<Categoria>(categoria));
            }
            catch 
            {
                return StatusCode(500, new ResultViewModel<Categoria>("01X04 - Falha interna no servidor"));
            }
        }

        [HttpPut("api/categorias/{id:Guid}")]
        public async Task<IActionResult> PutAsync(Guid id, UpdateCategoriaViewModel model)
        {
            try
            {
                var categoria = await _context.Categorias.Where(x => x.Id == id).FirstOrDefaultAsync();

                if (categoria is null)
                    return NotFound(new ResultViewModel<Categoria>("Categoria não encontrada"));

                categoria.Alterar(model.Nome);

                _context.Categorias.Update(categoria);
                await _context.SaveChangesAsync();

                return Ok(new ResultViewModel<Categoria>(categoria));
            }
            catch 
            {
                return StatusCode(500, new ResultViewModel<Categoria>("01X05 - Falha interna no servidor"));
            }
        }

        [HttpPatch("api/categorias/{id:Guid}")]
        public async Task<IActionResult> PatchAsync(Guid id)
        {
            try
            {
                var categoria = await _context.Categorias.Where(x => x.Id == id).FirstOrDefaultAsync();

                if (categoria is null)
                    return NotFound(new ResultViewModel<Categoria>("Categoria não encontrada"));

                categoria.Alterar();

                _context.Categorias.Update(categoria);
                await _context.SaveChangesAsync();

                return Ok(new ResultViewModel<Categoria>(categoria));
            }
            catch 
            {
                return StatusCode(500, new ResultViewModel<Categoria>("01X06 - Falha interna no servidor"));
            }
        }
    }
}
