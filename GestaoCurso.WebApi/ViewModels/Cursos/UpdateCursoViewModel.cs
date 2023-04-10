using Flunt.Notifications;
using Flunt.Validations;
using GestaoCurso.Shared.ViewModels;

namespace GestaoCurso.WebApi.ViewModels.Cursos
{
    public class UpdateCursoViewModel : ViewModel
    {
        public UpdateCursoViewModel(string imagem, string nome, string descricao, DateTime dataInicio, DateTime dataFim, int quantidadeDeAluno, Guid categoriaId)
        {
            Imagem = imagem;
            Nome = nome;
            Descricao = descricao;
            DataInicio = dataInicio;
            DataFim = dataFim;
            QuantidadeDeAluno = quantidadeDeAluno;
            CategoriaId = categoriaId;

            AddNotifications(new Contract<Notification>()
                .Requires()
                .IsUrlOrEmpty(Imagem, "Imagem", "A imagem precisa ser uma Url")
                .IsNotNullOrEmpty(Nome, "Nome", "Nome não pode ser nulo ou vazio")
                .IsLowerOrEqualsThan(Nome, 100, "Nome", "Nome precisa ter no maximo 100 caracteres")
                .IsGreaterOrEqualsThan(DataInicio, DateTime.Now, "DataInicio", "Data de inicio do curso precisa ser uma data futura")
                .IsGreaterThan(DataFim, DataInicio, "DataFim", "Data de término do curso precisa ser maior que a data de inicio")
                .IsGreaterOrEqualsThan(QuantidadeDeAluno, 0, "QuantidadeDeAluno", "A quantidade de aluno tem que ser positiva")
                .IsNotEmpty(CategoriaId, "CategoriaId", "O curso precisa de uma categoria")
            );
        }

        public string Imagem { get; set; }
        public string Nome { get; set; }
        public string Descricao { get; set; }
        public DateTime DataInicio { get; set; }
        public DateTime DataFim { get; set; }
        public int QuantidadeDeAluno { get; set; }
        public Guid CategoriaId { get; set; }
    }
}
