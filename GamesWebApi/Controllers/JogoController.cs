using GamesWebApi.Dto.Jogo;
using GamesWebApi.Models;
using GamesWebApi.Services.Jogos;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace GamesWebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class JogoController : ControllerBase
    {
        private readonly IJogoInterface _jogoInterface;
        public JogoController(IJogoInterface jogoInterface)
        {
            _jogoInterface = jogoInterface;
        }

        [HttpGet("ListarJogos")]
        public async Task<ActionResult<ResponseModel<List<JogoResponseDto>>>> ListarJogos()
        {
            var jogos = await _jogoInterface.ListarJogos();

            return Ok(jogos);
        }

        [HttpGet("BuscarJogoPorId")]
        public async Task<ActionResult<ResponseModel<JogoResponseDto>>> BuscarJogoPorId(int idJogo)
        {
            var jogo = await _jogoInterface.BuscarJogoPorId(idJogo);

            return Ok(jogo);
        }

        [HttpGet("BuscarJogoPorIdProdutora")]
        public async Task<ActionResult<ResponseModel<List<JogoResponseDto>>>> BuscarJogoPorIdProdutora(int idProdutora)
        {
            var jogos = await _jogoInterface.BuscarJogoPorIdProdutora(idProdutora);

            return Ok(jogos);
        }

        [HttpGet("BuscarJogoPorIdGenero")]
        public async Task<ActionResult<ResponseModel<List<JogoResponseDto>>>> BuscarJogoPorIdGenero(int idGenero)
        {
            var jogos = await _jogoInterface.BuscarJogoPorIdGenero(idGenero);

            return Ok(jogos);
        }

        [HttpGet("ListarJogosSemelhantes")]
        public async Task<ActionResult<ResponseModel<List<JogoResponseDto>>>> ListarJogosSemelhantes(int idJogo)
        {
            var jogos = await _jogoInterface.ListarJogosSemelhantes(idJogo);

            return Ok(jogos);
        }

        [HttpPost("CadastrarJogo")]
        public async Task<ActionResult<ResponseModel<JogoResponseDto>>> CadastrarJogo(JogoCriacaoDto jogoCriacaoDto)
        {
            var jogo = await _jogoInterface.CadastrarJogo(jogoCriacaoDto);

            return Ok(jogo);
        }

        [HttpPut("EditarJogo")]
        public async Task<ActionResult<ResponseModel<JogoResponseDto>>> EditarJogo(JogoEdicaoDto jogoEdicaoDto)
        {
            var jogo = await _jogoInterface.EditarJogo(jogoEdicaoDto);

            return Ok(jogo);
        }

        [HttpDelete("ExcluirJogo")]
        public async Task<ActionResult<ResponseModel<List<JogoResponseDto>>>> ExcluirJogo(int idJogo)
        {
            var jogos = await _jogoInterface.ExcluirJogo(idJogo);

            return Ok(jogos);
        }
    }
}
