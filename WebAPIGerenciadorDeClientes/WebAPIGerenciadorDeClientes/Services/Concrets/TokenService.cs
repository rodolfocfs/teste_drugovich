using WebAPIGerenciadorDeClientes.Services.Contracts;
using System.IdentityModel.Tokens.Jwt;
using WebAPIGerenciadorDeClientes.Models;
using System.Text;
using System.Security.Claims;
using Microsoft.IdentityModel.Tokens;

namespace WebAPIGerenciadorDeClientes.Services.Concrets
{
    public class TokenService : ITokenService
    {
        private readonly string _tokenSecret;
        private readonly int _validFor;

        public TokenService(IConfiguration config)
        {
            _tokenSecret = config["Token:Secret"];
            _validFor = int.Parse(config["Token:ValidFor"]);
        }

        public TokenResponse GenerateToken(Gerente gerente)
        {
            var handler = new JwtSecurityTokenHandler();
            var key = Encoding.ASCII.GetBytes(_tokenSecret);
            var descriptor = CreateTokenDescriptor(gerente, key, _validFor);

            var token = handler.CreateToken(descriptor);

            return new TokenResponse
            {
                Token = handler.WriteToken(token),
                Expiration = token.ValidTo.ToLocalTime()
            };
        }

        private static SecurityTokenDescriptor CreateTokenDescriptor(Gerente gerente, byte[] key, long tokenValidity)
        {
            var descriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(new[]
                {
                    new Claim(ClaimTypes.Name, gerente.Nome),
                    new Claim(ClaimTypes.Email, gerente.Email)
                }),
                Expires = DateTime.UtcNow.AddSeconds(tokenValidity),
                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key),
                    SecurityAlgorithms.HmacSha256Signature)
            };

            return descriptor;
        }
    }
}
