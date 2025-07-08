using GamesWebApi.Data;
using GamesWebApi.Dto.Produtora;
using GamesWebApi.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;

namespace GamesWebApi.Services.Produtora
{
    public class ProdutoraService : IProdutoraInterface
    {
        private readonly AppDbContext _context;

        public ProdutoraService(AppDbContext context)
        {
            _context = context;
        }

        public async Task<ResponseModel<ProdutoraModel>> BuscarProdutoraPorId(int idProdutora)
        {
            ResponseModel<ProdutoraModel> response = new ResponseModel<ProdutoraModel>();
            
            try
            {
                var produtora = await _context.Produtoras.FirstOrDefaultAsync(p => p.Id == idProdutora);

                if (produtora == null)
                {
                    response.Mensagem = "Produtora não encontrada!";
                    return response;
                }

                response.Dados = produtora;
                response.Mensagem = "Produtora encontrada com sucesso!";

                return response;
            }
            catch (Exception ex)
            {
                response.Mensagem = ex.Message;
                response.Status = false;

                return response;
            }
        }

        public async Task<ResponseModel<ProdutoraModel>> BuscarProdutoraPorIdJogo(int idJogo)
        {
            ResponseModel<ProdutoraModel> response = new ResponseModel<ProdutoraModel>();

            try
            {
                var jogo = await _context.Jogos
                    .Include(j => j.Produtora)
                    .FirstOrDefaultAsync(j => j.Id == idJogo);

                if (jogo == null)
                {
                    response.Mensagem = "Jogo não encontrado!";
                    return response;
                }

                ProdutoraModel produtora = jogo.Produtora;

                if (produtora == null)
                {
                    response.Mensagem = "O jogo não tem produtora definida!";
                    return response;
                }

                response.Dados = produtora;
                response.Mensagem = "Produtora localizada com sucesso!";

                return response;
            }
            catch (Exception ex)
            {
                response.Mensagem = ex.Message;
                response.Status = false;

                return response;
            }
        }

        public async Task<ResponseModel<ProdutoraModel>> CadastrarProdutora(ProdutoraCriacaoDto produtoraCriacaoDto)
        {
            ResponseModel<ProdutoraModel> response = new ResponseModel<ProdutoraModel>();

            try
            {
                if (produtoraCriacaoDto.Nome.IsNullOrEmpty())
                {
                    response.Mensagem = "Insira o nome da produtora!";
                    return response;
                }

                var produtora = new ProdutoraModel()
                {
                    Nome = produtoraCriacaoDto.Nome,
                };

                _context.Add(produtora);
                await _context.SaveChangesAsync();

                var novaProdutora = await _context.Produtoras
                    .OrderByDescending(p => p.Id)
                    .FirstOrDefaultAsync();

                response.Dados = novaProdutora;
                response.Mensagem = "Produtora cadastrada com sucesso!";

                return response;
            }
            catch (Exception ex)
            {
                response.Mensagem = ex.Message;
                response.Status = false;

                return response;
            }
        }

        public async Task<ResponseModel<ProdutoraModel>> EditarProdutora(ProdutoraEdicaoDto produtoraEdicaoDto)
        {
            ResponseModel<ProdutoraModel> response = new ResponseModel<ProdutoraModel>();

            try
            {
                var produtora = await _context.Produtoras.FirstOrDefaultAsync(p => p.Id == produtoraEdicaoDto.Id);

                if (produtora == null)
                {
                    response.Mensagem = "Produtora não encontrada!";
                    return response;
                }                

                if (produtoraEdicaoDto.Nome.IsNullOrEmpty())
                {
                    response.Mensagem = "Insira um nome para produtora";
                    return response;
                }

                produtora.Nome = produtoraEdicaoDto.Nome;

                _context.Update(produtora);
                await _context.SaveChangesAsync();

                response.Dados = produtora;
                response.Mensagem = "Produtora editada com sucesso!";
                
                return response;
            }
            catch (Exception ex)
            {
                response.Mensagem = ex.Message;
                response.Status = false;

                return response;
            }
        }

        public async Task<ResponseModel<List<ProdutoraModel>>> ExcluirProdutora(int idProdutora)
        {
            ResponseModel<List<ProdutoraModel>> response = new ResponseModel<List<ProdutoraModel>>();

            try
            {
                var produtora = await _context.Produtoras.FirstOrDefaultAsync(p => p.Id == idProdutora);

                if (produtora == null)
                {
                    response.Mensagem = "Produtora não encontrada!";
                    return response;
                }

                _context.Remove(produtora);
                await _context.SaveChangesAsync();

                response.Dados = await _context.Produtoras.ToListAsync();
                response.Mensagem = "Produtora excluída com sucesso, lista de produtoras disponíveis!";

                return response;
            }
            catch (Exception ex)
            {
                response.Mensagem = ex.Message;
                response.Status = false;

                return response;
            }
        }

        public async Task<ResponseModel<List<ProdutoraModel>>> ExibirProdutoras()
        {
            ResponseModel<List<ProdutoraModel>> response = new ResponseModel<List<ProdutoraModel>>();

            try
            {
                var produtora = await _context.Produtoras.ToListAsync();

                if (produtora == null)
                {
                    response.Mensagem = "Nenhuma produtora encontrada!";
                    return response;
                }

                response.Dados = produtora;
                response.Mensagem = "Produtoras localizadas com sucesso!";

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
