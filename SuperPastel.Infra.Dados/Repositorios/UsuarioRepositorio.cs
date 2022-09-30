using MediatR;
using SuperPastel.Dominio.Entidades.Usuarios;
using SuperPastel.Dominio.Entidades.Usuarios.Repositorio;
using SuperPastel.Infra.Dados.Contextos;
using SuperPastel.Nucleo.Ajudantes;
using SuperPastel.Nucleo.Notificacoes;

namespace SuperPastel.Infra.Dados.Repositorios
{
    public class UsuarioRepositorio : Repositorio<Usuario>, IUsuarioRepositorio
    {
        public UsuarioRepositorio(MongoDbContexto contexto, INotificationHandler<NotificacaoDominio> notificacao) : base(contexto, notificacao)
        {
        }

        public Usuario ObterPorEmail(string email)
        {
            return string.IsNullOrWhiteSpace(email)
                ? throw new ArgumentNullException("O e-mail precisa ser informado")
                : ObterPorExpressao(x => x.Email == email);
        }

        public bool SenhaValida(Usuario usuario, string senha)
        {
            var hashedSenha = Passwords.HashPassword(senha);
            return usuario.SenhaHash.Equals(hashedSenha);
        }
    }
}
