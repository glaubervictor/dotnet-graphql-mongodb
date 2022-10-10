namespace SuperPastel.GraphQL.Models
{
    public class UsuarioInput
    {
        public string Email { get; set; }
        public string Senha { get; set; }
        public bool SuperUsuario { get; set; }
        public PessoaInput Pessoa { get; set; }
    }
}
