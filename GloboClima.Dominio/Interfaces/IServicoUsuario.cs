using GloboClima.Dominio.Models.Usuarios;

namespace GloboClima.Dominio.Interfaces
{
    public interface IServicoUsuario
    {
        Task CriarAsync(Usuario usuario);
        Task<Usuario?> ObterPorId(string id);
        Task<string> ValidarLoginEGerarToken(string nomeUsuario, string senha);
    }
}
