using System.ComponentModel.DataAnnotations;

namespace GloboClima.Web.Shared.ViewModels
{
    public class LoginViewModel
    {
        [Required(ErrorMessage = "Nome usuario é obrigatório.")]
        public string Nome { get; set; }

        [Required(ErrorMessage = "Senha é obrigatório.")]
        public string Senha { get; set; }
    }
}
