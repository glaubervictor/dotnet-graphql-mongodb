using GraphQL.Types;
using SuperPastel.GraphQL.Models;

namespace SuperPastel.GraphQL.Types.Tarefas
{
    public class TarefaInputType : InputObjectGraphType<TarefaInput>, IGraphType
    {
        public TarefaInputType()
        {
            Field(x => x.UsuarioId, type: typeof(IdGraphType));
            Field(x => x.Mensagem);
            Field(x => x.DataLimite, type: typeof(DateGraphType));
        }
    }
}
