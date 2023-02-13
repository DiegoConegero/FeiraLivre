using AutoMapper;
using FeiraLivre.Api.Models;
using FeiraLivre.Api.ViewModels;
using FeiraLivre.Core.Entities;
using FeiraLivre.Core.Interfaces.UseCases;
using FluentValidation;
using Microsoft.AspNetCore.Mvc;
using System.Net;

namespace FeiraLivre.Api.Controllers
{
    [Route("api/feiras-livres")]
    [ApiController]
    public class FeiraLivreController : ControllerBase
    {
        private readonly IMapper _mapper;
        private readonly IFeiraLivreUseCase _feiraLivreUseCase;

        public FeiraLivreController(IMapper mapper,  IFeiraLivreUseCase feiraLivreUseCase, IValidator<CadastrarFeiraLivreInputModel> cadastrarFeiraLivreValidator, IValidator<AlterarFeiraLivreInputModel> alterarFeiraLivreValidator)
        {
            _mapper                         = mapper;
            _feiraLivreUseCase              = feiraLivreUseCase;
        }

        /// <summary>
        /// Cadastra nova feira
        /// </summary>
        /// <param name="inputModel">Modelo de feira</param>
        /// <response code="201">Created</response>
        /// <response code="400">BadRequest</response>
        /// <response code="406">NotAcceptable</response>
        /// <returns>Feira criada</returns>
        [HttpPost]
        [ProducesResponseType((int)HttpStatusCode.Created)]
        [ProducesResponseType((int)HttpStatusCode.BadRequest)]
        [ProducesResponseType(typeof(FeiraLivreViewModel), (int)HttpStatusCode.Created)]
        [Produces("application/json")]
        public async Task<IActionResult> Cadastrar([FromBody] CadastrarFeiraLivreInputModel inputModel)
        {
            var result = await _feiraLivreUseCase.Cadastrar(_mapper.Map<CadastrarFeiraLivreInputModel, FeiraLivreEntity>(inputModel));
            
            return CreatedAtAction(nameof(Listar), _mapper.Map<FeiraLivreEntity, FeiraLivreViewModel>(result));
        }

        /// <summary>
        /// Retorna lista com todas as feiras livres em determinado distrito
        /// </summary>
        /// <response code="200">Success</response>
        /// <response code="404">NotFound</response>
        /// <returns>Lista de feiras de acordo com o distrito</returns>
        [HttpGet("{distrito}")]
        [ProducesResponseType((int)HttpStatusCode.OK)]
        [ProducesResponseType((int)HttpStatusCode.NotFound)]
        [ProducesResponseType(typeof(List<FeiraLivreViewModel>), (int)HttpStatusCode.OK)]
        [Produces("application/json")]
        public async Task<IActionResult> ListarPorDistrito(string distrito)
        {
            var result = await _feiraLivreUseCase.ListarPorDistrito(distrito);

            if (result == null)
                return NotFound();

            return Ok(_mapper.Map<List<FeiraLivreEntity>, List<FeiraLivreViewModel>>(result));
        }

        /// <summary>
        /// Retorna lista com todas as feiras livres
        /// </summary>
        /// <response code="200">Success</response>
        /// <response code="404">NotFound</response>
        /// <returns>Lista com todas as ferias livres</returns>
        [HttpGet()]
        [ProducesResponseType((int)HttpStatusCode.OK)]
        [ProducesResponseType((int)HttpStatusCode.NotFound)]
        [ProducesResponseType(typeof(List<FeiraLivreViewModel>), (int)HttpStatusCode.OK)]
        [Produces("application/json")]
        public async Task<IActionResult> Listar()
        {
            var response = await _feiraLivreUseCase.Listar();

            if (response == null)
                return NotFound();

            return Ok(_mapper.Map<List<FeiraLivreEntity>, List<FeiraLivreViewModel>>(response));
        }

        /// <summary>
        /// Altera informações de determinada feira
        /// </summary>
        /// <param name="inputModel">Modelo de feira</param>
        /// <response code="204">NoContent</response>
        /// <response code="400">BadRequest</response>
        /// <response code="406">NotAcceptable</response>
        [HttpPut]
        [ProducesResponseType((int)HttpStatusCode.NoContent)]
        [ProducesResponseType((int)HttpStatusCode.BadRequest)]
        [ProducesResponseType((int)HttpStatusCode.NotAcceptable)]
        public async Task<IActionResult> Atualizar([FromBody] AlterarFeiraLivreInputModel inputModel)
        {
         await _feiraLivreUseCase.Atualizar(_mapper.Map<AlterarFeiraLivreInputModel, FeiraLivreEntity>(inputModel));

            return NoContent();
        }

        /// <summary>
        /// Deleta determinada feira
        /// </summary>
        /// <response code="204">NoContent</response>
        /// <response code="400">BadRequest</response>
        /// <response code="406">NotAcceptable</response>
        [HttpDelete("{id}")]
        [ProducesResponseType((int)HttpStatusCode.NoContent)]
        [ProducesResponseType((int)HttpStatusCode.BadRequest)]
        [ProducesResponseType((int)HttpStatusCode.NotAcceptable)]
        public async Task<IActionResult> DeletarPorId(string id)
        {
            await _feiraLivreUseCase.DeletarPorId(id);

            return NoContent();
        }
    }
}