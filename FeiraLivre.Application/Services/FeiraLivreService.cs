using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using AutoMapper.Configuration.Annotations;
using FeiraLivre.Application.InputModels;
using FeiraLivre.Application.Interfaces;
using FeiraLivre.Application.ViewModels;
using FeiraLivre.Core.Interfaces;
using FeiraLivre.Core.Entities;

namespace FeiraLivre.Application.Services
{
    public class FeiraLivreService : IFeiraLivreService
    {
        private readonly IMapper _mapper;
        private readonly IFeiraLivreRepository _feiraLivreRepository;

        public FeiraLivreService(IMapper mapper, IFeiraLivreRepository feiraLivreRepository)
        {
            _mapper = mapper;
            _feiraLivreRepository = feiraLivreRepository;
        }

        public async Task<FeiraLivreViewModel> Post(CadastrarFeiraLivreInputModel cadastrarFeiraLivreInputModel)
        {
            var entity = _mapper.Map<CadastrarFeiraLivreInputModel, FeiraLivreEntity>(cadastrarFeiraLivreInputModel);

            return _mapper.Map<FeiraLivreEntity, FeiraLivreViewModel>(await _feiraLivreRepository.Post(entity));
        }

        public async Task<List<FeiraLivreViewModel>> GetByDistrito(string distrito)
        {
            return _mapper.Map<List<FeiraLivreEntity>, List<FeiraLivreViewModel>>(await _feiraLivreRepository.GetByDistrito(distrito));
        }

        public async Task<List<FeiraLivreViewModel>> Get()
        {
            return _mapper.Map<List<FeiraLivreEntity>, List<FeiraLivreViewModel>>(await _feiraLivreRepository.Get());
        }

        public async Task Update(AlterarFeiraLivreInputModel alterarFeiraLivreInputModel)
        {
            await _feiraLivreRepository.Update(_mapper.Map<AlterarFeiraLivreInputModel, FeiraLivreEntity>(alterarFeiraLivreInputModel));
        }

        public async Task DeleteById(string id)
        {
            await _feiraLivreRepository.DeleteById(id);
        }
    }
}