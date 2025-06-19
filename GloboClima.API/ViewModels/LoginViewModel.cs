using System.ComponentModel.DataAnnotations;

namespace GloboClima.API.ViewModels
{
    public class LoginViewModel
    {
#nullable disable
        [Required(ErrorMessage = "Nome usuario é obrigatório.")]
        public string Nome { get; set; }

        [Required(ErrorMessage = "Senha é obrigatório.")]
        public string Senha { get; set; }
    }
}
