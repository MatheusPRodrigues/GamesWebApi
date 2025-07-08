using GamesWebApi.Dto.Genero;
using GamesWebApi.Models;

namespace GamesWebApi.Services.Generos
{
    public interface IGeneroInterface
    {
        Task<ResponseModel<List<GeneroResponseDto>>> ExibirGeneros();
        Task<ResponseModel<GeneroResponseDto>> BuscarGeneroPorId(int idGenero);
        Task<ResponseModel<List<GeneroResponseDto>>> BuscarGenerosPorIdJogo(int idJogo);
        Task<ResponseModel<GeneroResponseDto>> CadastrarGenero(GeneroCriacaoDto generoCriacaoDto);
        Task<ResponseModel<GeneroResponseDto>> EditarGenero(GeneroEdicaoDto generoEdicaoDto);
        Task<ResponseModel<List<GeneroResponseDto>>> ExcluirGenero(int idGenero);
    }
}
