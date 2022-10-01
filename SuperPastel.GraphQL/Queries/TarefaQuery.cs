using GraphQL;
using GraphQL.Types;
using SuperPastel.Dominio.Entidades.Tarefas.Repositorio;
using SuperPastel.GraphQL.Types.Tarefas;
using SuperPastel.Nucleo.Ajudantes;
using SuperPastel.Nucleo.Interfaces;

namespace SuperPastel.GraphQL.Queries
{
    public class TarefaQuery : ObjectGraphType
    {
        public TarefaQuery(ITarefaRepositorio tarefaRepositorio, ISessaoDoUsuario sessao)
        {
            Name = nameof(TarefaQuery);

            Field<TarefaType>(
               "byId",
               description: "Obter pelo id",
               arguments: new QueryArguments(new QueryArgument<IdGraphType>
               {
                   Name = "id",
                   Description = "Id"
               }),
               resolve: context => tarefaRepositorio
                .ObterPorId(context.GetArgument<Guid>("id")))
                .AuthorizeWith(Policies.MANAGER);

            Field<TarefaPagedInfoType>(
                "paged",
                description: "Obter todos os registros",
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
                    return tarefaRepositorio.ObterPaginadoComUsuario(
                        context.GetArgument<int>("indice"),
                        context.GetArgument<int>("tamanho"));
                }).AuthorizeWith(Policies.MANAGER);

            Field<TarefaPagedInfoType>(
                "paged_by_user",
                description: "Obter todos os registros",
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
                    var usuarioId = sessao.ObterUsuarioId();
                    var pesquisa = context.GetArgument<string>("name");

                    return tarefaRepositorio.ObterPaginado(
                        context.GetArgument<int>("indice"),
                        context.GetArgument<int>("tamanho"),
                        x => x.UsuarioId == usuarioId);
                }).AuthorizeWith(Policies.USER);
        }
    }
}
