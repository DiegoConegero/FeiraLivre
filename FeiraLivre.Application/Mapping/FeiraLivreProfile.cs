using AutoMapper;
using FeiraLivre.Application.InputModels;
using FeiraLivre.Application.ViewModels;
using FeiraLivre.Core.Entities;

namespace FeiraLivre.Application.Mapping
{
    public class FeiraLivreProfile : Profile
    {
        public FeiraLivreProfile()
        {
            AllowNullCollections = true;
            AllowNullDestinationValues = true;

            CreateMap<CadastrarFeiraLivreInputModel, FeiraLivreEntity>();
            //CreateMap<FeiraLivreEntity, CadastrarFeiraLivreInputModel>();
            CreateMap<AlterarFeiraLivreInputModel, FeiraLivreEntity>();

            //CreateMap<FeiraLivreViewModel, FeiraLivreEntity>();
            CreateMap<FeiraLivreEntity, FeiraLivreViewModel>();
        }
    }
}