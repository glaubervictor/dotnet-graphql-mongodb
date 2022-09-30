using SuperPastel.Nucleo.Base;
using FluentValidation;

namespace SuperPastel.Dominio.Entidades.Pessoas
{
    public class Pessoa
    {
        #region Propriedades

        public string Nome { get; private set; }
        public DateTime DataNascimento { get; private set; }
        public string Cep { get; private set; }
        public string Logradouro { get; private set; }
        public string Numero { get; private set; }
        public string Complemento { get; private set; }
        public string Bairro { get; private set; }
        public string Cidade { get; private set; }
        public string Telefone { get; private set; }
        public string Celular { get; private set; }
        public string FotoUrl { get; private set; }

        #endregion

        #region Construtor

        public Pessoa(
            string nome, 
            DateTime dataNascimento, 
            string cep, 
            string logradouro, 
            string numero, 
            string complemento, 
            string bairro, 
            string cidade, 
            string telefone, 
            string celular, 
            string fotoUrl)
        {
            Nome = nome;
            DataNascimento = dataNascimento;
            Cep = cep;
            Logradouro = logradouro;
            Numero = numero;
            Complemento = complemento;
            Bairro = bairro;
            Cidade = cidade;
            Telefone = telefone;
            Celular = celular;
            FotoUrl = fotoUrl;
        }

        #endregion
    }

    public class PessoaValidacao : AbstractValidator<Pessoa>
    {
        public PessoaValidacao()
        {
            var mensagens = new Mensagens<Pessoa>();

            RuleFor(x => x.Nome).NotEmpty().WithMessage(mensagens.Requerido(x => x.Nome)).MaximumLength(100).WithMessage(mensagens.MaximoCaracterePermitido(x => x.Nome, 100));
            RuleFor(x => x.Cep).NotEmpty().WithMessage(mensagens.Requerido(x => x.Cep)).MaximumLength(8).WithMessage(mensagens.MaximoCaracterePermitido(x => x.Cep, 8));
            RuleFor(x => x.Logradouro).NotEmpty().WithMessage(mensagens.Requerido(x => x.Logradouro)).MaximumLength(80).WithMessage(mensagens.MaximoCaracterePermitido(x => x.Logradouro, 80));
            RuleFor(x => x.Bairro).NotEmpty().WithMessage(mensagens.Requerido(x => x.Bairro)).MaximumLength(80).WithMessage(mensagens.MaximoCaracterePermitido(x => x.Bairro, 80));
            RuleFor(x => x.Cidade).NotEmpty().WithMessage(mensagens.Requerido(x => x.Cidade)).MaximumLength(80).WithMessage(mensagens.MaximoCaracterePermitido(x => x.Cidade, 80));
            RuleFor(x => x.Celular).NotEmpty().WithMessage(mensagens.Requerido(x => x.Celular)).MaximumLength(11).WithMessage(mensagens.MaximoCaracterePermitido(x => x.Celular, 11));
        }
    }
}
