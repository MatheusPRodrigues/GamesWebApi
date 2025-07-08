using GamesWebApi.Data;
using GamesWebApi.Dto.Jogo;
using GamesWebApi.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using System.Text.Json.Serialization;

namespace GamesWebApi.Services.Jogos
{
    public class JogoService : IJogoInterface
    {
        private readonly AppDbContext _context;
        public JogoService(AppDbContext appDbContext)
        {
            _context = appDbContext;
        }

        public async Task<ResponseModel<JogoResponseDto>> BuscarJogoPorId(int idJogo)
        {
            ResponseModel<JogoResponseDto> response = new ResponseModel<JogoResponseDto>();

            try
            {
                var jogo = await _context.Jogos
                    .Include(jogoBd => jogoBd.Produtora)
                    .Include(jogoBd => jogoBd.Generos)
                    .Select(jogosBd => new JogoResponseDto
                    {
                        Id = jogosBd.Id,
                        Nome = jogosBd.Nome,
                        Descricao = jogosBd.Descricao,
                        Produtora = jogosBd.Produtora,
                        Generos = jogosBd.Generos.Select(generosBd => generosBd.Nome).ToList()
                    })
                    .FirstOrDefaultAsync(jogosBd => jogosBd.Id == idJogo);

                if(jogo == null)
                {
                    response.Mensagem = "Jogo não localizado!";
                    return response;
                }

                response.Dados = jogo;
                response.Mensagem = "Jogo localizado com sucesso!";

                return response;
            }
            catch (Exception ex)
            {
                response.Mensagem = ex.Message;
                response.Status = false;

                return response;
            }
        }

        public async Task<ResponseModel<List<JogoResponseDto>>> BuscarJogoPorIdGenero(int idGenero)
        {
            ResponseModel<List<JogoResponseDto>> response = new ResponseModel<List<JogoResponseDto>>();

            try
            {
                var jogos = await _context.Jogos
                    .Include(jogosBd => jogosBd.Produtora)
                    .Include(jogosBd => jogosBd.Generos)
                    .Where(jogosBd => jogosBd.Generos.Any(generosBd => generosBd.Id == idGenero))
                    .Select(jogosBd => new JogoResponseDto
                    {
                        Id = jogosBd.Id,
                        Nome = jogosBd.Nome,
                        Descricao = jogosBd.Descricao,
                        Produtora = jogosBd.Produtora,
                        Generos = jogosBd.Generos.Select(generosBd => generosBd.Nome).ToList()
                    })
                    .ToListAsync();

                if(jogos == null || !jogos.Any())
                {
                    response.Mensagem = "Não há jogos localizados!";
                    return response;
                }

                response.Dados = jogos;
                response.Mensagem = jogos.Count > 1 ?
                    "Jogos localizados com sucesso!" :
                    "Jogo localizado com sucesso!";

                return response;
            }
            catch (Exception ex)
            {
                response.Mensagem = ex.Message;
                response.Status = false;

                return response;
            }
        }

        public async Task<ResponseModel<List<JogoResponseDto>>> BuscarJogoPorIdProdutora(int idProdutora)
        {
            ResponseModel<List<JogoResponseDto>> response = new ResponseModel<List<JogoResponseDto>>();

            try
            {
                var jogos = await _context.Jogos
                    .Include(jogosBd => jogosBd.Produtora)
                    .Include(jogosBd => jogosBd.Generos)
                    .Select(jogosBd => new JogoResponseDto
                    {
                        Id = jogosBd.Id,
                        Nome = jogosBd.Nome,
                        Descricao = jogosBd.Descricao,
                        Produtora = jogosBd.Produtora,
                        Generos = jogosBd.Generos.Select(generosBd => generosBd.Nome).ToList()
                    })
                    .Where(jogosBd => jogosBd.Produtora.Id == idProdutora)
                    .ToListAsync();

                if(jogos == null || jogos.Count < 1)
                {
                    response.Mensagem = "Jogos não localizados!";
                    return response;
                }

                response.Dados = jogos;
                response.Mensagem = jogos.Count > 1 ?
                    "Jogos localizados com sucesso!" :
                    "Jogo localizado com sucesso!";

                return response;
            }
            catch (Exception ex)
            {
                response.Mensagem = ex.Message;
                response.Status = false;

                return response;
            }
        }

