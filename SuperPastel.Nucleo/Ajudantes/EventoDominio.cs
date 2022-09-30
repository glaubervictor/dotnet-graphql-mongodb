using SuperPastel.Nucleo.Mensageria;
using SuperPastel.Nucleo.Notificacoes;
using FluentValidation.Results;

namespace SuperPastel.Nucleo.Ajudantes
{
    public static class EventoDominio
    {
        public static void NotificarErro(IMediatorHandler bus, ValidationResult validationResult)
        {
            validationResult.Errors
                .ToList()
                .ForEach(x => bus.RaiseEvent(new NotificacaoDominio(x.PropertyName, x.ErrorMessage)));
        }

        public static void NotificarErro(IMediatorHandler bus, string chave, string mensagem)
        {
            bus.RaiseEvent(new NotificacaoDominio(chave, mensagem));
        }
    }
}
