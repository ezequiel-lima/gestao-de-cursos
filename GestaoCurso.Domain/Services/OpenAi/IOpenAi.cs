namespace GestaoCurso.Domain.Services.OpenAi
{
    public interface IOpenAi
    {
        Task<string> GeradorDeDescricaoAsync(string nomeCurso);
    }
}
