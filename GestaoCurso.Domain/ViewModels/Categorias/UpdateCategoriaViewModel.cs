using Flunt.Notifications;
using Flunt.Validations;
using GestaoCurso.Shared.ViewModels;

namespace GestaoCurso.Domain.ViewModels.Categorias
{
    public class UpdateCategoriaViewModel : ViewModel
    {
        public UpdateCategoriaViewModel(string nome)
        {
            Nome = nome;

            AddNotifications(new Contract<Notification>()
                .Requires()
                .IsNotNullOrEmpty(Nome, "Nome", "Nome não pode ser nulo ou vazio")
                .IsLowerOrEqualsThan(Nome, 100, "Nome", "Nome precisa ter no maximo 100 caracteres")
            );
        }

        public string Nome { get; set; }
    }
}
