namespace GestaoCurso.Shared.Services.OpenAi
{
    public interface IOpenAI
    {
        Task<string> GeradorDeDescricaoAsync(string nomeCurso);
    }
}
