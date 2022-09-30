using MediatR;

namespace SuperPastel.Nucleo.Notificacoes
{
    public class NotificacaoDominioHandler : INotificationHandler<NotificacaoDominio>
    {
        private List<NotificacaoDominio> _notificacoes;

        public NotificacaoDominioHandler()
        {
            _notificacoes = new List<NotificacaoDominio>();
        }

        public Task Handle(NotificacaoDominio mensagem, CancellationToken cancellationToken)
        {
            _notificacoes.Add(mensagem);

            Console.ForegroundColor = ConsoleColor.Red;
            Console.WriteLine($"{DateTime.Now} - Erro: {mensagem.Chave} - {mensagem.Valor}");

            return Task.CompletedTask;
        }

        public virtual List<NotificacaoDominio> GetNotifications()
        {
            return _notificacoes;
        }

        public virtual bool HasNotifications()
        {
            return GetNotifications().Any();
        }

        public void Dispose()
        {
            _notificacoes = new List<NotificacaoDominio>();
        }
    }
}
