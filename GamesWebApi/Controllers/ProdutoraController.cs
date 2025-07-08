using GamesWebApi.Dto.Produtora;
using GamesWebApi.Models;
using GamesWebApi.Services.Produtora;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace GamesWebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProdutoraController : ControllerBase
    {
        private readonly IProdutoraInterface _produtoraInterface;

        public ProdutoraController(IProdutoraInterface produtoraInterface)
        {
            _produtoraInterface = produtoraInterface;
        }

        [HttpGet("ExibirProdutoras")]
        public async Task<ActionResult<ResponseModel<List<ProdutoraModel>>>> ExibirProdutoras()
        {
            var produtoras = await _produtoraInterface.ExibirProdutoras();

            return Ok(produtoras);
        }

        [HttpGet("BuscarProdutoraPorId")]
        public async Task<ActionResult<ResponseModel<ProdutoraModel>>> BuscarProdutoraPorId(int idProdutora)
        {
            var produtora = await _produtoraInterface.BuscarProdutoraPorId(idProdutora);

            return Ok(produtora);
        }

        [HttpGet("BuscarProdutoraPorIdJogo")]
        public async Task<ActionResult<ResponseModel<ProdutoraModel>>> BuscarProdutoraPorIdJogo(int idJogo)
        {
            var produtora = await _produtoraInterface.BuscarProdutoraPorIdJogo(idJogo);

            return Ok(produtora);
        }

        [HttpPost("CadastrarProdutora")]
        public async Task<ActionResult<ResponseModel<ProdutoraModel>>> CadastrarProdutora(ProdutoraCriacaoDto produtoraCriacaoDto)
        {
            var produtora = await _produtoraInterface.CadastrarProdutora(produtoraCriacaoDto);

            return Ok(produtora);
        }

        [HttpPut("EditarProdutora")]
        public async Task<ActionResult<ResponseModel<ProdutoraModel>>> EditarProdutora(ProdutoraEdicaoDto produtoraEdicaoDto)
        {
            var produtora = await _produtoraInterface.EditarProdutora(produtoraEdicaoDto);

            return Ok(produtora);
        }

        [HttpDelete("ExcluirProdutora")]
        public async Task<ActionResult<ResponseModel<List<ProdutoraModel>>>> ExcluirProdutora(int idProdutora)
        {
            var produtora = await _produtoraInterface.ExcluirProdutora(idProdutora);

            return Ok(produtora);
        }
    }
}
