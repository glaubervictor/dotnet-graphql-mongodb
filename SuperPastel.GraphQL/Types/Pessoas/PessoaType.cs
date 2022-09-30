using SuperPastel.Dominio.Entidades.Pessoas;
using GraphQL.Types;

namespace SuperPastel.GraphQL.Types.Pessoas
{
    public class PessoaType : ObjectGraphType<Pessoa>, IGraphType
    {
        public PessoaType()
        {
            Field(x => x.Nome);
            Field(x => x.Cep);
            Field(x => x.Logradouro);
            Field(x => x.Numero, nullable: true);
            Field(x => x.Complemento, nullable: true);
            Field(x => x.Bairro);
            Field(x => x.Cidade);
            Field(x => x.Telefone, nullable: true);
            Field(x => x.Celular);
        }
    }
 }