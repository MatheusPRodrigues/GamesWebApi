using GamesWebApi.Dto.Jogo;
using GamesWebApi.Models;

namespace GamesWebApi.Services.Jogos
{
    public interface IJogoInterface
    {
        Task<ResponseModel<List<JogoResponseDto>>> ListarJogos();
        Task<ResponseModel<JogoResponseDto>> BuscarJogoPorId(int idJogo);
        Task<ResponseModel<List<JogoResponseDto>>> BuscarJogoPorIdProdutora(int idProdutora);
        Task<ResponseModel<List<JogoResponseDto>>> BuscarJogoPorIdGenero(int idGenero);
        Task<ResponseModel<List<JogoResponseDto>>> ListarJogosSemelhantes(int idJogo);
        Task<ResponseModel<JogoResponseDto>> CadastrarJogo(JogoCriacaoDto jogoCriacaoDto);
        Task<ResponseModel<JogoResponseDto>> EditarJogo(JogoEdicaoDto jogoEdicaoDto);
        Task<ResponseModel<List<JogoResponseDto>>> ExcluirJogo(int idJogo);
    }
}
