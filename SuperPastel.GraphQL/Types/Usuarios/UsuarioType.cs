using GraphQL.Types;
using SuperPastel.Dominio.Entidades.Usuarios;
using SuperPastel.GraphQL.Types.Pessoas;

namespace SuperPastel.GraphQL.Types.Usuarios
{
    public class UsuarioType : ObjectGraphType<Usuario>, IGraphType
    {
        public UsuarioType()
        {
            Field(x => x.Id, type: typeof(IdGraphType));
            Field(x => x.Pessoa, type: typeof(PessoaType));
            Field(x => x.Email);
            Field(x => x.SuperUsuario);
            Field(x => x.DataCadastro, type: typeof(DateTimeGraphType));
        }
    }
}
