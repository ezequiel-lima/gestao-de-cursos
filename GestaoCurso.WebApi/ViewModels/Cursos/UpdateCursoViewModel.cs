namespace GestaoCurso.WebApi.ViewModels.Cursos
{
    public class UpdateCursoViewModel
    {
        public string Imagem { get; set; }
        public string Nome { get; set; }
        public string Descricao { get; set; }
        public DateTime DataInicio { get; set; }
        public DateTime DataFim { get; set; }
        public int QuantidadeDeAluno { get; set; }
        public Guid CategoriaId { get; set; }
    }
}
