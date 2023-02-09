using FeiraLivre.Application.InputModels;
using FeiraLivre.Application.ViewModels;

namespace FeiraLivre.Application.Interfaces
{
    public interface IFeiraLivreService
    {
        Task<FeiraLivreViewModel> Post(CadastrarFeiraLivreInputModel cadastrarFeiraLivreInputModel);

        Task<List<FeiraLivreViewModel>> GetByDistrito(string distrito);

        Task<List<FeiraLivreViewModel>> Get();

        Task Update(AlterarFeiraLivreInputModel alterarFeiraLivreInputModel);

        Task DeleteById(string id);
    }
}