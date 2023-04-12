using GestaoCurso.Application.Services.Interfaces;
using Microsoft.Extensions.Configuration;
using System.Text;
using System.Text.Json;

namespace GestaoCurso.Application.Services.OpenAi
{
    public class OpenAi : IOpenAi
    {
        private readonly string _api;
        private readonly IConfiguration _configuration;

        public OpenAi(IConfiguration configuration)
        {
            _configuration = configuration;
            _api = _configuration["OpenAI:ApiKey"];
        }

        public async Task<string> GeradorDeDescricaoAsync(string nomeCurso)
        {
            if (string.IsNullOrEmpty(nomeCurso))
                return "Não foi possível gerar uma descrição para o curso.";

            // modelo de linguagem a ser usado na geração de texto
            var modelo = "text-davinci-003";

            // prompt para a geração de texto
            var prompt = $"Escreva uma pequena descrição do curso de no maximo 50 caracteres, ele é sobre {nomeCurso}";

            using (var client = new HttpClient())
            {
                client.DefaultRequestHeaders.Add("Authorization", "Bearer " + _api);
                var response = await client.PostAsync("https://api.openai.com/v1/completions",
                    new StringContent("{\"model\": \"" + modelo + "\", \"prompt\": \"" + prompt + "\", \"temperature\": 1, \"max_tokens\": 512}", Encoding.UTF8, "application/json"));

                if (response.IsSuccessStatusCode)
                {
                    // passamos todo o conteudo do response para uma string
                    string conteudo = await response.Content.ReadAsStringAsync();

                    // Fazemos um Deserialize para obter somente o Choices
                    Resposta resposta = JsonSerializer.Deserialize<Resposta>(conteudo);

                    // retornamos uma string pegando o texto
                    return resposta?.choices?.FirstOrDefault()?.text.TrimStart('\n').Replace("\n", "");
                }
                else
                {
                    return "Não foi possível gerar uma descrição para o curso.";
                }
            }
        }

        public class Resposta
        {
            public List<Choice> choices { get; set; }

            public class Choice
            {
                public string text { get; set; }
            }
        }
    }
}
