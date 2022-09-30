using GraphQL.Types;
using SuperPastel.GraphQL.Models;
using SuperPastel.GraphQL.Types.Pessoas;

namespace SuperPastel.GraphQL.Types.Usuarios
{
    public class UsuarioInputType : InputObjectGraphType<UsuarioInput>, IGraphType
    {
        public UsuarioInputType()
        {
            Field(x => x.Email);
            Field(x => x.Senha);
            Field(x => x.Pessoa, type: typeof(PessoaInputType));
        }
    }
}
