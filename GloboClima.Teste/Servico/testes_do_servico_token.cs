using GloboClima.Servico.Servicos;

namespace GloboClima.Teste.Servico
{
    public class testes_do_servico_token
    {
        [Fact]
        public void GerarToken_DeveRetornarTokenValido()
        {
            var secretKey = "super-secreto-token-chave-segura-123456";
            var issuer = "GloboClima";
            var audience = "GloboClimaUsers";
            var nomeUsuario = "admin";

            var servicoToken = new ServicoToken(secretKey, issuer, audience);

            var token = servicoToken.GerarToken(nomeUsuario);

            Assert.False(string.IsNullOrWhiteSpace(token));
            Assert.Contains(".", token);
        }

    }
}
