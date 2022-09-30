using SuperPastel.Nucleo.Mensageria;
using SuperPastel.Nucleo.Notificacoes;
using FluentValidation.Results;
using MediatR;

namespace SuperPastel.Dominio.CommandHandlers
{
    public abstract class CommandHandler
    {
        private readonly IMediatorHandler _bus;
        private readonly NotificacaoDominioHandler _notificacoes;

        protected CommandHandler(
            IMediatorHandler bus,
            INotificationHandler<NotificacaoDominio> notificacoes)
        {
            _bus = bus;
            _notificacoes = (NotificacaoDominioHandler)notificacoes;
        }

        protected void NotifyValidacaoErrors(ValidationResult ValidacaoResult)
        {
            foreach (var error in ValidacaoResult.Errors)
            {
                _bus.RaiseEvent(new NotificacaoDominio(error.PropertyName, error.ErrorMessage));
            }
        }
    }
}