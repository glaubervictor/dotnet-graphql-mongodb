namespace SuperPastel.Nucleo.Eventos
{
    public interface IHandler<in T> where T : Mensagem
    {
        void Handle(T message);
    }
}
