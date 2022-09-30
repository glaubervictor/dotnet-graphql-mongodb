using MediatR;
using SuperPastel.Dominio.Entidades.Tarefas;
using SuperPastel.Dominio.Entidades.Tarefas.Repositorio;
using SuperPastel.Infra.Dados.Contextos;
using SuperPastel.Nucleo.Notificacoes;

namespace SuperPastel.Infra.Dados.Repositorios
{
    public class TarefaRepositorio : Repositorio<Tarefa>, ITarefaRepositorio
    {
        public TarefaRepositorio(MongoDbContexto contexto, INotificationHandler<NotificacaoDominio> notificacao) : base(contexto, notificacao)
        {
        }
    }
}
