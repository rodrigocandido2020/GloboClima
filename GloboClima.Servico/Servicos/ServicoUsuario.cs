using Amazon.DynamoDBv2.DataModel;
using GloboClima.Dominio.Excecoes;
using GloboClima.Dominio.Models.Usuarios;

namespace GloboClima.Servico.Servicos
{
    public class ServicoUsuario
    {
        private readonly IDynamoDBContext _context;
        private readonly ServicoToken _tokenService;

        public ServicoUsuario(
            IDynamoDBContext context, 
            ServicoToken tokenService)
        {
            _context = context;
            _tokenService = tokenService;
        }

        public async Task CriarAsync(Usuario usuario)
        {
            await _context.SaveAsync(usuario);
        }

        public async Task<Usuario?> ObterPorId(string id)
        {
            var usuario = await _context.LoadAsync<Usuario>(id);
            return usuario;
        }

        public async Task<string> ValidarLoginEGerarToken(string NomeUsuario, string Senha)
        {
            var usuario = await _context.LoadAsync<Usuario>(NomeUsuario);

            if (usuario == null)
                throw new NotFoundException($"Usuário com Nome '{NomeUsuario}' não encontrado.");

            if (usuario.Senha != Senha)
                throw new UnauthorizedException("Usuário ou senha inválidos.");

            return _tokenService.GerarToken(usuario.Nome);
        }
    }
}
