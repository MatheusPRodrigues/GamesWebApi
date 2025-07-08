using GamesWebApi.Data;
using GamesWebApi.Dto.Genero;
using GamesWebApi.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;

namespace GamesWebApi.Services.Generos
{
    public class GeneroService : IGeneroInterface
    {
        private readonly AppDbContext _context;

        public GeneroService(AppDbContext appDbContext)
        {
            _context = appDbContext;
        }

        public async Task<ResponseModel<GeneroResponseDto>> BuscarGeneroPorId(int idGenero)
        {
            ResponseModel<GeneroResponseDto> response = new ResponseModel<GeneroResponseDto>();

            try
            {
                var genero = await _context.Generos
                    .Where(g => g.Id == idGenero)
                    .Select(g => new GeneroResponseDto
                    {
                        Id = g.Id,
                        Nome = g.Nome,
                        Jogos = g.Jogos.Select(j => j.Nome).ToList()
                    })
                    .FirstOrDefaultAsync();

                if (genero == null)
                {
                    response.Mensagem = "Gênero não encontrado!";
                    return response;
                }

                response.Dados = genero;
                response.Mensagem = "Gênero localizado com sucesso!";

                return response;
            }
            catch (Exception ex)
            {
                response.Mensagem = ex.Message;
                response.Status = false;

                return response;
            }  
        }

        public async Task<ResponseModel<List<GeneroResponseDto>>> BuscarGenerosPorIdJogo(int idJogo)
        {
            ResponseModel<List<GeneroResponseDto>> response = new ResponseModel<List<GeneroResponseDto>>();

            try
            {
                var generos = await _context.Generos
                    .Include(g => g.Jogos)
                    .Where(g => g.Jogos.Any(j => j.Id == idJogo))
                    .Select(g => new GeneroResponseDto
                    {
                        Id = g.Id,
                        Nome = g.Nome,
                        Jogos = g.Jogos.Select(j => j.Nome).ToList()
                    })
                    .ToListAsync();

                if (generos == null || generos.Count < 1)
                {
                    response.Mensagem = "Jogo não encontrado!";
                    return response;
                }

                response.Dados = generos;
                response.Mensagem = "Jogo encontrado, esses são os gêneros que pertencem a ele!";
                
                return response;
            }
            catch (Exception ex)
            {
                response.Mensagem = ex.Message;
                response.Status = false;

                return response;
            }
        }

        public async Task<ResponseModel<GeneroResponseDto>> CadastrarGenero(GeneroCriacaoDto generoCriacaoDto)
        {
            ResponseModel<GeneroResponseDto> response = new ResponseModel<GeneroResponseDto>();

            try
            {
                if (generoCriacaoDto.Nome.IsNullOrEmpty())
                {
                    response.Mensagem = "Insira o nome do gênero!";
                    return response;
                }

                var genero = new GeneroModel(generoCriacaoDto.Nome);

                _context.Add(genero);
                await _context.SaveChangesAsync();

                var novoGenero = await _context.Generos
                    .OrderByDescending(g => g.Id)
                    .Select(g => new GeneroResponseDto
                    {
                        Id = g.Id,
                        Nome = g.Nome,
                        Jogos = g.Jogos.Select(j => j.Nome).ToList()
                    })
                    .FirstOrDefaultAsync();

                response.Dados = novoGenero;
                response.Mensagem = "Gênero cadastrado com sucesso!";

                return response;
            }
            catch (Exception ex)
            {
                response.Mensagem = ex.Message;
                response.Status = false;

                return response;
            } 
        }

        public async Task<ResponseModel<GeneroResponseDto>> EditarGenero(GeneroEdicaoDto generoEdicaoDto)
        {
            ResponseModel<GeneroResponseDto> response = new ResponseModel<GeneroResponseDto>();

            try
            {
                var genero = await _context.Generos.FirstOrDefaultAsync(g => g.Id == generoEdicaoDto.Id);

                if (genero == null)
                {
                    response.Mensagem = "Gênero não encontrado!";
                    return response;
                }

                if (generoEdicaoDto.Nome.IsNullOrEmpty())
                {
                    response.Mensagem = "Insira um nome para do gênero!";
                    return response;
                }

                genero.Nome = generoEdicaoDto.Nome;

                _context.Update(genero);
                await _context.SaveChangesAsync();

                var generoEditado = await _context.Generos
                    .Where(g => g.Id == generoEdicaoDto.Id)
                    .Select(g => new GeneroResponseDto
                    {
                        Id = g.Id,
                        Nome = g.Nome,
                        Jogos = g.Jogos.Select(j => j.Nome).ToList()
                    })
                    .FirstOrDefaultAsync();

                response.Dados = generoEditado;
                response.Mensagem = "Gênero editado com sucesso!";

                return response;
            }
            catch (Exception ex)
            {
                response.Mensagem = ex.Message;
                response.Status = false;

                return response;
            }
        }

        public async Task<ResponseModel<List<GeneroResponseDto>>> ExcluirGenero(int idGenero)
        {
            ResponseModel<List<GeneroResponseDto>> response = new ResponseModel<List<GeneroResponseDto>>();

            try
            {
                var genero = await _context.Generos.FirstOrDefaultAsync(g => g.Id == idGenero);

                if (genero == null)
                {
                    response.Mensagem = "Gênero não encontrado!";
                    return response;
                }

                _context.Remove(genero);
                await _context.SaveChangesAsync();

                var generos = await _context.Generos
                    .Select(g => new GeneroResponseDto
                    {
                        Id = g.Id,
                        Nome = g.Nome,
                        Jogos = g.Jogos.Select(j => j.Nome).ToList()
                    })
                    .ToListAsync();

                response.Dados = generos;
                response.Mensagem = "Gênero excluído com sucesso! Listando gêneros disponíveis!";

                return response;
            }
            catch (Exception ex)
            {
                response.Mensagem = ex.Message;
                response.Status = false;

                return response;
            }
        }

        public async Task<ResponseModel<List<GeneroResponseDto>>> ExibirGeneros()
        {
            ResponseModel<List<GeneroResponseDto>> response = new ResponseModel<List<GeneroResponseDto>>();

            try
            {
                var generos = await _context.Generos
                    .Select(g => new GeneroResponseDto
                    {
                        Id = g.Id,
                        Nome = g.Nome,
                        Jogos = g.Jogos.Select(j => j.Nome).ToList()
                    })
                    .ToListAsync();

                if (generos == null)
                {
                    response.Mensagem = "Nenhum gênero localizado!";
                    return response;
                }

                response.Dados = generos;
                response.Mensagem = "Gêneros localizados com sucesso!";

                return response;
            }
            catch (Exception ex)
            {
                response.Mensagem = ex.Message;
                response.Status = false;

                return response;
            }
        }
    }
}
