using GestaoCurso.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace GestaoCurso.Infra.Mappings
{
    public class CursoMap : IEntityTypeConfiguration<Curso>
    {
        public void Configure(EntityTypeBuilder<Curso> builder)
        {
            builder.ToTable("Curso");

            builder.HasKey(x => x.Id);

            builder.Property(x => x.Imagem)
                .HasMaxLength(350)
                .IsRequired(false);

            builder.Property(x => x.Nome)
                .HasMaxLength(100)
                .IsRequired();

            builder.Property(x => x.Descricao)
                .HasMaxLength(250)
                .IsRequired();

            builder.Property(x => x.DataInicio)
                .IsRequired();

            builder.Property(x => x.DataFim)
                .IsRequired();

            builder.Property(x => x.QuantidadeDeAluno)
                .HasColumnType("BIGINT")
                .IsRequired();

            builder.Property(x => x.CategoriaId)
                .IsRequired();

            builder.HasOne(x => x.Categoria)
                .WithMany()
                .HasForeignKey(x => x.CategoriaId);
        }
    }
}
