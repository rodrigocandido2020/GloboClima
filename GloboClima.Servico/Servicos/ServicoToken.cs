using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace GloboClima.Servico.Servicos
{
    public class ServicoToken
    {
        private readonly string _secretKey;
        private readonly string _issuer;
        private readonly string _audience;

        public ServicoToken(string secretKey, string issuer, string audience)
        {
            _secretKey = secretKey;
            _issuer = issuer;
            _audience = audience;
        }

        public string GerarToken(string nomeUsuario)
        {
            var claims = new[]
            {
            new Claim(ClaimTypes.Name, nomeUsuario),
        };

            var chaveSecreta = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_secretKey));
            var credenciais = new SigningCredentials(chaveSecreta, SecurityAlgorithms.HmacSha256);

            var token = new JwtSecurityToken(
                issuer: _issuer,
                audience: _audience,
                claims: claims,
                expires: DateTime.Now.AddHours(8),
                signingCredentials: credenciais
            );

            return new JwtSecurityTokenHandler().WriteToken(token);
        }
    }
}
