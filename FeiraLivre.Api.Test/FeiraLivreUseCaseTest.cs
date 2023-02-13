using AutoFixture;
using FeiraLivre.Application.UseCases;
using FeiraLivre.Core.Constants;
using FeiraLivre.Core.Entities;
using FeiraLivre.Core.Entities.Validators;
using FeiraLivre.Core.Interfaces.Repository;
using FluentValidation;
using Moq;
using System.Data;

namespace FeiraLivre.Api.Test
{
    public class FeiraLivreUseCaseTest
    {
        private readonly Mock<IFeiraLivreRepository> _mockFeiraLivreRepository = new();
        private readonly IValidator<FeiraLivreEntity> _validator = new FeiraLivreEntityValidator();

        public FeiraLivreUseCase GetFeiraService()
        {
            return new FeiraLivreUseCase(_mockFeiraLivreRepository.Object, _validator);
        }

        #region Cadastrar

        // Método - Teste - Retorno
        [Theory]
        [MemberData(nameof(DadosValidosCadastrarFeiraLivre))]
        public async Task CadastrarDadosValidosRetornaFeiraLivre(FeiraLivreEntity feiraLivreEntity)
        {
            var fixture = new Fixture();
            var feiraLivreFixture= fixture.Create<FeiraLivreEntity>();

            _mockFeiraLivreRepository
                .Setup(s => s.Cadastrar(It.IsAny<FeiraLivreEntity>()))
                .ReturnsAsync(feiraLivreFixture);

            var service = GetFeiraService();

            var result = await service.Cadastrar(feiraLivreEntity);

            Assert.NotNull(result);
            Assert.NotEmpty(result.Id);
            Assert.Equal(result.Nome, feiraLivreFixture.Nome);
            Assert.Equal(result.Bairro, feiraLivreFixture.Bairro);
            Assert.Equal(result.Regiao, feiraLivreFixture.Regiao);
            Assert.Equal(result.Distrito, feiraLivreFixture.Distrito);

            _mockFeiraLivreRepository.Verify(m => m.Cadastrar(It.IsAny<FeiraLivreEntity>()), Times.Once);
        }

        [Theory]
        [MemberData(nameof(DadosInvalidosCadastrarFeiraLivre))]
        public async Task CadastrarDadosInvalidosRetornaExcecao(FeiraLivreEntity feiraLivreEntity, string descricaoErro)
        {
            var fixture = new Fixture();
            var feiraLivreFixture = fixture.Create<FeiraLivreEntity>();

            _mockFeiraLivreRepository
                .Setup(s => s.Cadastrar(It.IsAny<FeiraLivreEntity>()))
                .ReturnsAsync(new FeiraLivreEntity());

            var service = GetFeiraService();

            Func<Task> result = async () => await service.Cadastrar(feiraLivreEntity);

            var exception = await Assert.ThrowsAsync<ArgumentException>(result);
            Assert.Equal(descricaoErro, exception.Message);

            _mockFeiraLivreRepository.Verify(m => m.Cadastrar(It.IsAny<FeiraLivreEntity>()), Times.Never);
        }

        [Theory]
        [MemberData(nameof(DadosExistentesCadastrarFeiraLivre))]
        public async Task CadastrarDadosDuplicadosRetornaExcecao(FeiraLivreEntity feiraLivreEntity, string descricaoErro)
        {
            var fixture             = new Fixture();
            var feiraLivreFixture   = fixture.Create<FeiraLivreEntity>();

            _mockFeiraLivreRepository
                .Setup(s => s.Cadastrar(It.IsAny<FeiraLivreEntity>()))
                .ReturnsAsync(new FeiraLivreEntity());

            _mockFeiraLivreRepository
                .Setup(s => s.ObterPorBairroENome(It.IsAny<string>(), It.IsAny<string>()))
                .ReturnsAsync(feiraLivreFixture);

            var service = GetFeiraService();

            Func<Task> result = async () => await service.Cadastrar(feiraLivreEntity);

            var exception = await Assert.ThrowsAsync<ConstraintException>(result);
            Assert.Equal(descricaoErro, exception.Message);

            _mockFeiraLivreRepository.Verify(m => m.Cadastrar(It.IsAny<FeiraLivreEntity>()), Times.Never);
        }

