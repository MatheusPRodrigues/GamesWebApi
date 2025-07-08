using GamesWebApi.Models;

namespace GamesWebApi.Dto.Jogo
{
    public class JogoResponseDto
    {
        public int Id { get; set; }
        public string Nome { get; set; }
        public string Descricao { get; set; }
        public ProdutoraModel Produtora { get; set; }
        public List<string> Generos { get; set; }
    }
}
