
using FeiraLivre.Core.Entities;
using MongoDB.Driver;

namespace FeiraLivre.Infrastructure.DbContext
{
    public interface IDbConnectionFeiraLivre
    {
        IMongoCollection<FeiraLivreEntity> CreateConnection();
    }
}