        #endregion

        #region ConsultarPorDistrito

        [Theory]
        [InlineData("Santana do Parnaiba")]
        public async Task ConsultarPorDistritoDadosValidosRetornaListaFeiraLivre(string distrito)
        {
            var fixture = new Fixture();
            var feiraLivreResult = fixture.Create<List<FeiraLivreEntity>>();

            _mockFeiraLivreRepository
               .Setup(s => s.ListarPorDistrito(It.IsAny<string>()))
               .ReturnsAsync(feiraLivreResult);

            var service = GetFeiraService();

            var result = await service.ListarPorDistrito(distrito);

            Assert.NotNull(result);
            _mockFeiraLivreRepository.Verify(m => m.ListarPorDistrito(It.IsAny<string>()), Times.Once);
        }

        [Theory]
        [InlineData("Distrito Invalido")]
        public async Task ConsultarPorDistritoDadosInvalidosRetornaExcecao(string distrito)
        {
            _mockFeiraLivreRepository
                .Setup(s => s.ListarPorDistrito(It.IsAny<string>()))
                .ReturnsAsync(new List<FeiraLivreEntity>());

            var service = GetFeiraService();

            Func<Task> result = async () => await service.ListarPorDistrito(distrito);

            var exception = await Assert.ThrowsAsync<KeyNotFoundException>(result);
            Assert.Equal(MensagensErroConstant.ListaNaoEncontradaDistrito, exception.Message);

            _mockFeiraLivreRepository.Verify(m => m.ListarPorDistrito(It.IsAny<string>()), Times.Once);
        }

        #endregion

        #region Listar

        [Fact]
        public async Task ListarRetornaListaFeiraLivre()
        {
            var fixture = new Fixture();
            var feiraLivreResult = fixture.Create<List<FeiraLivreEntity>>();

            _mockFeiraLivreRepository
                .Setup(s => s.Listar())
                .ReturnsAsync(feiraLivreResult);

            var service = GetFeiraService();

            var result = await service.Listar();

            Assert.NotNull(result);
            Assert.True(result.Count > 0);

            _mockFeiraLivreRepository.Verify(m => m.Listar(), Times.Once);
        }

        [Fact]
        public async Task ListarRetornaExcecao()
        {
            _mockFeiraLivreRepository
                .Setup(s => s.Listar())
                .ReturnsAsync(new List<FeiraLivreEntity>());

            var service = GetFeiraService();

            Func<Task> result = async () => await service.Listar();

            var exception = await Assert.ThrowsAsync<KeyNotFoundException>(result);
            Assert.Equal(MensagensErroConstant.ListaNaoEncontrada, exception.Message);

            _mockFeiraLivreRepository.Verify(m => m.Listar(), Times.Once);
        }

        #endregion

        #region Alterar

        [Theory]
        [MemberData(nameof(DadosValidosAlterarFeiraLivre))]
        public async Task AlterarDadosValidosSemRetorno(FeiraLivreEntity feiraLivreEntity)
        {
            _mockFeiraLivreRepository
                .Setup(s => s.Atualizar(It.IsAny<FeiraLivreEntity>()))
                .ReturnsAsync(true);

            var service = GetFeiraService();

            await service.Atualizar(feiraLivreEntity);

            _mockFeiraLivreRepository.Verify(m => m.Atualizar(It.IsAny<FeiraLivreEntity>()), Times.Once);
        }

