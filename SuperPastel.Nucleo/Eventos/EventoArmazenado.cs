using System;

namespace SuperPastel.Nucleo.Eventos
{
    public class EventoArmazenado : Evento
    {
        public EventoArmazenado(Evento oEvento, string dados, Guid? usuarioId, Guid? inquilinoId)
        {
            Id = Guid.NewGuid();
            AgregadorId = oEvento.AgregadorId;
            MensagemTipo = oEvento.MensagemTipo;
            Dados = dados;
            UsuarioId = usuarioId;
            InquilinoId = inquilinoId;
        }

        // EF Construtor
        protected EventoArmazenado() { }

        public Guid Id { get; private set; }

        public string Dados { get; private set; }

        public Guid? UsuarioId { get; private set; }

        public Guid? InquilinoId { get; private set; }
    }
}
