namespace GamesWebApi.Models
{
    public class JogoModel
    {
        public int Id { get; set; }
        public string Nome { get; set; }
        public string Descricao { get; set; }
        public ProdutoraModel Produtora { get; set; }
        public ICollection<GeneroModel> Generos { get; set; } = new List<GeneroModel>();

        public JogoModel()
        {            
        }

        public JogoModel(string nome, string descricao, ProdutoraModel produtora, ICollection<GeneroModel> generos)
        {
            Nome = nome;
            Descricao = descricao;
            Produtora = produtora;
            Generos = generos;
        }
    }
}