        [Theory]
        [MemberData(nameof(DadosInvalidosAlterarFeiraLivre))]
        public async Task AlterarDadosInvalidosRetornaExcecao(FeiraLivreEntity feiraLivreEntity, string descricaoErro)
        {
            _mockFeiraLivreRepository
                .Setup(s => s.Atualizar(It.IsAny<FeiraLivreEntity>()))
                .ReturnsAsync(true);

            var service = GetFeiraService();

            Func<Task> data = async () => await service.Atualizar(feiraLivreEntity);

            var exception = await Assert.ThrowsAsync<ArgumentException>(data);
            Assert.Equal(descricaoErro, exception.Message);

            _mockFeiraLivreRepository.Verify(m => m.Atualizar(It.IsAny<FeiraLivreEntity>()), Times.Never);
        }

        [Theory]
        [MemberData(nameof(DadosValidosAlterarFeiraLivre))]
        public async Task AlterarDadosInexistentesRetornaExcecao(FeiraLivreEntity feiraLivreEntity)
        {
            _mockFeiraLivreRepository
                .Setup(s => s.Atualizar(It.IsAny<FeiraLivreEntity>()))
                .ReturnsAsync(false);

            var service = GetFeiraService();

            Func<Task> data = async () => await service.Atualizar(feiraLivreEntity);

            var exception = await Assert.ThrowsAsync<ArgumentException>(data);
            Assert.Equal(MensagensErroConstant.NaoEncontrada, exception.Message);

            _mockFeiraLivreRepository.Verify(m => m.Atualizar(It.IsAny<FeiraLivreEntity>()), Times.Once);
        }

        #endregion

        #region Deletar

        [Fact]
        public async Task DeletarDadosValidosSemRetorno()
        {
            _mockFeiraLivreRepository
                .Setup(s => s.DeletarPorId(It.IsAny<string>()))
                .ReturnsAsync(true);

            var service = GetFeiraService();

            await service.DeletarPorId("63e7ee29592fb76a0a00af68");

            _mockFeiraLivreRepository.Verify(m => m.DeletarPorId(It.IsAny<string>()), Times.Once);
        }

        [Fact]
        public async Task DeletarDadosInvalidosRetornaExcecao()
        {
            _mockFeiraLivreRepository
                .Setup(s => s.DeletarPorId(It.IsAny<string>()))
                .ReturnsAsync(false);

            var service = GetFeiraService();

            Func<Task> data = async () => await service.DeletarPorId("");

            var exception = await Assert.ThrowsAsync<ArgumentException>(data);
            Assert.Equal(MensagensErroConstant.NaoEncontrada, exception.Message);

            _mockFeiraLivreRepository.Verify(m => m.DeletarPorId(It.IsAny<string>()), Times.Once);
        }

        #endregion

        #region Dados

        #region Cadastrar

        public static IEnumerable<object[]> DadosValidosCadastrarFeiraLivre = new[]
        {
            new object[]
                {
                    new FeiraLivreEntity()
                    {
                        Nome        = "Feira do Diego",
                        Bairro      = "Santa Terezinha",
                        Distrito    = "Centro",
                        Regiao      = "Sul"
                    }
                }
        };

