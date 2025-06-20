using GloboClima.Dominio.Interfaces;
using GloboClima.Dominio.Models.Usuarios;

namespace GloboClima.Servico.Servicos
{
    public class CriarUsuarioAdmin
    {
        private readonly IServicoUsuario _usuarioServico;

        public CriarUsuarioAdmin(IServicoUsuario usuarioServico)
        {
            _usuarioServico = usuarioServico;
        }

        public async Task CriarUsuarioAdministrador()
        {
            var usuarioExistente = await _usuarioServico.ObterPorId("admin");

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
