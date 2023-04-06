using GestaoCurso.Shared.Entities;

namespace GestaoCurso.Domain.Entities
{
    public class Categoria : Entity
    {
        protected Categoria() { }

        public Categoria(string nome)
        {
            Nome = nome;
            Ativo = true;
        }

        public string Nome { get; private set; }
        public bool Ativo { get; private set; }

        public void Alterar(string nome)
        {
            Nome = nome;
        }

        public void Alterar()
        {
            Ativo = !Ativo;
        }
    }
}
