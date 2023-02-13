using FeiraLivre.Core.Entities;

namespace FeiraLivre.Core.Interfaces.UseCases
{
    public interface IFeiraLivreUseCase
    {
        Task<FeiraLivreEntity> Cadastrar(FeiraLivreEntity feiraLivreEntity);

        Task<List<FeiraLivreEntity>> ListarPorDistrito(string distrito);

        Task<List<FeiraLivreEntity>> Listar();

        Task Atualizar(FeiraLivreEntity feiraLivreEntity);

        Task DeletarPorId(string id);
    }
}