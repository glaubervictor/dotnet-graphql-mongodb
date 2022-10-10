using GraphQL.Types;
using SuperPastel.Dominio.Entidades.Usuarios;
using SuperPastel.Nucleo.Base;

namespace SuperPastel.GraphQL.Types.Usuarios
{
    public class UsuarioPagedInfoType : ObjectGraphType<PageInfo<Usuario>>
    {
        public UsuarioPagedInfoType()
        {
            Field<ListGraphType<UsuarioType>>("items", resolve: context => context.Source.List);
            Field(x => x.PageCount);
            Field(x => x.Size);
            Field(x => x.TotalCount);
        }
    }
}
