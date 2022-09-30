using SuperPastel.Dominio.Entidades.Autenticacoes;
using SuperPastel.GraphQL.Base;
using GraphQL.Types;

namespace SuperPastel.GraphQL.Types.Autenticacoes
{
    public class LoginInputType : InputObjectGraphType<Login>, IGraphQL
    {
        public LoginInputType()
        {
            Name = nameof(LoginInputType);

            Field(x => x.Email);
            Field(x => x.Senha);
        }
    }
}
