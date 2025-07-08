namespace GamesWebApi.Dto.Genero
{
    public class GeneroResponseDto
    {
        public int Id { get; set; }
        public string Nome { get; set; }
        public List<string> Jogos { get; set; }
    }
}
