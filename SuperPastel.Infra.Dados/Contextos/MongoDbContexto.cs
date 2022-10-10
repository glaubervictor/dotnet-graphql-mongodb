using Microsoft.Extensions.Configuration;
using MongoDB.Bson.Serialization;
using MongoDB.Driver;

namespace SuperPastel.Infra.Dados.Contextos
{
    public class MongoDbContexto
    {
        public IMongoClient Client { get; set; }
        public IMongoDatabase Database { get; set; }

        public MongoDbContexto(IConfiguration configuration)
        {
            InicializarDadosConexao(configuration);
        }

        public IMongoCollection<TEntity> GetCollection<TEntity>()
        {
            return Database?.GetCollection<TEntity>(BsonClassMap.LookupClassMap(typeof(TEntity)).Discriminator);
        }

        private void InicializarDadosConexao(IConfiguration configuration)
        {
            MongoUrl url = null;

#if DEBUG
            url = new MongoUrl(configuration.GetConnectionString("MONGODB_CONNECTION"));
#else
            url = new MongoUrl(Environment.GetEnvironmentVariable("MONGO_DB"));
#endif
            var settings = new MongoClientSettings
            {
                MaxConnectionIdleTime = new TimeSpan(0, 5, 0),
                Server = url.Server,
                Credential = url.GetCredential(),
                MinConnectionPoolSize = 1,
                MaxConnectionPoolSize = 25,
            };

            Client = new MongoClient(settings);
            Database = Client.GetDatabase("DbSuperPastel");
        }
    }
}
