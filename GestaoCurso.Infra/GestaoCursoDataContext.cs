using GestaoCurso.Domain.Entities;
using GestaoCurso.Infra.Mappings;
using Microsoft.EntityFrameworkCore;

namespace GestaoCurso.Infra
{
    public class GestaoCursoDataContext : DbContext
    {
        public GestaoCursoDataContext(DbContextOptions<GestaoCursoDataContext> options) : base (options) { }

        public DbSet<Curso> Cursos { get; set; }
        public DbSet<Categoria> Categorias { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfiguration(new CategoriaMap());
            modelBuilder.ApplyConfiguration(new CursoMap());
        }
    }
}
