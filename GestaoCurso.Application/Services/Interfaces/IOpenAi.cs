namespace GestaoCurso.Application.Services.Interfaces
{
    public interface IOpenAi
    {
        Task<string> GeradorDeDescricaoAsync(string nomeCurso);
    }
}
