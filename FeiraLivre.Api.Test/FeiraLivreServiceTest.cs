using AutoMapper;
using FeiraLivre.Application.InputModels;
using FeiraLivre.Application.Interfaces;
using FeiraLivre.Application.Services;
using FeiraLivre.Core.Constants;
using FeiraLivre.Core.Entities;
using FeiraLivre.Core.Interfaces;
using Microsoft.Extensions.DependencyInjection;
using Moq;
using System.Data;
using System.Net;
using System.Net.NetworkInformation;
using System.Reflection.PortableExecutable;
using System.Runtime.Serialization;

namespace FeiraLivre.Api.Test
{
    public class FeiraLivreServiceTest
    {
        private IFeiraLivreService _feiraLivreService;
        private readonly Mock<IFeiraLivreRepository> _mockFeiraLivreRepository = new();
        private readonly Mock<IMapper> _mockMapper = new();

        public FeiraLivreService GetFeiraService()
        {
            return new FeiraLivreService(_mockMapper.Object, _mockFeiraLivreRepository.Object);
        }

        #region Inserir

        // Método - Teste - Retorno
        [Theory]
        [MemberData(nameof(DadosValidosInserirFeiraLivre))]
        public async Task InserirDadosValidosRetornaFeiraLivre(CadastrarFeiraLivreInputModel feiraLivreInputModel)
        {
            _mockFeiraLivreRepository
                .Setup(s => s.Post(It.IsAny<FeiraLivreEntity>()))
                .ReturnsAsync(new FeiraLivreEntity());

            var service = GetFeiraService();

            var data = await service.Post(feiraLivreInputModel);

            Assert.NotNull(data);

            _mockFeiraLivreRepository.Verify(m => m.Post(It.IsAny<FeiraLivreEntity>()), Times.Once);
        }

        [Theory]
        [MemberData(nameof(DadosInvalidosInserirFeiraLivre))]
        public async Task InserirDadosInvalidosRetornaExcecao(CadastrarFeiraLivreInputModel feiraLivreInputModel, string descricaoErro)
        {
            _mockFeiraLivreRepository
                .Setup(s => s.Post(It.IsAny<FeiraLivreEntity>()))
                .ReturnsAsync(new FeiraLivreEntity());

            var service = GetFeiraService();

            Func<Task> data = async () => await service.Post(feiraLivreInputModel);

            var exception = await Assert.ThrowsAsync<ArgumentException>(data);
            Assert.Equal(descricaoErro, exception.Message);

            _mockFeiraLivreRepository.Verify(m => m.Post(It.IsAny<FeiraLivreEntity>()), Times.Never);
        }

        [Theory]
        [MemberData(nameof(DadosExistentesInserirFeiraLivre))]
        public async Task InserirDadosExistentesRetornaExcecao(CadastrarFeiraLivreInputModel feiraLivreInputModel, string descricaoErro)
        {
            _mockFeiraLivreRepository
                .Setup(s => s.Post(It.IsAny<FeiraLivreEntity>()))
                .ReturnsAsync(new FeiraLivreEntity());

            var service = GetFeiraService();

            Func<Task> data = async () => await service.Post(feiraLivreInputModel);

            var exception = await Assert.ThrowsAsync<ConstraintException>(data);
            Assert.Equal(descricaoErro, exception.Message);

            _mockFeiraLivreRepository.Verify(m => m.Post(It.IsAny<FeiraLivreEntity>()), Times.Never);
        }

        #endregion

        #region Consultar

        [Theory]
        [InlineData("Santana do Parnaiba")]
        public async Task ConsultarPorDistritoDadosValidosRetornaListaFeiraLivre(string distrito)
        {
            _mockFeiraLivreRepository
                .Setup(s => s.GetByDistrito(It.IsAny<string>()))
                .ReturnsAsync(new List<FeiraLivreEntity>());

            var service = GetFeiraService();

            var data = await service.GetByDistrito(distrito);

            Assert.NotNull(data);
            _mockFeiraLivreRepository.Verify(m => m.GetByDistrito(It.IsAny<string>()), Times.Once);
        }

        [Theory]
        [InlineData("Santana do Parnaiba")]
        public async Task ConsultarPorDistritoDadosInvalidosRetornaExcecao(string distrito)
        {
            _mockFeiraLivreRepository
               .Setup(s => s.GetByDistrito(It.IsAny<string>()))
               .ReturnsAsync(new List<FeiraLivreEntity>());

            var service = GetFeiraService();

            Func<Task> data = async () => await service.GetByDistrito(distrito);

            var exception = await Assert.ThrowsAsync<KeyNotFoundException>(data);
            Assert.Equal(MensagensErroConstant.NenhumaEncontrada, exception.Message);

            _mockFeiraLivreRepository.Verify(m => m.GetByDistrito(It.IsAny<string>()), Times.Never);
        }

        #endregion

        #region Alterar

        [Theory]
        [MemberData(nameof(DadosValidosAlterarFeiraLivre))]
        public async Task AlterarDadosValidosSemRetorno(AlterarFeiraLivreInputModel feiraLivreInputModel)
        {
            _mockFeiraLivreRepository
                .Setup(s => s.Update(It.IsAny<FeiraLivreEntity>()));

            var service = GetFeiraService();

            await service.Update(feiraLivreInputModel);

            _mockFeiraLivreRepository.Verify(m => m.Update(It.IsAny<FeiraLivreEntity>()), Times.Once);
        }

