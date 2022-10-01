using FluentValidation;
using MongoDB.Bson.Serialization.Attributes;
using SuperPastel.Dominio.Entidades.Usuarios;
using SuperPastel.Nucleo.Base;
using SuperPastel.Nucleo.Mensageria;
using SuperPastel.Nucleo.Notificacoes;

namespace SuperPastel.Dominio.Entidades.Tarefas
{
    public class Tarefa : Entidade<Tarefa>
    {
        #region Propriedades

        public Guid UsuarioId { get; private set; }
        [BsonIgnoreIfNull]
        public Usuario Usuario { get; private set; }
        public string Mensagem { get; private set; }
        public DateTime DataLimite { get; private set; }
        public bool Finalizada { get; private set; } = false;

        #endregion

        #region Construtor

        public Tarefa(IMediatorHandler bus) : base(bus) { }

        #endregion

        #region Métodos Públicos

        public Tarefa Criar(Guid usuarioId, string mensagem, DateTime dataLimite)
        {
            UsuarioId = usuarioId;
            Mensagem = mensagem;
            DataLimite = dataLimite;

            ValidarTarefa();

            return this;
        }

        public Tarefa SetarUsuario(Guid id)
        {
            UsuarioId = id;
            return this;
        }

        public Tarefa Finalizar()
        {
            Finalizada = true;
            return this;
        }

        #endregion

        #region Métodos Privados

        private void ValidarTarefa()
        {
            var result = new TarefaValidacao().Validate(this);

            if (!result.IsValid)
            {
                result.Errors
                    .ToList()
                    .ForEach(x => Bus.RaiseEvent(new NotificacaoDominio(x.PropertyName, x.ErrorMessage)));
            }
        }

        #endregion
    }

    public class TarefaValidacao : AbstractValidator<Tarefa>
    {
        public TarefaValidacao()
        {
            var mensagens = new Mensagens<Tarefa>();
            RuleFor(x => x.Mensagem)
                .NotEmpty()
                .WithMessage(mensagens.Requerido(x => x.Mensagem));
        }
    }
}
