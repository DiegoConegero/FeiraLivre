using FeiraLivre.Core.Entities;

namespace FeiraLivre.Core.Interfaces.Repository
{
    public interface IFeiraLivreRepository
    {
        Task<FeiraLivreEntity> Cadastrar(FeiraLivreEntity feiraLivreEntity);

        Task<FeiraLivreEntity> ObterPorBairroENome(string bairro, string nome);

        Task<List<FeiraLivreEntity>> ListarPorDistrito(string distrito);

        Task<List<FeiraLivreEntity>> Listar();

        Task<bool> Atualizar(FeiraLivreEntity feiraLivreEntity);

        Task<bool> DeletarPorId(string id);

        Task<FeiraLivreEntity> ObterPorId(string id);
    }
}