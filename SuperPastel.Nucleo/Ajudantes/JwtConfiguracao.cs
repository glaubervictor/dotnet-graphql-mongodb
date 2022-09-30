using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace SuperPastel.Nucleo.Ajudantes
{
    public class JwtConfiguracao
    {
        public string GerarToken(IConfiguration configuracao, Guid? usuarioId, DateTime expiracao, bool superUsuario, string[] papeis)
        {
            var issuer = configuracao["Authentication:Issuer"];
            if (string.IsNullOrWhiteSpace(issuer))
            {
                throw new Exception("Missing Authentication:Issuer value");
            }

            var audience = configuracao["Authentication:Audience"];
            if (string.IsNullOrWhiteSpace(audience))
            {
                throw new Exception("Missing Authentication:Audience value");
            }

            var secretKey = configuracao["Authentication:SecretKey"];
            if (string.IsNullOrWhiteSpace(secretKey))
            {
                throw new Exception("Missing Authentication:SecretKey value");
            }

            var claims = new List<Claim>
            {
                new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
                new Claim("userId", usuarioId.HasValue ? usuarioId.Value.ToString() : string.Empty),
                new Claim("superUser", superUsuario ? "true" : "false")
            };

            foreach (var role in papeis)
            {
                claims.Add(new Claim(ClaimTypes.Role, role));
            }

            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(secretKey));

            var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha512Signature);

            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(claims),
                Expires = expiracao,
                SigningCredentials = creds,
                Issuer = issuer,
                Audience = audience
            };

            var tokenHandler = new JwtSecurityTokenHandler();
            var token = tokenHandler.CreateToken(tokenDescriptor);

            return tokenHandler.WriteToken(token);
        }
    }
}
