using GraphQL.Types;
using SuperPastel.Dominio.Entidades.Tarefas;

namespace SuperPastel.GraphQL.Types.Tarefas
{
    public class TarefaType : ObjectGraphType<Tarefa>, IGraphType
    {
        public TarefaType()
        {
            Field(x => x.UsuarioId);
            Field(x => x.Mensagem);
            Field(x => x.DataLimite, type: typeof(DateGraphType));
            Field(x => x.DataCadastro, type: typeof(DateTimeGraphType));
        }
    }
}
