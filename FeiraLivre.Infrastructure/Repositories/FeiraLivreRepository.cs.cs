using FeiraLivre.Core.Entities;
using FeiraLivre.Core.Interfaces;
using FeiraLivre.Infrastructure.DbContext;
using MongoDB.Driver;
using SharpCompress.Common;
//using Microsoft.Extensions.Options;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FeiraLivre.Infrastructure.Repositories
{
    public class FeiraLivreRepository : IFeiraLivreRepository
    {
        private readonly IDbConnectionFeiraLivre _connection;
        private readonly IMongoCollection<FeiraLivreEntity> _feiraLivreCollection;

        public FeiraLivreRepository(IDbConnectionFeiraLivre connection)
        {
            _connection             = connection;
            _feiraLivreCollection   = _connection.CreateConnection();
        }

        public async Task<FeiraLivreEntity> Post(FeiraLivreEntity feiraLivreEntity)
        {
            await _feiraLivreCollection.InsertOneAsync(feiraLivreEntity);

            return await GetById(feiraLivreEntity.Id);
        }

        public async Task<FeiraLivreEntity> GetById(string id)
        {
            return await _feiraLivreCollection.FindAsync(f => f.Id == id).Result.FirstAsync();
        }

        public async Task<List<FeiraLivreEntity>> GetByDistrito(string distrito)
        {
            return await _feiraLivreCollection.FindAsync(f => f.Distrito == distrito).Result.ToListAsync();
        }

        public async Task <List<FeiraLivreEntity>> Get()
        {
            return await _feiraLivreCollection.Find(_ => true).ToListAsync();
        }

        public async Task Update(FeiraLivreEntity feiraLivreEntity)
        {
            await _feiraLivreCollection.ReplaceOneAsync(f => f.Id == feiraLivreEntity.Id, feiraLivreEntity);
        }

        public async Task DeleteById(string id)
        {
            await _feiraLivreCollection.DeleteOneAsync(d => d.Id == id);
        }
    }
}