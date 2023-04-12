using GestaoCurso.Application.Services.Interfaces;
using GestaoCurso.Domain.Entities;
using GestaoCurso.Domain.ViewModels;
using GestaoCurso.Domain.ViewModels.Cursos;
using Microsoft.AspNetCore.Mvc;

namespace GestaoCurso.WebApi.Controllers
{
    [ApiController]
    public class CursoController : ControllerBase
    {
        private readonly ICursoService _cursoService;

        public CursoController(ICursoService cursoService)
        {
            _cursoService = cursoService;
        }

        [HttpGet("api/cursos")]
        public async Task<IActionResult> GetAsync()
        {
            try
            {
                var cursos = await _cursoService.GetAll();
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
                var curso = await _cursoService.GetById(id);
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
                var cursos = await _cursoService.GetCursoByCategoria(nome);
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
                var curso =  await _cursoService.GetByNome(nome);
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
                if (!model.IsValid)
                    return BadRequest(new ResultViewModel<dynamic>(model.Notifications.ToList()));

                var curso = await _cursoService.CreateCurso(model);

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
                if (!model.IsValid)
                    return BadRequest(new ResultViewModel<dynamic>(model.Notifications.ToList()));

                var curso = await _cursoService.UpdateCurso(id, model);

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
                var curso = await _cursoService.DeleteCurso(id);
                return Ok(new ResultViewModel<Curso>(curso));
            }
            catch 
            {
                return StatusCode(500, new ResultViewModel<Curso>("0CX07 - Falha interna no servidor"));
            }
        }
    }
}
