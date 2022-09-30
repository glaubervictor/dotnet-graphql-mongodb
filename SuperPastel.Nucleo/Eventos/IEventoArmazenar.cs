namespace SuperPastel.Nucleo.Eventos
{
    public interface IEventoArmazenar
    {
        void Salvar<T>(T oEvento) where T : Evento;
    }
}
