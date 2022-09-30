using MediatR;
using System;

namespace SuperPastel.Nucleo.Eventos
{
    public abstract class Evento : Mensagem, INotification
    {
        public DateTime Timestamp { get; private set; }

        protected Evento()
        {
            Timestamp = DateTime.Now;
        }
    }
}
