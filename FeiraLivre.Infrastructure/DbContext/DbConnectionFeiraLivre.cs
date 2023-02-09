using FeiraLivre.Core.Entities;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Options;
using MongoDB.Driver;


namespace FeiraLivre.Infrastructure.DbContext
{
    public class DbConnectionFeiraLivre : IDbConnectionFeiraLivre
    {
        private readonly IOptions<DbConfigurationFeiraLivre> _options;

        public DbConnectionFeiraLivre(IConfiguration config, IOptions<DbConfigurationFeiraLivre> options)
        {
            _options = options;
        }

        public IMongoCollection<FeiraLivreEntity> CreateConnection()
        {
            var mongoClient = new MongoClient(_options.Value.ConnectionString);

            var mongoDatabase = mongoClient.GetDatabase(_options.Value.DatabaseName);

            return mongoDatabase.GetCollection<FeiraLivreEntity>(_options.Value.CollectionName);
        }
    }
}