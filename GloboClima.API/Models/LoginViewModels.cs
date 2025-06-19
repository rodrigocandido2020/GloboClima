using System.ComponentModel.DataAnnotations;

namespace GloboClima.API.Models
{
    public class LoginViewModels
    {
#nullable disable
        [Required(ErrorMessage = "O nome de usuário é obrigatório.")]
        public string NomeUsuario { get; set; }

        [Required(ErrorMessage = "A senha é obrigatória.")]
        public string Senha { get; set; }
    }

}