using MediatR;
using System;

namespace SuperPastel.Nucleo.Eventos
{
    public abstract class Mensagem : IRequest<bool>
    {
        public string MensagemTipo { get; protected set; }
        public Guid AgregadorId { get; protected set; }

        protected Mensagem()
        {
            MensagemTipo = GetType().Name;
        }
    }
}
