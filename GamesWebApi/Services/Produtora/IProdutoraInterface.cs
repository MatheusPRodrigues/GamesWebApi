using GamesWebApi.Dto.Produtora;
using GamesWebApi.Models;

namespace GamesWebApi.Services.Produtora
{
    public interface IProdutoraInterface
    {
        Task<ResponseModel<List<ProdutoraModel>>> ExibirProdutoras();
        Task<ResponseModel<ProdutoraModel>> BuscarProdutoraPorId(int idProdutora);
        Task<ResponseModel<ProdutoraModel>> BuscarProdutoraPorIdJogo(int idJogo);
        Task<ResponseModel<ProdutoraModel>> CadastrarProdutora(ProdutoraCriacaoDto produtoraCriacaoDto);
        Task<ResponseModel<ProdutoraModel>> EditarProdutora(ProdutoraEdicaoDto produtoraEdicaoDto);
        Task<ResponseModel<List<ProdutoraModel>>> ExcluirProdutora(int idProdutora);
    }
}
