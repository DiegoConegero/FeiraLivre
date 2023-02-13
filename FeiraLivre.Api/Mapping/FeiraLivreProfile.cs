using AutoMapper;
using FeiraLivre.Api.Models;
using FeiraLivre.Api.ViewModels;
using FeiraLivre.Core.Entities;

namespace FeiraLivre.Api.Mapping
{
    public class FeiraLivreProfile : Profile
    {
        public FeiraLivreProfile()
        {
            AllowNullCollections = true;
            AllowNullDestinationValues = true;

            CreateMap<CadastrarFeiraLivreInputModel, FeiraLivreEntity>();
            CreateMap<AlterarFeiraLivreInputModel, FeiraLivreEntity>();
            CreateMap<FeiraLivreEntity, FeiraLivreViewModel>();
        }
    }
}