using SuperPastel.Nucleo.Eventos;
using SuperPastel.Nucleo.Mensageria;
using MediatR;

namespace SuperPastel.Infra.Transporte
{
    public sealed class InMemoryBus : IMediatorHandler
    {
        private readonly IMediator _mediator;

        public InMemoryBus(IMediator mediator)
        {
            _mediator = mediator;
        }

        public Task RaiseEvent<T>(T @event) where T : Evento => _mediator.Publish(@event);
    }
}
