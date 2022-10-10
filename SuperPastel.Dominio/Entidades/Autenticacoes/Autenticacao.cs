namespace SuperPastel.Dominio.Entidades.Autenticacoes
{
    public class Autenticacao
    {
        public Autenticacao(string perfil, string token, DateTime validoAte)
        {
            Perfil = perfil;
            Token = token;
            ValidoAte = validoAte;
        }

        public string Perfil { get; set; }
        public string Token { get; private set; }
        public DateTime ValidoAte { get; private set; }
    }
}
