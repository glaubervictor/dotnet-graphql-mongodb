using SuperPastel.Nucleo.Eventos;

namespace SuperPastel.Nucleo.Mensageria
{
    public interface IMediatorHandler
    {
        Task RaiseEvent<T>(T @event) where T : Evento;
    }
}
