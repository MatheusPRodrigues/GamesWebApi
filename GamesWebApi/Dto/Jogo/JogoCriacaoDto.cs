namespace GamesWebApi.Dto.Jogo
{
    public class JogoCriacaoDto
    {
        public string Nome { get; set; }
        public string Descricao { get; set; }
        public int idProdutora { get; set; }
        public List<int> idGenero { get; set; }
    }
}
