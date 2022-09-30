using GraphQL.Types;

namespace SuperPastel.GraphQL.Mutations
{
    public class Mutation : ObjectGraphType
    {
        public Mutation()
        {
            Name = "mutations";
            Description = "Mutations GraphQL";

            Field<AutenticacaoMutation>("autenticacao", resolve: context => new { });
            Field<UsuarioMutation>("usuario", resolve: context => new { });
            Field<TarefaMutation>("tarefa", resolve: context => new { });
        }
    }
}