        public async Task<ResponseModel<JogoResponseDto>> CadastrarJogo(JogoCriacaoDto jogoCriacaoDto)
        {
            ResponseModel<JogoResponseDto> response = new ResponseModel<JogoResponseDto>();
            
            try
            {
                if (jogoCriacaoDto.idGenero.Count < 1)
                {
                    response.Mensagem = "Insira pelo menos 1 gênero para cadastrar o jogo!";
                    return response;
                }

                var produtora = await _context.Produtoras.FindAsync(jogoCriacaoDto.idProdutora);

                if (produtora == null)
                {
                    response.Mensagem = "Produtora não encontrada!";
                    return response;
                }

                var generos = await _context.Generos
                    .Where(g => jogoCriacaoDto.idGenero.Contains(g.Id))
                    .ToListAsync();

                if (generos == null)
                {
                    response.Mensagem = "Generos não encontrados!";
                    return response;
                }

                var jogo = new JogoModel(jogoCriacaoDto.Nome, jogoCriacaoDto.Descricao, produtora, generos);

                _context.Add(jogo);
                await _context.SaveChangesAsync();

                var novoJogo = await _context.Jogos
                    .Include(jogoBd => jogoBd.Produtora)
                    .Include(jogoBd => jogoBd.Generos)
                    .OrderByDescending(jogosBd => jogosBd.Id)
                    .Select(jogosBd => new JogoResponseDto
                    {
                        Id = jogosBd.Id,
                        Nome = jogosBd.Nome,
                        Descricao = jogosBd.Descricao,
                        Produtora = jogosBd.Produtora,
                        Generos = jogosBd.Generos.Select(generosBd => generosBd.Nome).ToList()
                    })
                    .FirstOrDefaultAsync();

                response.Dados = novoJogo;
                response.Mensagem = "Jogo cadastrado com sucesso!";

                return response;
            }
            catch (Exception ex)
            {
                response.Mensagem = ex.Message;
                response.Status = false;

                return response;
            }
        }

        public async Task<ResponseModel<JogoResponseDto>> EditarJogo(JogoEdicaoDto jogoEdicaoDto)
        {
            ResponseModel<JogoResponseDto> response = new ResponseModel<JogoResponseDto>();

            try
            {
                var jogo = await _context.Jogos
                    .Include(j => j.Generos)
                    .Include(j => j.Produtora)
                    .FirstOrDefaultAsync(j => j.Id == jogoEdicaoDto.Id);

                if (jogo == null)
                {
                    response.Mensagem = "Jogo não encontrado!";
                    return response;
                }

                if (jogoEdicaoDto.Nome.IsNullOrEmpty())
                {
                    response.Mensagem = "Insira um nome para o jogo!";
                    return response;
                }

                var produtora = await _context.Produtoras.FirstOrDefaultAsync(p => p.Id == jogoEdicaoDto.IdProdutora);

                if (produtora == null)
                {
                    response.Mensagem = "Produtora não encontrada!";
                    return response;
                }

                if (jogoEdicaoDto.idGeneros.Count < 1)
                {
                    response.Mensagem = "O jogo precisa ter pelo menos 1 gênero!";
                    return response;
                }

                var generos = await _context.Generos
                    .Where(g => jogoEdicaoDto.idGeneros.Contains(g.Id))
                    .ToListAsync();

                if (generos == null)
                {
                    response.Mensagem = "Generos não encontrados!";
                    return response;
                }

                jogo.Nome = jogoEdicaoDto.Nome;
                jogo.Descricao = jogoEdicaoDto.Descricao;
                jogo.Produtora = produtora;
                jogo.Generos = generos;

                _context.Update(jogo);
                await _context.SaveChangesAsync();

                var jogoEditado = await _context.Jogos
                    .Where(j => j.Id == jogoEdicaoDto.Id)
                    .Select(jogosBd => new JogoResponseDto
                    {
                        Id = jogosBd.Id,
                        Nome = jogosBd.Nome,
                        Descricao = jogosBd.Descricao,
                        Produtora = jogosBd.Produtora,
                        Generos = jogosBd.Generos.Select(generosBd => generosBd.Nome).ToList()
                    })
                    .FirstOrDefaultAsync();

                response.Dados = jogoEditado;
                response.Mensagem = "Jogo editado com sucesso!";

                return response;
            }
            catch (Exception ex)
            {
                response.Mensagem = ex.Message;
                response.Status = false;

                return response;
            }
        }

