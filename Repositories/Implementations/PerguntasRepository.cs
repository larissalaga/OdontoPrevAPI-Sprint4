using Microsoft.EntityFrameworkCore;
using OdontoPrevAPI.Data;
using OdontoPrevAPI.Dtos;
using OdontoPrevAPI.Models;
using OdontoPrevAPI.Repositories.Interfaces;

namespace OdontoPrevAPI.Repositories.Implementations
{
    public class PerguntasRepository : IPerguntasRepository
    {
        private DataContext _context;

        public PerguntasRepository(DataContext context)
        {
            _context = context;
        }
        public async Task<Models.Perguntas> Create(PerguntasDtos perguntas)
        {
            // First check if this question already exists
            var existingPergunta = await _context.Perguntas
                .FirstOrDefaultAsync(p => p.DsPergunta == perguntas.DsPergunta);
            
            if (existingPergunta != null)
            {
                throw new Exception("Pergunta já cadastrada.");
            }

            // Use the INSERT_PERGUNTAS procedure from PKG_CRUD_PERGUNTAS
            await _context.Database.ExecuteSqlInterpolatedAsync($@"
                BEGIN
                    PKG_CRUD_PERGUNTAS.INSERT_PERGUNTAS({perguntas.DsPergunta});
                END;
            ");

            // Fetch the newly created question
            var newPergunta = await _context.Perguntas
                .Where(p => p.DsPergunta == perguntas.DsPergunta)
                .OrderByDescending(p => p.IdPergunta)
                .FirstOrDefaultAsync();

            if (newPergunta == null)
            {
                throw new Exception("Erro ao recuperar a Pergunta criada.");
            }

            return newPergunta;
        }

        public async void Delete(int idPergunta)
        {
            var getPergunta = await _context.Perguntas.FirstOrDefaultAsync(x => x.IdPergunta == idPergunta);
            if (getPergunta == null)
            {
                throw new Exception("Pergunta não encontrada.");
            }
            else
            {
                // Use the DELETE_PERGUNTAS procedure from PKG_CRUD_PERGUNTAS
                await _context.Database.ExecuteSqlInterpolatedAsync($@"
                    BEGIN
                        PKG_CRUD_PERGUNTAS.DELETE_PERGUNTAS({idPergunta});
                    END;
                ");
            }
        }

        public async Task<List<Models.Perguntas>> GetAll()
        {
            var getPerguntas = await _context.Perguntas.ToListAsync();
            if (getPerguntas != null)
            {
                return getPerguntas;
            }
            else
            {
                throw new Exception("Nenhuma pergunta encontrada.");
            }
        }

        public async Task<Models.Perguntas> GetById(int idPergunta)
        {
            var getPergunta = await _context.Perguntas.FirstOrDefaultAsync(x => x.IdPergunta == idPergunta);
            if (getPergunta == null)
                throw new Exception("Pergunta não encontrada.");
            else
                return getPergunta;
        }

        public async Task<Models.Perguntas> GetProximaPerguntaAsync(int idPerguntaAtual)
        {
            var proximaPergunta = await _context.Perguntas
                .Where(p => p.IdPergunta > idPerguntaAtual)
                .OrderBy(p => p.IdPergunta)
                .FirstOrDefaultAsync();

            if (proximaPergunta == null)
            {
                return await _context.Perguntas
                    .OrderBy(p => p.IdPergunta)
                    .FirstOrDefaultAsync();
            }

            return proximaPergunta;
        }

        public async Task<Models.Perguntas> GetPerguntaAleatoriaAsync()
        {
            if (!_context.Perguntas.Any())
            {
                throw new Exception("Nenhuma pergunta encontrada.");
            }

            return await _context.Perguntas
                     .OrderBy(p => Guid.NewGuid()) // Ordenação aleatória
                     .FirstOrDefaultAsync();
        }
    }
}
