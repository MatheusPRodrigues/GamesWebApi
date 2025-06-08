namespace GamesWebApi.Models
{
    public class JogoModel
    {
        public int Id { get; set; }
        public string Nome { get; set; }
        public string Descricao { get; set; }
        public ProdutoraModel[] Produtoras { get; set; } = new ProdutoraModel[3];
    }
}
