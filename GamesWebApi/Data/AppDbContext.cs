using GamesWebApi.Models;
using Microsoft.EntityFrameworkCore;

namespace GamesWebApi.Data
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
        {
            
        }

        public DbSet<GeneroModel> Generos { get; set; }
        public DbSet<JogoModel> Jogos { get; set; }
        public DbSet<ProdutoraModel> Produtoras { get; set; }
    }
}
