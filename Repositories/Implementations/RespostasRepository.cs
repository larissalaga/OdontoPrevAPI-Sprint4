using Microsoft.EntityFrameworkCore;
using OdontoPrevAPI.Data;
using OdontoPrevAPI.Dtos;
using OdontoPrevAPI.Models;
using OdontoPrevAPI.Repositories.Interfaces;

namespace OdontoPrevAPI.Repositories.Implementations
{
    public class RespostasRepository : IRespostasRepository
    {
        private DataContext _context;

        public RespostasRepository(DataContext context)
        {
            _context = context;
        }

        public async Task<Models.Respostas> Create(RespostasDtos respostas)
        {
            // Use the INSERT_RESPOSTAS procedure from PKG_CRUD_RESPOSTAS
            await _context.Database.ExecuteSqlInterpolatedAsync($@"
                BEGIN
                    PKG_CRUD_RESPOSTAS.INSERT_RESPOSTAS({respostas.DsResposta});
                END;
            ");

            // Fetch the newly created response (most recent one with this description)
            var newResposta = await _context.Respostas
                .Where(r => r.DsResposta == respostas.DsResposta)
                .OrderByDescending(r => r.IdResposta)
                .FirstOrDefaultAsync();

            if (newResposta == null)
            {
                throw new Exception("Erro ao recuperar a Resposta criada.");
            }

            return newResposta;
        }

        public async Task<bool> Delete(int id)
        {
            var getResposta = await _context.Respostas.FirstOrDefaultAsync(x => x.IdResposta == id);
            if (getResposta == null)
            {
                return true;
            }
            else
            {
                // Use the DELETE_RESPOSTAS procedure from PKG_CRUD_RESPOSTAS
                await _context.Database.ExecuteSqlInterpolatedAsync($@"
                    BEGIN
                        PKG_CRUD_RESPOSTAS.DELETE_RESPOSTAS({id});
                    END;
                ");
                return true;
            }
        }

        public async Task<List<Models.Respostas>> GetAll()
        {
            var getRespostas = await _context.Respostas.ToListAsync();
            if (getRespostas != null)
            {
                return getRespostas;
            }
            else
            {
                throw new Exception("Nenhuma resposta encontrada.");
            }
        }

        public async Task<Models.Respostas> GetById(int id)
        {
            var getResposta = await _context.Respostas.FirstOrDefaultAsync(x => x.IdResposta == id);
            if (getResposta == null)
            {
                throw new Exception("Resposta não encontrada.");
            }
            else
            {
                return getResposta;
            }
        }

        public async Task<Models.Respostas> Update(int id, RespostasDtos respostas)
        {
            var getResposta = await _context.Respostas.FirstOrDefaultAsync(x => x.IdResposta == id);
            if (getResposta == null)
            {
                throw new Exception("Resposta não encontrada.");
            }
            
            // Check if this response is used in a check-in
            var checkIn = await _context.CheckIn.FirstOrDefaultAsync(c => c.IdResposta == id);
            if (checkIn != null)
            {
                // Format the date for Oracle (yyyy-MM-dd)
                string formattedDate = checkIn.DtCheckIn.ToString("yyyy-MM-dd");
                
                // Use the UPDATE_RESPOSTAS procedure from PKG_CRUD_RESPOSTAS
                await _context.Database.ExecuteSqlInterpolatedAsync($@"
                    BEGIN
                        PKG_CRUD_RESPOSTAS.UPDATE_RESPOSTAS(
                            {checkIn.IdPaciente},
                            {checkIn.IdPergunta},
                            TO_DATE({formattedDate}, 'YYYY-MM-DD'),
                            {respostas.DsResposta}
                        );
                    END;
                ");
            }
            else
            {
                // If the response is not associated with a check-in, just update it directly
                getResposta.DsResposta = respostas.DsResposta;
                await _context.SaveChangesAsync();
            }
            
            // Fetch the updated response
            var updatedResposta = await _context.Respostas.FirstOrDefaultAsync(x => x.IdResposta == id);
            return updatedResposta;
        }
    }
}
