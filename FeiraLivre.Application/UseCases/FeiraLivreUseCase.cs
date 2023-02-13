using FeiraLivre.Core.Interfaces.UseCases;
using FeiraLivre.Core.Entities;
using System.Data;
using FeiraLivre.Core.Constants;
using FeiraLivre.Core.Interfaces.Repository;
using FluentValidation;

namespace FeiraLivre.Application.UseCases
{
    public class FeiraLivreUseCase : IFeiraLivreUseCase
    {
        private readonly IFeiraLivreRepository _feiraLivreRepository;
        private readonly IValidator<FeiraLivreEntity> _feiraLivreEntityValidator;

        public FeiraLivreUseCase(IFeiraLivreRepository feiraLivreRepository, IValidator<FeiraLivreEntity> feiraLivreEntityValidator)
        {
            _feiraLivreRepository       = feiraLivreRepository;
            _feiraLivreEntityValidator  = feiraLivreEntityValidator;
        }

        public async Task<FeiraLivreEntity> Cadastrar(FeiraLivreEntity feiraLivreEntity)
        {
            var validationResult = _feiraLivreEntityValidator.Validate(feiraLivreEntity);

            if (!validationResult.IsValid)
                throw new ArgumentException(validationResult.Errors.First().ToString());

            if (await _feiraLivreRepository.ObterPorBairroENome(feiraLivreEntity.Bairro, feiraLivreEntity.Nome) != null)
                throw new ConstraintException(MensagensErroConstant.FeiraExistente);

            return await _feiraLivreRepository.Cadastrar(feiraLivreEntity);
        }

        public async Task<List<FeiraLivreEntity>> ListarPorDistrito(string distrito)
        {
            var feiraLivreList = await _feiraLivreRepository.ListarPorDistrito(distrito);

            if (feiraLivreList.Count == 0)
                throw new KeyNotFoundException(MensagensErroConstant.ListaNaoEncontradaDistrito);

            return feiraLivreList;
        }

        public async Task<List<FeiraLivreEntity>> Listar()
        {
            var feiraLivreList = await _feiraLivreRepository.Listar();

            if (feiraLivreList.Count == 0)
                throw new KeyNotFoundException(MensagensErroConstant.ListaNaoEncontrada);

            return feiraLivreList;
        }

        public async Task Atualizar(FeiraLivreEntity feiraLivreEntity)
        {
            var validationResult = _feiraLivreEntityValidator.Validate(feiraLivreEntity);

            if (!validationResult.IsValid)
                throw new ArgumentException(validationResult.Errors.First().ToString());

            if (!await _feiraLivreRepository.Atualizar(feiraLivreEntity))
                throw new ArgumentException(MensagensErroConstant.NaoEncontrada);
        }

        public async Task DeletarPorId(string id)
        {
            var feiraDeletada = await _feiraLivreRepository.DeletarPorId(id);

            if (!feiraDeletada)
                throw new ArgumentException(MensagensErroConstant.NaoEncontrada);
        }
    }
}