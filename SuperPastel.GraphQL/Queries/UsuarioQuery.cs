using GraphQL;
using GraphQL.Types;
using SuperPastel.Dominio.Entidades.Usuarios.Repositorio;
using SuperPastel.GraphQL.Types.Pessoas;
using SuperPastel.GraphQL.Types.Usuarios;
using SuperPastel.Nucleo.Ajudantes;

namespace SuperPastel.GraphQL.Queries
{
    public class UsuarioQuery : ObjectGraphType
    {
        public UsuarioQuery(IUsuarioRepositorio usuarioRepositorio)
        {
            Name = nameof(UsuarioQuery);

            Field<PessoaType>(
               "byId",
               description: "Obter pelo id",
               arguments: new QueryArguments(new QueryArgument<IdGraphType>
               {
                   Name = "id",
                   Description = "Id"
               }),
               resolve: context => usuarioRepositorio.ObterPorId(context.GetArgument<Guid>("id")))
                .AuthorizeWith(Policies.MANAGER);

            Field<UsuarioPagedInfoType>(
                "paged",
                description: "Obter todos os registros paginados",
                 arguments: new QueryArguments(new QueryArgument<IntGraphType>
                 {
                     Name = "index",
                     DefaultValue = 0,
                     Description = "Índice da página"
                 },
                new QueryArgument<IntGraphType>
                {
                    Name = "size",
                    DefaultValue = 30,
                    Description = "Tamanho da página"
                }),
                resolve: context =>
                {
                    return usuarioRepositorio.ObterPaginado(
                        context.GetArgument<int>("indice"),
                        context.GetArgument<int>("tamanho"));
                }).AuthorizeWith(Policies.MANAGER); ;

        }
    }
}
