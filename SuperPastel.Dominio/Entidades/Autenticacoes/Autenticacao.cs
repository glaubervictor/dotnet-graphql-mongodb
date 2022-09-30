using System;

namespace SuperPastel.Dominio.Entidades.Autenticacoes
{
    public class Autenticacao
    {
        public Autenticacao(string token, DateTime validoAte)
        {
            Token = token;
            ValidoAte = validoAte;
        }

        public string Token { get; private set; }
        public DateTime ValidoAte { get; private set; }
    }
}
