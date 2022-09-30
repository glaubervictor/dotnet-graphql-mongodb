using GraphQL;
using GraphQL.Types;
using MediatR;
using SuperPastel.GraphQL.Models;
using SuperPastel.GraphQL.Types.Usuarios;
using SuperPastel.Nucleo.Mensageria;
using SuperPastel.Nucleo.Notificacoes;
using SuperPastel.Dominio.Entidades.Tarefas.Repositorio;
using SuperPastel.Dominio.Entidades.Tarefas;
using SuperPastel.GraphQL.Types.Tarefas;
using SuperPastel.GraphQL.Base;
using SuperPastel.Nucleo.Ajudantes;

namespace SuperPastel.GraphQL.Mutations
{
    public class TarefaMutation : ObjectGraphType
    {
        public TarefaMutation(
            IMediatorHandler bus,
            INotificationHandler<NotificacaoDominio> notificacao,
            ITarefaRepositorio tarefaRepositorio)
        {
            Name = nameof(TarefaMutation);
            Description = "Mutations tarefa";

            Field<TarefaType>(
                "create",
                description: "Criar um nova tarefa",
                arguments: new QueryArguments(new QueryArgument<NonNullGraphType<TarefaInputType>> { Name = "input" }),
                resolve: context =>
                {
                    var input = context.GetArgument<TarefaInput>("input");

                    var tarefa = new Tarefa(bus)
                        .Criar(input.UsuarioId, input.Mensagem, input.DataLimite);

                    var notificacoes = (NotificacaoDominioHandler)notificacao;

                    if (notificacoes.HasNotifications())
                    {
                        context.AddErrorMessages(notificacoes);
                        return null;
                    }

                    return tarefaRepositorio.Adicionar(tarefa);
                }).AuthorizeWith(Policies.MANAGER);
        }
    }
}
