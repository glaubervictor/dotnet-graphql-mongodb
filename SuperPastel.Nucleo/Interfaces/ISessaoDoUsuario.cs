using System;

namespace SuperPastel.Nucleo.Interfaces
{
    public interface ISessaoDoUsuario
    {
        Guid ObterUsuarioId();
        bool EhSuperUsuario();
        bool EstaAutenticado();
    }
}