        [Theory]
        [MemberData(nameof(DadosInvalidosAlterarFeiraLivre))]
        public async Task AlterarDadosInvalidosRetornaExcecao(AlterarFeiraLivreInputModel feiraLivreInputModel, string descricaoErro)
        {
            _mockFeiraLivreRepository
                .Setup(s => s.Update(It.IsAny<FeiraLivreEntity>()));

            var service = GetFeiraService();

            Func<Task> data = async () => await service.Update(feiraLivreInputModel);

            var exception = await Assert.ThrowsAsync<ArgumentException>(data);
            Assert.Equal(descricaoErro, exception.Message);

            _mockFeiraLivreRepository.Verify(m => m.Update(It.IsAny<FeiraLivreEntity>()), Times.Never);
        }

        #endregion


        #region Dados

        #region Inserir

        public static IEnumerable<object[]> DadosValidosInserirFeiraLivre = new[]
        {
            new object[]
                {
                    new CadastrarFeiraLivreInputModel()
                    {
                        Nome        = "Feira do Diego",
                        Bairro      = "Santa Terezinha",
                        Distrito    = "Centro",
                        Regiao      = "Sul"
                    }
                }
        };

        public static IEnumerable<object[]> DadosInvalidosInserirFeiraLivre = new[]
        {
            new object[]
            {
                new CadastrarFeiraLivreInputModel()
                {
                    Nome        = string.Empty,
                    Bairro      = "Santa Terezinha",
                    Distrito    = "Centro",
                    Regiao      = "Sul"
                },
                MensagensErroConstant.NomeObrigatorio
            },
            new object[]
            {
                new CadastrarFeiraLivreInputModel()
                {
                    Nome        = "Feira sem bairro",
                    Bairro      = string.Empty,
                    Distrito    = "Centro",
                    Regiao      = "Sul"
                },
                MensagensErroConstant.BairroObrigatorio
            },
            new object[]
            {
                new CadastrarFeiraLivreInputModel()
                {
                    Nome        = "Feira sem distrito",
                    Bairro      = "Santa Terezinha",
                    Distrito    = string.Empty,
                    Regiao      = "Sul"
                },
                MensagensErroConstant.DistritoObrigatorio
            },
            new object[]
            {
                new CadastrarFeiraLivreInputModel()
                {
                    Nome        = "Feira sem regiao",
                    Bairro      = "Santa Terezinha",
                    Distrito    = "Centro",
                    Regiao      = string.Empty
                },
                MensagensErroConstant.RegiaoObrigatorio
            }
        };

        public static IEnumerable<object[]> DadosExistentesInserirFeiraLivre = new[]
        {
            new object[]
                {
                    new CadastrarFeiraLivreInputModel()
                    {
                        Nome        = "JaExiste",
                        Bairro      = "Santa Terezinha",
                        Distrito    = "Centro",
                        Regiao      = "Sul"
                    },
                    MensagensErroConstant.FeiraExistente
                }
        };

        #endregion

        #region Alterar 

        public static IEnumerable<object[]> DadosValidosAlterarFeiraLivre = new[]
        {
            new object[]
                {
                    new AlterarFeiraLivreInputModel()
                    {
                        Id          = "64946464sad6sada6d67",
                        Nome        = "Feira alterada",
                        Bairro      = "Santa Terezinha",
                        Distrito    = "Centro",
                        Regiao      = "Sul"
                    }
                }
        };

        public static IEnumerable<object[]> DadosInvalidosAlterarFeiraLivre = new[]
        {
            new object[]
            {
                new AlterarFeiraLivreInputModel()
                {
                    Id          = "64946464sad6sada6d67",
                    Nome        = string.Empty,
                    Bairro      = "Santa Terezinha",
                    Distrito    = "Centro",
                    Regiao      = "Sul"
                },
                MensagensErroConstant.NomeObrigatorio
            },
            new object[]
            {
                new AlterarFeiraLivreInputModel()
                {
                    Id          = "64946464sad6sada6d67",
                    Nome        = "Feira sem bairro",
                    Bairro      = string.Empty,
                    Distrito    = "Centro",
                    Regiao      = "Sul"
                },
                MensagensErroConstant.BairroObrigatorio
            },
            new object[]
            {
                new AlterarFeiraLivreInputModel()
                {
                    Id          = "64946464sad6sada6d67",
                    Nome        = "Feira sem distrito",
                    Bairro      = "Santa Terezinha",
                    Distrito    = string.Empty,
                    Regiao      = "Sul"
                },
                MensagensErroConstant.DistritoObrigatorio
            },
            new object[]
            {
                new AlterarFeiraLivreInputModel()
                {
                    Id          = "64946464sad6sada6d67",
                    Nome        = "Feira sem regiao",
                    Bairro      = "Santa Terezinha",
                    Distrito    = "Centro",
                    Regiao      = string.Empty
                },
                MensagensErroConstant.RegiaoObrigatorio
            }
        };

        #endregion

        #endregion
    }
}