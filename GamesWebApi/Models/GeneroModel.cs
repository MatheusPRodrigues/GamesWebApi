using System.Text.Json.Serialization;

namespace GamesWebApi.Models
{
    public class GeneroModel
    {
        public int Id { get; set; }
        public string Nome { get; set; }
        public ICollection<JogoModel> Jogos { get; set; } = new List<JogoModel>();

        public GeneroModel()
        {
            
        }

        public GeneroModel(string nome)
        {
            Nome = nome;
            Jogos = new List<JogoModel>();
        }
    }
}
