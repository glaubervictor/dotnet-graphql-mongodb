using SuperPastel.Nucleo.Base;
using SuperPastel.Nucleo.Mensageria;

namespace SuperPastel.Dominio.Entidades.Tarefas
{
    public class Tarefa : Entidade<Tarefa>
    {
        #region Propriedades

        public Guid UsuarioId { get; private set; }
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



    }
}
