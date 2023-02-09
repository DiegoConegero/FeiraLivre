using FeiraLivre.Application.InputModels;
using FeiraLivre.Application.Interfaces;
using FeiraLivre.Application.ViewModels;
using Microsoft.AspNetCore.Mvc;
using System.Net;

namespace FeiraLivre.Api.Controllers
{
    [Route("api/feiras-livres")]
    [ApiController]
    public class FeiraLivreController : ControllerBase
    {
        private readonly IFeiraLivreService _feiraLivreService;

        public FeiraLivreController(IFeiraLivreService feiraLivreService)
        {
            _feiraLivreService = feiraLivreService;
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
        [ProducesResponseType((int)HttpStatusCode.NotAcceptable)]
        [ProducesResponseType(typeof(FeiraLivreViewModel), (int)HttpStatusCode.Created)]
        [Produces("application/json")]
        public async Task<IActionResult> Post([FromBody] CadastrarFeiraLivreInputModel inputModel)
        {
            return CreatedAtAction(nameof(Get), await _feiraLivreService.Post(inputModel));
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
        public async Task<IActionResult> GetByDistrito(string distrito)
        {
            var response = await _feiraLivreService.GetByDistrito(distrito);

            if (response == null)
                return NotFound();

            return Ok(response);
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
        public async Task<IActionResult> Get()
        {
            var response = await _feiraLivreService.Get();

            if (response == null)
                return NotFound();

            return Ok(response);
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
        public async Task<IActionResult> Put([FromBody] AlterarFeiraLivreInputModel inputModel)
        {
            await _feiraLivreService.Update(inputModel);

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
        public async Task<IActionResult> DeleteById(string id)
        {
            await _feiraLivreService.DeleteById(id);

            return NoContent();
        }
    }
}