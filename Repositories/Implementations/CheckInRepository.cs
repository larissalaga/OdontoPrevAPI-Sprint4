using Microsoft.EntityFrameworkCore;
using OdontoPrevAPI.Data;
using OdontoPrevAPI.Dtos;
using OdontoPrevAPI.Models;
using OdontoPrevAPI.Repositories.Interfaces;

namespace OdontoPrevAPI.Repositories.Implementations
{
    public class CheckInRepository : ICheckInRepository
    {
        private DataContext _context;

        public CheckInRepository(DataContext context)
        {
            _context = context;
        }
        public async Task<Models.CheckIn> Create(CheckInDtos checkIn)
        {
            // Validate that the required entities exist
            var paciente = await _context.Paciente.FirstOrDefaultAsync(p => p.IdPaciente == checkIn.IdPaciente);
            if (paciente == null)
            {
                throw new Exception("Paciente não encontrado.");
            }

            var pergunta = await _context.Perguntas.FirstOrDefaultAsync(p => p.IdPergunta == checkIn.IdPergunta);
            if (pergunta == null)
            {
                throw new Exception("Pergunta não encontrada.");
            }

            var resposta = await _context.Respostas.FirstOrDefaultAsync(r => r.IdResposta == checkIn.IdResposta);
            if (resposta == null)
            {
                throw new Exception("Resposta não encontrada.");
            }

            // Format the date for Oracle (yyyy-MM-dd)
            string formattedDate = checkIn.DtCheckIn.ToString("yyyy-MM-dd");

            // Use the INSERT_CHECK_IN procedure from PKG_CRUD_CHECK_IN
            await _context.Database.ExecuteSqlInterpolatedAsync($@"
                BEGIN
                    PKG_CRUD_CHECK_IN.INSERT_CHECK_IN(
                        TO_DATE({formattedDate}, 'YYYY-MM-DD'),
                        {checkIn.IdPaciente},
                        {checkIn.IdPergunta},
                        {checkIn.IdResposta}
                    );
                END;
            ");

            // Fetch the newly created CheckIn (get the latest one for this patient)
            var newCheckIn = await _context.CheckIn
                .Where(c => c.IdPaciente == checkIn.IdPaciente)
                .OrderByDescending(c => c.IdCheckIn)
                .FirstOrDefaultAsync();

            if (newCheckIn == null)
            {
                throw new Exception("Erro ao recuperar o Check-In criado.");
            }

            return newCheckIn;
        }

        public async void Delete(int idCheckIn)
        {
            var getCheckIn = await _context.CheckIn.FirstOrDefaultAsync(x => x.IdCheckIn == idCheckIn);
            if (getCheckIn == null)
            {
                throw new Exception("Check-In não encontrado.");
            }
            else
            {
                // Use the DELETE_CHECK_IN procedure from PKG_CRUD_CHECK_IN
                await _context.Database.ExecuteSqlInterpolatedAsync($@"
                    BEGIN
                        PKG_CRUD_CHECK_IN.DELETE_CHECK_IN({idCheckIn});
                    END;
                ");
            }
        }

        public async Task<bool> DeleteByIdPaciente(int idPaciente)
        {
            var getCheckIn = await GetByIdPaciente(idPaciente);
            if (getCheckIn == null)
            {
                return false;
            }
            else
            {
                foreach (var check in getCheckIn)
                {
                    try
                    {
                        await _context.Database.ExecuteSqlInterpolatedAsync($@"
                            BEGIN
                                PKG_CRUD_CHECK_IN.DELETE_CHECK_IN({check.IdCheckIn});
                            END;
                        ");
                    }
                    catch (Exception e)
                    {
                        return false;
                    }
                }
            }
            return true;
        }

        public async Task<List<Models.CheckIn>> GetAll()
        {
            var getCheckIn = await _context.CheckIn.ToListAsync();
            if (getCheckIn != null)
            {
                return getCheckIn;
            }
            else
            {
                throw new Exception("Nenhum Check-In encontrado.");
            }
        }

        public async Task<Models.CheckIn> GetById(int idCheckIn)
        {
            var getCheckIn = await _context.CheckIn.FirstOrDefaultAsync(x => x.IdCheckIn == idCheckIn);
            if (getCheckIn == null)
            {
                throw new Exception("Check-In não encontrado.");
            }
            else
            {
                return getCheckIn;
            }
        }

        public async Task<List<Models.CheckIn>> GetByIdPaciente(int idPaciente)
        {
            var getCheckIn = await _context.CheckIn.Where(x => x.IdPaciente == idPaciente).ToListAsync();
            if (getCheckIn == null || !getCheckIn.Any())
            {
                throw new Exception("Nenhum Check-In encontrado para este paciente.");
            }
            else
            {
                return getCheckIn;
            }
        }

        public async Task<Models.CheckIn> Update(int idCheckIn, CheckInDtos checkIn)
        {
            var getCheckIn = await _context.CheckIn.FirstOrDefaultAsync(x => x.IdCheckIn == idCheckIn);
            if (getCheckIn == null)
            {
                throw new Exception("CheckIn não encontrado.");
            }
            else
            {
                getCheckIn.DtCheckIn = checkIn.DtCheckIn;
                getCheckIn.IdPaciente = checkIn.IdPaciente;
                getCheckIn.IdPergunta = checkIn.IdPergunta;
                getCheckIn.IdResposta = checkIn.IdResposta;
                await _context.SaveChangesAsync();
                return getCheckIn;
            }
        }
    }
}
