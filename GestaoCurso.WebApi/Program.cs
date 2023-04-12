using GestaoCurso.Infra;
using GestaoCurso.Infra.Data.Interfaces;
using GestaoCurso.Infra.Data;
using Microsoft.EntityFrameworkCore;
using GestaoCurso.Domain.Entities;
using GestaoCurso.Application.Services.Interfaces;
using GestaoCurso.Application.Services.Categorias;
using GestaoCurso.Application.Services.Cursos;
using GestaoCurso.Application.Services.OpenAi;

var builder = WebApplication.CreateBuilder(args);

ConfigureServices(builder);

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();

void ConfigureServices(WebApplicationBuilder builder)
{
    var connectionString = builder.Configuration.GetConnectionString("DefaultConnection");
    builder.Services.AddDbContext<GestaoCursoDataContext>(options =>
    {
        options.UseSqlServer(connectionString, x => x.MigrationsAssembly("GestaoCurso.Infra"));
    });

    builder.Services.AddControllers();
    builder.Services.AddEndpointsApiExplorer();
    builder.Services.AddSwaggerGen();
    var services = GetServiceCollection(builder);
}

IServiceCollection GetServiceCollection(WebApplicationBuilder builder)
{
    // Adicionando serviços
    var services = builder.Services;
    services.AddScoped(typeof(IUnitOfWork), typeof(UnitOfWork));
    services.AddScoped(typeof(IOpenAi), typeof(OpenAi));
    services.AddScoped<IReadRepository<Categoria>, ApplicationRepository<Categoria>>();
    services.AddScoped<IWriteRepository<Categoria>, ApplicationRepository<Categoria>>();
    services.AddScoped<IReadRepository<Curso>, ApplicationRepository<Curso>>();
    services.AddScoped<IWriteRepository<Curso>, ApplicationRepository<Curso>>();
    services.AddScoped<ICategoriaService, CategoriaService>();
    services.AddScoped<ICursoService, CursoService>();

    return services;
}