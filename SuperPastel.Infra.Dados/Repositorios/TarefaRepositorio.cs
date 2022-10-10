using MediatR;
using MongoDB.Bson;
using MongoDB.Driver;
using SuperPastel.Dominio.Entidades.Tarefas;
using SuperPastel.Dominio.Entidades.Tarefas.Repositorio;
using SuperPastel.Infra.Dados.Contextos;
using SuperPastel.Nucleo.Base;
using SuperPastel.Nucleo.Notificacoes;

namespace SuperPastel.Infra.Dados.Repositorios
{
    public class TarefaRepositorio : Repositorio<Tarefa>, ITarefaRepositorio
    {
        public TarefaRepositorio(MongoDbContexto contexto, INotificationHandler<NotificacaoDominio> notificacao) : base(contexto, notificacao)
        {
        }

        public PageInfo<Tarefa> ObterPaginadoComUsuario(int indice, int tamanho, Guid? usuarioId = null)
        {
            if (tamanho < 1 || tamanho > 50)
                tamanho = 50;

            PipelineDefinition<Tarefa, Tarefa> pipeline = new[]
            {
                new BsonDocument("$lookup",
                new BsonDocument
                    {
                        { "from", "Usuario" },
                        { "localField", "UsuarioId" },
                        { "foreignField", "_id" },
                        { "as", "Usuario" }
                    }),
                new BsonDocument("$set",
                new BsonDocument("Usuario",
                new BsonDocument("$first", "$Usuario"))),
                new BsonDocument("$skip", tamanho * indice),
                new BsonDocument("$limit", tamanho)
            };

            var query = _collection.Aggregate(pipeline).ToList();

            if (usuarioId.HasValue)
            {
                var queryNew = query.Where(x => x.UsuarioId == usuarioId.Value).ToList();
                return new PageInfo<Tarefa>(queryNew.Count, tamanho, queryNew);
            }

            return new PageInfo<Tarefa>(query.Count, tamanho, query);
        }
    }
}
