using SuperPastel.Nucleo.Mensageria;
using SuperPastel.Nucleo.Notificacoes;
using FluentValidation.Results;
using System.Linq;

namespace SuperPastel.Nucleo.Ajudantes
{
    public static class Validacao
    {
        public static void NotificarErros(IMediatorHandler bus, ValidationResult validationResult)
        {
            validationResult.Errors
                .ToList()
                .ForEach(x => bus.RaiseEvent(new NotificacaoDominio(x.PropertyName, x.ErrorMessage)));
        }
    }
}
