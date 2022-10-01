using SuperPastel.Nucleo.Ajudantes;
using SuperPastel.Nucleo.Base;
using FluentValidation;
using SuperPastel.Dominio.Entidades.Pessoas;
using SuperPastel.Nucleo.Mensageria;
using SuperPastel.Nucleo.Notificacoes;

namespace SuperPastel.Dominio.Entidades.Usuarios
{
    public class Usuario : Entidade<Usuario>
    {
        #region Propriedades

        public Pessoa Pessoa { get; private set; }
        public string Email { get; private set; }
        public string SenhaHash { get; private set; }
        public bool EmailConfirmado { get; private set; }
        public bool SuperUsuario { get; private set; } = false;

        #endregion

        #region Construtor

        public Usuario(IMediatorHandler bus) : base(bus) { }

        #endregion

        #region Métodos Públicos

        public Usuario Criar(string email, string senha, Pessoa pessoa)
        {
            Email = email;
            Pessoa = pessoa;

            GerarHashSenha(senha);

            ValidarPessoa();
            ValidarUsuario();

            return this;
        }

        public Usuario ConfirmarEmail()
        {
            EmailConfirmado = true;
            return this;
        }

        public Usuario EhSuperUsuario()
        {
            SuperUsuario = true;
            return this;
        }

        #endregion

        #region Métodos Privados

        private Usuario GerarHashSenha(string senha)
        {
            SenhaHash = Passwords.HashPassword(senha);
            return this;
        }

        private void ValidarPessoa()
        {
            var result = new PessoaValidacao().Validate(Pessoa);

            if (!result.IsValid)
            {
                result.Errors
                    .ToList()
                    .ForEach(x => Bus.RaiseEvent(new NotificacaoDominio(x.PropertyName, x.ErrorMessage)));
            }
        }

        private void ValidarUsuario()
        {
            var result = new UsuarioValidacao().Validate(this);

            if (!result.IsValid)
            {
                result.Errors
                    .ToList()
                    .ForEach(x => Bus.RaiseEvent(new NotificacaoDominio(x.PropertyName, x.ErrorMessage)));
            }
        }

        #endregion
    }

    public class UsuarioValidacao : AbstractValidator<Usuario>
    {
        public UsuarioValidacao()
        {
            var mensagens = new Mensagens<Usuario>();

            RuleFor(x => x.Email)
                .NotEmpty().WithMessage(mensagens.Requerido(x => x.Email))
                .EmailAddress().WithMessage(mensagens.EmailInvalido());
        }
    }
}
