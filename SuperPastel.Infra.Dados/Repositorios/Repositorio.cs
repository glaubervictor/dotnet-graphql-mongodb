using SuperPastel.Infra.Dados.Contextos;
using SuperPastel.Nucleo.Base;
using SuperPastel.Nucleo.Interfaces;
using System.Linq.Expressions;
using MongoDB.Driver;
using MediatR;
using SuperPastel.Nucleo.Notificacoes;
using Castle.DynamicProxy.Generators.Emitters.SimpleAST;

namespace SuperPastel.Infra.Dados.Repositorios
{
    public abstract class Repositorio<TEntidade> : IRepositorio<TEntidade> where TEntidade : Entidade<TEntidade>
    {
        protected readonly MongoDbContexto _contexto;
        protected readonly IMongoCollection<TEntidade> _collection;
        protected readonly INotificationHandler<NotificacaoDominio> _notificacao;

        public Repositorio(MongoDbContexto contexto, INotificationHandler<NotificacaoDominio> notificacao)
        {
            _contexto = contexto;
            _notificacao = notificacao;
            _collection = _contexto.GetCollection<TEntidade>();
        }

        public virtual TEntidade Adicionar(TEntidade entidade)
        {
            ValidateAndThrow();

            _collection.InsertOne(entidade);
            return entidade;
        }

        public virtual void AdicionarMuitos(IEnumerable<TEntidade> entidades)
        {
            ValidateAndThrow();

            _collection.InsertMany(entidades);
        }

        public virtual long Contar(Expression<Func<TEntidade, bool>> expressao) => _collection.Find(expressao).CountDocuments();

        public virtual PageInfo<TEntidade> ObterPaginado(int indice, int tamanho)
        {
            if (tamanho < 1 || tamanho > 50)
                tamanho = 50;

            var query = _collection.Find(x => true);

            var registroTotal = query.CountDocuments();

            var dados = query
                .Skip(tamanho * indice)
                .Limit(tamanho)
                .ToEnumerable();

            return new PageInfo<TEntidade>(registroTotal, tamanho, dados);
        }

        public virtual PageInfo<TEntidade> ObterPaginado(int indice, int tamanho, Expression<Func<TEntidade, bool>> expressao)
        {
            if (tamanho < 1 || tamanho > 50)
                tamanho = 50;

            indice = indice < 0 ? 0 : indice;

            var query = _collection.Find(expressao);

            var registroTotal = query.CountDocuments();

            var dados = query
                .Skip(tamanho * indice)
                .Limit(tamanho)
                .ToEnumerable();

            return new PageInfo<TEntidade>(registroTotal, tamanho, dados);
        }

        public virtual TEntidade ObterPorExpressao(Expression<Func<TEntidade, bool>> expressao) => _collection.Find(expressao).FirstOrDefault();

        public virtual TEntidade ObterPorId(Guid id) => _collection.Find(x => x.Id == id).FirstOrDefault();

        public virtual IEnumerable<TEntidade> ObterTodos(Expression<Func<TEntidade, bool>> expressao) => _collection.Find(expressao).ToEnumerable();

        public virtual void Remover(Guid id)
        {
            ValidateAndThrow();

            var filter = Builders<TEntidade>.Filter.Eq(s => s.Id, id);
            var update = Builders<TEntidade>.Update.Set(s => s.Excluido, true);

            _collection.UpdateOne(filter, update);

        }

        public virtual void Dispose()
        {
            GC.SuppressFinalize(this);
        }

        private void ValidateAndThrow()
        {
            var notification = ((NotificacaoDominioHandler)_notificacao);

            if (notification.HasNotifications())
            {
                throw new Exception("Informação não pode ser inserida, editada ou removida pois possuí validação pendente.");
            }
        }

    }
}
