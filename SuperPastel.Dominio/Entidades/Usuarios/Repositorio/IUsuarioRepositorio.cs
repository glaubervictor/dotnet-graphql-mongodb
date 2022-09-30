using SuperPastel.Nucleo.Interfaces;

namespace SuperPastel.Dominio.Entidades.Usuarios.Repositorio
{
    public interface IUsuarioRepositorio : IRepositorio<Usuario>
    {
        Usuario ObterPorEmail(string email);
        bool SenhaValida(Usuario usuario, string senha);
    }
}
