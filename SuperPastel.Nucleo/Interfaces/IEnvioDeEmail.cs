using System.Threading.Tasks;

namespace SuperPastel.Nucleo.Interfaces
{
    public interface IEnvioDeEmail
    {
        Task Boleto(string email, string nome, string linha, string link, string itens, string dataReferencia, string dataVencimento);
    }
}
