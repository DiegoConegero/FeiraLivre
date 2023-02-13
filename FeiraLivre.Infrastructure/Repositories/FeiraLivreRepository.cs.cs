using FeiraLivre.Core.Entities;
using FeiraLivre.Core.Interfaces.Repository;
using FeiraLivre.Infrastructure.DbContext;
using MongoDB.Driver;

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

        public async Task<FeiraLivreEntity> Cadastrar(FeiraLivreEntity feiraLivreEntity)
        {
            await _feiraLivreCollection.InsertOneAsync(feiraLivreEntity);

            return await ObterPorId(feiraLivreEntity.Id);
        }

        public async Task<FeiraLivreEntity> ObterPorBairroENome(string bairro, string nome)
        {
            return await _feiraLivreCollection.FindAsync(f => f.Bairro == bairro && f.Nome == nome).Result.FirstOrDefaultAsync();
        }

        public async Task<List<FeiraLivreEntity>> ListarPorDistrito(string distrito)
        {
            return await _feiraLivreCollection.FindAsync(f => f.Distrito == distrito).Result.ToListAsync();
        }

        public async Task<List<FeiraLivreEntity>> Listar()
        {
            return await _feiraLivreCollection.Find(_ => true).ToListAsync(); ;
        }

        public async Task<bool> Atualizar(FeiraLivreEntity feiraLivreEntity)
        {
            var feiraAtualizada = await _feiraLivreCollection.ReplaceOneAsync(f => f.Id == feiraLivreEntity.Id, feiraLivreEntity);

            return feiraAtualizada.ModifiedCount > 0;
        }

        public async Task<bool> DeletarPorId(string id)
        {
            var feiraDeletada = await _feiraLivreCollection.DeleteOneAsync(d => d.Id == id);

            return feiraDeletada.DeletedCount > 0;
        }

        public async Task<FeiraLivreEntity> ObterPorId(string id)
        {
            return await _feiraLivreCollection.FindAsync(f => f.Id == id).Result.FirstOrDefaultAsync();
        }
    }
}