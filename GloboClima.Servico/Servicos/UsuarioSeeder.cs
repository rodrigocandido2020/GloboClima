using GloboClima.Dominio.Models.Usuarios;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GloboClima.Servico.Servicos
{
    public class UsuarioSeeder
    {
        private readonly UsuarioServico _usuarioServico;

        public UsuarioSeeder(UsuarioServico usuarioServico)
        {
            _usuarioServico = usuarioServico;
        }

        public async Task CriarUsuarioAdminAsync()
        {
            var usuarioExistente = await _usuarioServico.ObterPorIdAsync("admin");

            if (usuarioExistente == null)
            {
                var admin = new Usuario
                {
                    Id = "admin",
                    Nome = "ADMIN",
                    Senha = "ADMIN" // ⚠️ Em produção, nunca armazene senha em texto puro!
                };

                await _usuarioServico.CriarAsync(admin);
                Console.WriteLine("Usuário ADMIN criado.");
            }
            else
            {
                Console.WriteLine("Usuário ADMIN já existe.");
            }
        }
    }
}
