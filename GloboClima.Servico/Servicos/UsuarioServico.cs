using Amazon.DynamoDBv2.DataModel;
using GloboClima.Dominio.Models.Usuarios;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GloboClima.Servico.Servicos
{
    public class UsuarioServico
    {
        private readonly IDynamoDBContext _context;

        public UsuarioServico(IDynamoDBContext context)
        {
            _context = context;
        }

        public async Task CriarAsync(Usuario usuario)
        {
            await _context.SaveAsync(usuario);
        }

        public async Task<Usuario?> ObterPorIdAsync(string id)
        {
            return await _context.LoadAsync<Usuario>(id);
        }
    }
}
