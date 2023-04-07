using GestaoCurso.Shared.Entities;

namespace GestaoCurso.Domain.Entities
{
    public class Curso : Entity
    {
        protected Curso() { }

        public Curso(string nome, string descricao, DateTime dataInicio, DateTime dataFim, int quantidadeDeAluno, Guid categoriaId)
        {
            Nome = nome;
            Descricao = descricao;
            DataInicio = dataInicio;
            DataFim = dataFim;
            QuantidadeDeAluno = quantidadeDeAluno;
            CategoriaId = categoriaId;
        }

        public string Imagem { get; private set; }
        public string Nome { get; private set; }
        public string Descricao { get; private set; }
        public DateTime DataInicio { get; private set; }
        public DateTime DataFim { get; private set; }
        public int QuantidadeDeAluno { get; private set; }
        public Guid CategoriaId { get; private set; }
        public Categoria Categoria { get; private set; }

        public void Alterar(string imagem, string nome, string descricao, DateTime dataInicio, DateTime dataFim, int quantidadeDeAluno, Guid categoriaId)
        {
            Imagem = imagem;
            Nome = nome;
            Descricao = descricao;
            DataInicio = dataInicio;
            DataFim = dataFim;
            QuantidadeDeAluno = quantidadeDeAluno;
            CategoriaId = categoriaId;
        }
    }
}
