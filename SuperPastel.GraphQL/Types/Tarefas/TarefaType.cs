using GraphQL.Types;
using SuperPastel.Dominio.Entidades.Tarefas;
using SuperPastel.GraphQL.Types.Usuarios;

namespace SuperPastel.GraphQL.Types.Tarefas
{
    public class TarefaType : ObjectGraphType<Tarefa>, IGraphType
    {
        public TarefaType()
        {
            Field(x => x.Id, type: typeof(IdGraphType));
            Field(x => x.UsuarioId);
            Field(x => x.Usuario, type: typeof(UsuarioType));
            Field(x => x.Mensagem);
            Field(x => x.DataLimite, type: typeof(DateGraphType));
            Field(x => x.Finalizada, type: typeof(BooleanGraphType));
            Field(x => x.DataCadastro, type: typeof(DateTimeGraphType));
        }
    }
}
