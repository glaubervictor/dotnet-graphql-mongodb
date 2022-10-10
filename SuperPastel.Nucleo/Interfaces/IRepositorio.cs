using SuperPastel.Nucleo.Base;
using System.Linq.Expressions;

namespace SuperPastel.Nucleo.Interfaces
{
    public interface IRepositorio<TEntidade> : IDisposable where TEntidade : Entidade<TEntidade>
    {
        TEntidade Adicionar(TEntidade entidade);
        TEntidade Atualizar(TEntidade entidade);
        void AdicionarMuitos(IEnumerable<TEntidade> entidades);
        long Contar(Expression<Func<TEntidade, bool>> expressao);
        PageInfo<TEntidade> ObterPaginado(int indice, int tamanho);
        PageInfo<TEntidade> ObterPaginado(int indice, int tamanho, Expression<Func<TEntidade, bool>> expressao);
        TEntidade ObterPorExpressao(Expression<Func<TEntidade, bool>> expressao);
        TEntidade ObterPorId(Guid id);
        IEnumerable<TEntidade> ObterTodos(Expression<Func<TEntidade, bool>> expressao);
        void Remover(Guid id);
    }
}
