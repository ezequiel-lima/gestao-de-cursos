using GestaoCurso.Application.Services.Interfaces;
using GestaoCurso.Domain.Entities;
using GestaoCurso.Domain.ViewModels;
using GestaoCurso.Domain.ViewModels.Categorias;
using Microsoft.AspNetCore.Mvc;

namespace GestaoCurso.WebApi.Controllers
{
    [ApiController]
    public class CategoriaController : ControllerBase
    {
        private readonly ICategoriaService _categoriaService;

        public CategoriaController(ICategoriaService categoriaService)
        {
            _categoriaService = categoriaService;
        }

        [HttpGet("api/categorias")]
        public async Task<IActionResult> GeyAsync()
        {
            try
            {
                var categorias = await _categoriaService.GetAtivos();
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
                var categoria = await _categoriaService.GetById(id);
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
                var categoria = await _categoriaService.GetByNome(nome);
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
                    return BadRequest(new ResultViewModel<dynamic>(model.Notifications.ToList()));

                var categoria = await _categoriaService.CreateCategoria(model);

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
                if (!model.IsValid)
                    return BadRequest(new ResultViewModel<dynamic>(model.Notifications.ToList()));

                var categoria = await _categoriaService.UpdateCategoria(id, model);

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
                var categoria = await _categoriaService.PatchCategoria(id);
                return Ok(new ResultViewModel<Categoria>(categoria));
            }
            catch 
            {
                return StatusCode(500, new ResultViewModel<Categoria>("01X06 - Falha interna no servidor"));
            }
        }
    }
}