        public async Task<ResponseModel<List<JogoResponseDto>>> ExcluirJogo(int idJogo)
        {
            ResponseModel<List<JogoResponseDto>> response = new ResponseModel<List<JogoResponseDto>>();

            try
            {
                var jogo = await _context.Jogos.FirstOrDefaultAsync(j => j.Id == idJogo);

                if (jogo == null)
                {
                    response.Mensagem = "Jogo não encontrado!";
                    return response;
                }

                _context.Remove(jogo);
                await _context.SaveChangesAsync();

                var jogos = await _context.Jogos
                    .Select(j => new JogoResponseDto
                    {
                        Id = j.Id,
                        Nome = j.Nome,
                        Descricao = j.Descricao,
                        Produtora = j.Produtora,
                        Generos = j.Generos.Select(g => g.Nome).ToList()
                    })
                    .ToListAsync();

                response.Dados = jogos;
                response.Mensagem = "Jogo excluído com sucesso! Aqui está uma lista dos jogos disponíveis!";

                return response;
            }
            catch (Exception ex)
            {
                response.Mensagem = ex.Message;
                response.Status = false;

                return response;
            }
        }

        public async Task<ResponseModel<List<JogoResponseDto>>> ListarJogos()
        {
            ResponseModel<List<JogoResponseDto>> response = new ResponseModel<List<JogoResponseDto>>();

            try
            {
                var jogos = await _context.Jogos
                    .Include(jogosBd => jogosBd.Produtora)
                    .Include(jogosBd => jogosBd.Generos)
                    .Select(jogosBd => new JogoResponseDto
                    {
                        Id = jogosBd.Id,
                        Nome = jogosBd.Nome,
                        Descricao = jogosBd.Descricao,
                        Produtora = jogosBd.Produtora,
                        Generos = jogosBd.Generos.Select(generosBd => generosBd.Nome).ToList()
                    })
                    .ToListAsync();


                if(jogos == null)
                {
                    response.Mensagem = "Não houve jogos localizados!";
                    return response;
                }

                response.Dados = jogos;
                response.Mensagem = "Jogos localizados com sucesso!";

                return response;
            }
            catch (Exception ex)
            {
                response.Mensagem = ex.Message;
                response.Status = false;

                return response;
            }
        }

        public async Task<ResponseModel<List<JogoResponseDto>>> ListarJogosSemelhantes(int idJogo)
        {
            ResponseModel<List<JogoResponseDto>> response = new ResponseModel<List<JogoResponseDto>>();

            try
            {
                var selectedGame = await _context.Jogos
                    .Include(game => game.Produtora)
                    .Include(game => game.Generos)
                    .FirstOrDefaultAsync(game => game.Id == idJogo);

                if(selectedGame == null)
                {
                    response.Mensagem = $"Jogo de id{idJogo} não encontrado!";
                    return response;
                }

                var referenceGenres = selectedGame.Generos.Select(genre => genre.Id).ToList();

                int genreCount;

                if (referenceGenres.Count == 1)
                {
                    genreCount = 1;
                }
                else
                {
                    genreCount = referenceGenres.Count / 2;
                }

                var jogos = await _context.Jogos
                    .Include(jogosBd => jogosBd.Produtora)
                    .Include(jogosBd => jogosBd.Generos)                   
                    .Where(jogosBd => jogosBd.Id != idJogo)
                    .Where(jogosBd => jogosBd.Generos.Count(genero => referenceGenres.Contains(genero.Id)) > genreCount)
                    .OrderByDescending(jogosBd => jogosBd.Generos.Count(genero => referenceGenres.Contains(genero.Id)))
                    .Select(jogosBd => new JogoResponseDto
                    {
                        Id = jogosBd.Id,
                        Nome = jogosBd.Nome,
                        Descricao = jogosBd.Descricao,
                        Produtora = jogosBd.Produtora,
                        Generos = jogosBd.Generos.Select(generos => generos.Nome).ToList()
                    })
                    .ToListAsync();

                if(jogos == null || jogos.Count < 1)
                {
                    response.Mensagem = "Não foi encontrado nenhum jogo semelhante ao inserido!";
                    return response;
                }

                response.Dados = jogos;
                response.Mensagem = jogos.Count > 1 ?
                    "Jogos localizados com sucesso!" :
                    "Jogo localizado com sucesso!";

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
