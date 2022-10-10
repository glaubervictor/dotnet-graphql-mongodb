using SuperPastel.Dominio.Entidades.Autenticacoes;
using SuperPastel.GraphQL.Base;
using GraphQL.Types;

namespace SuperPastel.GraphQL.Types.Autenticacoes
{
    public class AutenticacaoType : ObjectGraphType<Autenticacao>, IGraphQL
    {
        public AutenticacaoType()
        {
            Name = nameof(AutenticacaoType);

            Field(x => x.Perfil);
            Field(x => x.Token);
            Field(x => x.ValidoAte, type: typeof(DateTimeGraphType));
        }
    }
}
