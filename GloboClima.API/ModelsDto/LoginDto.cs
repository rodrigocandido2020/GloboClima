using System.ComponentModel.DataAnnotations;

namespace GloboClima.API.ModelDto
{
    public class LoginDto
    {
#nullable disable
        [Required(ErrorMessage = "Nome usuario é obrigatório.")]
        public string Nome { get; set; }

        [Required(ErrorMessage = "Senha é obrigatório.")]
        public string Senha { get; set; }
    }
}
