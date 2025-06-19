using GloboClima.Dominio.Models.Usuarios;

namespace GloboClima.Servico.Servicos
{
    public class CriarUsuarioAdmin
    {
        private readonly ServicoUsuario _usuarioServico;

        public CriarUsuarioAdmin(ServicoUsuario usuarioServico)
        {
            _usuarioServico = usuarioServico;
        }

        public async Task CriarUsuarioAdminAsync()
        {
            var usuarioExistente = await _usuarioServico.ObterPorIdOuNomeAsync("admin");

            if (usuarioExistente == null)
            {
                var admin = new Usuario
                {
                    Id = "admin",
                    Nome = "admin",
                    Senha = "admin"
                };

                await _usuarioServico.CriarAsync(admin);
            }
        }
    }
}
