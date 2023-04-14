using GestaoCurso.Domain.ViewModels.Cursos;

namespace GestaoCurso.Tests.ViewModels
{
    [TestClass]
    public class CursoViewModelTests
    {
        [TestMethod]
        [DataTestMethod]
        [DataRow("Programação", "2035-01-01", "2035-12-31", 30, "A8BB0629-2877-405F-B636-5303B715540A")]
        public void Deve_Retornar_Sucesso_Quando_Curso_For_Valido_Para_Criacao(string nome, string dataInicio, string dataFim, int quantidadeDeAluno, string categoriaId)
        {        
            var curso = new CreateCursoViewModel(nome, DateTime.Parse(dataInicio), DateTime.Parse(dataFim), quantidadeDeAluno, Guid.Parse(categoriaId));

            Assert.IsTrue(curso.IsValid);
        }

        [TestMethod]
        [DataTestMethod]
        [DataRow("", "2035-01-01", "2035-12-31", 30, "A8BB0629-2877-405F-B636-5303B715540A")]
        [DataRow("rRSHccAbeJxPHwEGRUGcQqPtSjMTyBDqxWqgigVFNUKASTRdASrGThMEnhYcaUhdcuiddUWfauUWPSjCEVQnkmzdfYWUdBEtZrEPF", "2035-01-01", "2035-12-31", 30, "A8BB0629-2877-405F-B636-5303B715540A")]
        [DataRow("Programação", "2022-01-01", "2035-12-31", 30, "A8BB0629-2877-405F-B636-5303B715540A")]
        [DataRow("Programação", "2035-02-02", "2035-01-01", 30, "A8BB0629-2877-405F-B636-5303B715540A")]
        [DataRow("Programação", "2035-01-01", "2035-12-31", -1, "A8BB0629-2877-405F-B636-5303B715540A")]
        public void Deve_Retornar_Erro_Quando_Curso_For_Invalido_Para_Criacao(string nome, string dataInicio, string dataFim, int quantidadeDeAluno, string categoriaId)
        {
            var curso = new CreateCursoViewModel(nome, DateTime.Parse(dataInicio), DateTime.Parse(dataFim), quantidadeDeAluno, Guid.Parse(categoriaId));

            Assert.IsFalse(curso.IsValid);
        }
    }
}
