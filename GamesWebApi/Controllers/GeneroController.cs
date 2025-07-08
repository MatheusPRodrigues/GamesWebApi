using GamesWebApi.Dto.Genero;
using GamesWebApi.Models;
using GamesWebApi.Services.Generos;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace GamesWebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class GeneroController : ControllerBase
    {
        private readonly IGeneroInterface _generoInterface;

        public GeneroController(IGeneroInterface generoInterface)
        {
            _generoInterface = generoInterface;
        }

        [HttpGet("ExibirGeneros")]
        public async Task<ActionResult<ResponseModel<List<GeneroResponseDto>>>> ExibirGeneros()
        {
            var generos = await _generoInterface.ExibirGeneros();

            return Ok(generos);
        }

        [HttpPost("CadastrarGenero")]
        public async Task<ActionResult<ResponseModel<GeneroResponseDto>>> CadastrarGenero(GeneroCriacaoDto generoCriacaoDto)
        {
            var genero = await _generoInterface.CadastrarGenero(generoCriacaoDto);

            return Ok(genero);
        }

        [HttpGet("BuscarGeneroPorId")]
        public async Task<ActionResult<ResponseModel<GeneroResponseDto>>> BuscarGeneroPorId(int idGenero)
        {
            var genero = await _generoInterface.BuscarGeneroPorId(idGenero);

            return Ok(genero);
        }

        [HttpGet("BuscarGenerosPorIdJogo")]
        public async Task<ActionResult<ResponseModel<List<GeneroResponseDto>>>> BuscarGenerosPorIdJogo(int idJogo)
        {
            var generos = await _generoInterface.BuscarGenerosPorIdJogo(idJogo);

            return Ok(generos);
        }

        [HttpPut("EditarGenero")]
        public async Task<ActionResult<ResponseModel<GeneroResponseDto>>> EditarGenero(GeneroEdicaoDto generoEdicaoDto)
        {
            var genero = await _generoInterface.EditarGenero(generoEdicaoDto);

            return Ok(genero);
        }

        [HttpDelete("ExcluirGenero")]
        public async Task<ActionResult<ResponseModel<List<GeneroResponseDto>>>> ExcluirGenero(int idGenero)
        {
            var genero = await _generoInterface.ExcluirGenero(idGenero);

            return Ok(genero);
        }
    }
}
