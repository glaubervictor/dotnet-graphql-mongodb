namespace SuperPastel.GraphQL.Models
{
    public class TarefaInput
    {
        public Guid UsuarioId { get; set; }
        public string Mensagem { get; set; }
        public DateTime DataLimite { get; set; }
    }
}
