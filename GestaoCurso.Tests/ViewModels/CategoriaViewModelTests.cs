using GestaoCurso.Domain.ViewModels.Categorias;

namespace GestaoCurso.Tests.ViewModels
{
    [TestClass]
    public class CategoriaViewModelTests
    {       
        [TestMethod]
        [DataTestMethod]
        [DataRow("Programação")]
        [DataRow("Desenvolvimento")]
        [DataRow("Arte")]
        public void Deve_Retornar_Sucesso_Quando_Categoria_For_Valida_Para_Criacao(string nome)
        {
            var categoria = new CreateCategoriaViewModel(nome);

            Assert.IsTrue(categoria.IsValid);
        }

        [TestMethod]
        [DataTestMethod]
        [DataRow("")]
        [DataRow("rRSHccAbeJxPHwEGRUGcQqPtSjMTyBDqxWqgigVFNUKASTRdASrGThMEnhYcaUhdcuiddUWfauUWPSjCEVQnkmzdfYWUdBEtZrEPF")]
        public void Deve_Retornar_Erro_Quando_Categoria_For_Invalida_Para_Criacao(string nome)
        {
            var categoria = new CreateCategoriaViewModel(nome);

            Assert.IsFalse(categoria.IsValid);
        }

        [TestMethod]
        [DataTestMethod]
        [DataRow("Programação")]
        [DataRow("Desenvolvimento")]
        [DataRow("Arte")]
        public void Deve_Retornar_Sucesso_Quando_Categoria_For_Valida_Para_Atualizacao(string nome)
        {
            var categoria = new UpdateCategoriaViewModel(nome);

            Assert.IsTrue(categoria.IsValid);
        }

        [TestMethod]
        [DataTestMethod]
        [DataRow("")]
        [DataRow("rRSHccAbeJxPHwEGRUGcQqPtSjMTyBDqxWqgigVFNUKASTRdASrGThMEnhYcaUhdcuiddUWfauUWPSjCEVQnkmzdfYWUdBEtZrEPF")]
        public void Deve_Retornar_Erro_Quando_Categoria_For_Invalida_Para_Atualizacao(string nome)
        {
            var categoria = new UpdateCategoriaViewModel(nome);

            Assert.IsFalse(categoria.IsValid);
        }
    }
}
