using SuperPastel.Nucleo.Eventos;
using System;

namespace SuperPastel.Nucleo.Notificacoes
{
    public class NotificacaoDominio : Evento
    {
        public string Chave { get; private set; }
        public string Valor { get; private set; }
        public int Versao { get; private set; }

        public NotificacaoDominio(string chave, string valor)
        {
            Chave = chave;
            Valor = valor;
            Versao = 1;
        }
    }
}
