using GraphQL.Types;
using SuperPastel.GraphQL.Models;

namespace SuperPastel.GraphQL.Types.Pessoas
{
    public class PessoaInputType : InputObjectGraphType<PessoaInput>, IGraphType
    {
        public PessoaInputType()
        {
            Field(x => x.Nome);
            Field(x => x.DataNascimento, type: typeof(DateGraphType));
            Field(x => x.Cep);
            Field(x => x.Logradouro);
            Field(x => x.Numero, nullable: true);
            Field(x => x.Complemento, nullable: true);
            Field(x => x.Bairro);
            Field(x => x.Cidade);
            Field(x => x.Telefone, nullable: true);
            Field(x => x.Celular);
            Field(x => x.FotoUrl);
        }
    }
 }