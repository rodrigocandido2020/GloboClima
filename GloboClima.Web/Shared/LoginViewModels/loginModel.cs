using System.ComponentModel.DataAnnotations;

namespace GloboClima.Web.Shared.LoginViewModels
{
    public class loginModel
    {
        [Required(ErrorMessage = "Nome usuario é obrigatório.")]
        public string Nome { get; set; }

        [Required(ErrorMessage = "Senha é obrigatório.")]
        public string Senha { get; set; }
    }
}
