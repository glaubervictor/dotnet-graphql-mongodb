using SuperPastel.Nucleo.Ajudantes;
using SuperPastel.Nucleo.Interfaces;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Configuration;
using System.Security.Claims;

namespace SuperPastel.GraphQL.Sessao
{
    public class SessaoDoUsuario : ISessaoDoUsuario
    {
        private readonly IHttpContextAccessor _accessor;
        private readonly IConfiguration _configuration;

        public SessaoDoUsuario(IHttpContextAccessor acessor,
            IConfiguration configuration)
        {
            _accessor = acessor;
            _configuration = configuration;
        }

        public Guid ObterUsuarioId()
        {
            if (EstaAutenticado())
            {
                var token = _accessor.HttpContext.Request.Headers["Authorization"].First().Replace("Bearer ", string.Empty);
                dynamic json = TokenHelper.Deserialize(token, _configuration["Authentication:SecretKey"]);

                if (Guid.TryParse(json.userId.Value, out Guid id))
                {
                    return id;
                }
            }

            return Guid.Empty;
        }

        public bool EhSuperUsuario()
        {
            if (EstaAutenticado())
            {
                var token = _accessor.HttpContext.Request.Headers["Authorization"].First().Replace("Bearer ", string.Empty);
                dynamic json = TokenHelper.Deserialize(token, _configuration["Authentication:SecretKey"]);

                if (!string.IsNullOrEmpty(json.superUser.Value))
                {
                    return json.superUser.Value == "true";
                }
            }

            return false;
        }

        public bool EstaAutenticado()
        {
            var identity = _accessor.HttpContext.User.Identity as ClaimsIdentity;
            return identity.IsAuthenticated;
        }
    }
}
