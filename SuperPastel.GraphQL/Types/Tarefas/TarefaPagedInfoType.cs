using GraphQL.Types;
using SuperPastel.Dominio.Entidades.Tarefas;
using SuperPastel.Nucleo.Base;

namespace SuperPastel.GraphQL.Types.Tarefas
{
    public class TarefaPagedInfoType : ObjectGraphType<PageInfo<Tarefa>>
    {
        public TarefaPagedInfoType()
        {
            Field<ListGraphType<TarefaType>>("items", resolve: context => context.Source.List);
            Field(x => x.PageCount);
            Field(x => x.Size);
            Field(x => x.TotalCount);
        }
    }
}
