using GraphQL.Types;

namespace SuperPastel.GraphQL.Queries
{
    public class Query : ObjectGraphType, IGraphType
    {
        public Query()
        {
            Name = "queries";
            Description = "Queries GraphQL";

            Field<UsuarioQuery>("usuario", resolve: context => new { });
            Field<TarefaQuery>("tarefa", resolve: context => new { });
        }
    }
}