        public static IEnumerable<object[]> DadosInvalidosCadastrarFeiraLivre = new[]
        {
            new object[]
            {
                new FeiraLivreEntity()
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
                new FeiraLivreEntity()
                {
                    Id          = "64946464sad6sada6d67",
                    Nome        = "Di",
                    Bairro      = "Santa Terezinha",
                    Distrito    = "Centro",
                    Regiao      = "Sul"
                },
                MensagensErroConstant.TamanhoNome
            },
            new object[]
            {
                new FeiraLivreEntity()
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
                new FeiraLivreEntity()
                {
                    Id          = "64946464sad6sada6d67",
                    Nome        = "Feira sem bairro",
                    Bairro      = "Du",
                    Distrito    = "Centro",
                    Regiao      = "Sul"
                },
                MensagensErroConstant.TamanhoBairro
            },
            new object[]
            {
                new FeiraLivreEntity()
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
                new FeiraLivreEntity()
                {
                    Id          = "64946464sad6sada6d67",
                    Nome        = "Feira sem distrito",
                    Bairro      = "Santa Terezinha",
                    Distrito    = "Di",
                    Regiao      = "Sul"
                },
                MensagensErroConstant.TamanhoDistrito
            },
            new object[]
            {
                new FeiraLivreEntity()
                {
                    Id          = "64946464sad6sada6d67",
                    Nome        = "Feira sem regiao",
                    Bairro      = "Santa Terezinha",
                    Distrito    = "Centro",
                    Regiao      = string.Empty
                },
                MensagensErroConstant.RegiaoObrigatorio
            },
            new object[]
            {
                new FeiraLivreEntity()
                {
                    Id          = "64946464sad6sada6d67",
                    Nome        = "Feira sem regiao",
                    Bairro      = "Santa Terezinha",
                    Distrito    = "Centro",
                    Regiao      = "Re"
                },
                MensagensErroConstant.TamanhoRegiao
            }
        };

        public static IEnumerable<object[]> DadosExistentesCadastrarFeiraLivre = new[]
        {
            new object[]
                {
                    new FeiraLivreEntity()
                    {
                        Nome        = "VILA FORMOSA",
                        Bairro      = "VL FORMOSA",
                        Distrito    = "Leste",
                        Regiao      = "VILA FORMOSA"
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
                    new FeiraLivreEntity()
                    {
                        Id          = "63e7edd33650256c8b335dd7",
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
                new FeiraLivreEntity()
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
                new FeiraLivreEntity()
                {
                    Id          = "64946464sad6sada6d67",
                    Nome        = "Di",
                    Bairro      = "Santa Terezinha",
                    Distrito    = "Centro",
                    Regiao      = "Sul"
                },
                MensagensErroConstant.TamanhoNome
            },
            new object[]
            {
                new FeiraLivreEntity()
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
                new FeiraLivreEntity()
                {
                    Id          = "64946464sad6sada6d67",
                    Nome        = "Feira sem bairro",
                    Bairro      = "Du",
                    Distrito    = "Centro",
                    Regiao      = "Sul"
                },
                MensagensErroConstant.TamanhoBairro
            },
            new object[]
            {
                new FeiraLivreEntity()
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
                new FeiraLivreEntity()
                {
                    Id          = "64946464sad6sada6d67",
                    Nome        = "Feira sem distrito",
                    Bairro      = "Santa Terezinha",
                    Distrito    = "Di",
                    Regiao      = "Sul"
                },
                MensagensErroConstant.TamanhoDistrito
            },
            new object[]
            {
                new FeiraLivreEntity()
                {
                    Id          = "64946464sad6sada6d67",
                    Nome        = "Feira sem regiao",
                    Bairro      = "Santa Terezinha",
                    Distrito    = "Centro",
                    Regiao      = string.Empty
                },
                MensagensErroConstant.RegiaoObrigatorio
            },
            new object[]
            {
                new FeiraLivreEntity()
                {
                    Id          = "64946464sad6sada6d67",
                    Nome        = "Feira sem regiao",
                    Bairro      = "Santa Terezinha",
                    Distrito    = "Centro",
                    Regiao      = "Re"
                },
                MensagensErroConstant.TamanhoRegiao
            }
        };

        public static IEnumerable<object[]> DadosInexistentesAlterarFeiraLivre = new[]
        {
            new object[]
                {
                    new FeiraLivreEntity()
                    {
                        Id          = "64946464sad6sada6d67",
                        Nome        = "Feira alterada",
                        Bairro      = "Santa Terezinha",
                        Distrito    = "Centro",
                        Regiao      = "Sul"
                    }
                }
        };

        #endregion

        #endregion
    }
}