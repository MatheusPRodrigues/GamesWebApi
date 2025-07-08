namespace GamesWebApi.Dto.Jogo
{
    public class JogoEdicaoDto
    {
        public int Id { get; set; }
        public string Nome { get; set; }
        public string Descricao { get; set; }
        public int IdProdutora { get; set; }
        public List<int> idGeneros { get; set; }
    }
}
