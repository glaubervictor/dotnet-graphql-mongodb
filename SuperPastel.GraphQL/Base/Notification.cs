using SuperPastel.Nucleo.Notificacoes;
using GraphQL;

namespace SuperPastel.GraphQL.Base
{
    public static class Notification
    {
        public static void AddErrorMessages(this IResolveFieldContext<object> context, NotificacaoDominioHandler notificacao)
        {
            if (notificacao.HasNotifications())
            {
                var data = new Dictionary<string, string>();
                notificacao.GetNotifications().ForEach(x => data.Add(x.Chave, x.Valor));

                context.Errors.Add(new ExecutionError("Por favor verifique os dados", data));
            }
        }
    }

}
