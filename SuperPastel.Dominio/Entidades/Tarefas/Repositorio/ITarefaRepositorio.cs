using SuperPastel.Nucleo.Base;
using SuperPastel.Nucleo.Interfaces;

namespace SuperPastel.Dominio.Entidades.Tarefas.Repositorio
{
    public interface ITarefaRepositorio : IRepositorio<Tarefa>
    {
        PageInfo<Tarefa> ObterPaginadoComUsuario(int indice, int tamanho);
    }
}
