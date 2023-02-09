using FeiraLivre.Core.Entities;

namespace FeiraLivre.Core.Interfaces
{
    public interface IFeiraLivreRepository
    {
        Task<FeiraLivreEntity> Post(FeiraLivreEntity feiraLivreEntity);

        Task<FeiraLivreEntity> GetById(string id);

        Task<List<FeiraLivreEntity>> GetByDistrito(string distrito);

        Task<List<FeiraLivreEntity>> Get();

        Task Update(FeiraLivreEntity feiraLivreEntity);

        Task DeleteById(string id);
    }
}